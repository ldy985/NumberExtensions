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
    /// <param name="value">The input <see cref="ushort" /> value.</param>
    /// <param name="pos">The position of the bit to check (in [0, 15] range).</param>
    /// <returns>Whether or not the n-th bit is set.</returns>
    /// <remarks>
    ///     This method doesn't validate <paramref name="pos" /> against the valid range.
    ///     If the parameter is not valid, the result will just be inconsistent.
    ///     Additionally, no conditional branches are used to retrieve the flag.
    /// </remarks>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe bool GetBit(this ushort value, byte pos)
    {
#if DEBUG
        Debug.Assert(pos < 16, "Bit position out of range for ushort (0-15)");
#endif
        byte flag = (byte)((value >> pos) & 1);

        return *(bool*)&flag;
    }

    /// <summary>
    ///     Returns the binary representation of the <see cref="ushort" /> value as a string.
    /// </summary>
    /// <param name="value">The input <see cref="ushort" /> value.</param>
    /// <returns>The binary string representation.</returns>
    [Pure]
    public static string ToBinary(this ushort value)
    {
        return Convert.ToString(unchecked((short)value), 2).PadLeft(16, _paddingChar);
    }

    /// <summary>Reverses the order of bytes in a 16-bit unsigned integer.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The converted value.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort Reverse(this ushort value)
    {
        return BinaryPrimitives.ReverseEndianness(value);
    }

    /// <summary>Converts a 16-bit unsigned integer to big-endian format (see Remarks).</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The converted value.</returns>
    /// <remarks>The value will be converted according the current value of <see cref="Endianness" />.</remarks>
    [Pure]
    public static ushort ToBigEndian(this ushort value)
    {
        return IsBigEndian ? value : value.Reverse();
    }

    /// <summary>Converts a 16-bit unsigned integer to little-endian format (see Remarks).</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The converted value.</returns>
    /// <remarks>The value will be converted according the current value of <see cref="Endianness" />.</remarks>
    [Pure]
    public static ushort ToLittleEndian(this ushort value)
    {
        return IsLittleEndian ? value : value.Reverse();
    }
}