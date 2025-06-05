using System.Buffers.Binary;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace ldy985.NumberExtensions;

public static partial class NumberExtensions
{
    /// <summary>
    ///     Checks whether or not a given bit is set.
    /// </summary>
    /// <param name="value">The input <see cref="uint" /> value.</param>
    /// <param name="pos">The position of the bit to check (in [0, 31] range).</param>
    /// <returns>Whether or not the n-th bit is set.</returns>
    /// <remarks>
    ///     This method doesn't validate <paramref name="pos" /> against the valid range.
    ///     If the parameter is not valid, the result will just be inconsistent.
    ///     Additionally, no conditional branches are used to retrieve the flag.
    /// </remarks>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe bool GetBit(this uint value, byte pos)
    {
#if DEBUG
        Debug.Assert(pos < 32, "Bit position out of range for uint (0-31)");
#endif
        // Read the n-th bit, downcast to byte
        byte flag = (byte)((value >> pos) & 1);

        // Reinterpret the byte to avoid the test, setnz and
        // movzx instructions (asm x64). This is because the JIT
        // compiler is able to optimize this reinterpret-cast as
        // a single "and eax, 0x1" instruction, whereas if we had
        // compared the previous computed flag against 0, the assembly
        // would have had to perform the test, set the non-zero
        // flag and then extend the (byte) result to eax.
        return *(bool*)&flag;
    }

    /// <summary>
    ///     Returns the binary representation of the <see cref="uint" /> value as a string.
    /// </summary>
    /// <param name="value">The input <see cref="uint" /> value.</param>
    /// <returns>The binary string representation.</returns>
    [Pure]
    public static string ToBinary(this uint value)
    {
        return Convert.ToString(unchecked((int)value), 2).PadLeft(32, _paddingChar);
    }

    /// <summary>Reverses the order of bytes in a 32-bit unsigned integer.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The converted value.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint Reverse(this uint value)
    {
        return BinaryPrimitives.ReverseEndianness(value);
    }

    /// <summary>Converts a 32-bit unsigned integer to big-endian format (see Remarks).</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The converted value.</returns>
    /// <remarks>The value will be converted according the current value of <see cref="Endianness" />.</remarks>
    [Pure]
    public static uint ToBigEndian(this uint value)
    {
        return IsBigEndian ? value : value.Reverse();
    }

    /// <summary>Converts a 32-bit unsigned integer to little-endian format (see Remarks).</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The converted value.</returns>
    /// <remarks>The value will be converted according the current value of <see cref="Endianness" />.</remarks>
    [Pure]
    public static uint ToLittleEndian(this uint value)
    {
        return IsLittleEndian ? value : value.Reverse();
    }
}