using System.Diagnostics;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
#if NET8_0_OR_GREATER
using System.Buffers.Binary;
#endif

namespace ldy985.NumberExtensions;

public static partial class NumberExtensions
{
    /// <summary>
    ///     Checks whether or not a given bit is set.
    /// </summary>
    /// <param name="value">The input <see cref="Int128" /> value.</param>
    /// <param name="pos">The position of the bit to check (in [0, 127] range).</param>
    /// <returns>Whether or not the n-th bit is set.</returns>
    /// <remarks>
    ///     This method doesn't validate <paramref name="pos" /> against the valid range.
    ///     If the parameter is not valid, the result will just be inconsistent.
    ///     Additionally, no conditional branches are used to retrieve the flag.
    /// </remarks>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe bool GetBit(this Int128 value, byte pos)
    {
#if DEBUG
        Debug.Assert(pos < 128, "Bit position out of range for Int128 (0-127)");
#endif
        // Same logic as the uint version, see that for more info
        byte flag = (byte)((value >> pos) & 1);

        return *(bool*)&flag;
    }
#if NET8_0_OR_GREATER
    /// <summary>Reverses the order of bytes in a 128-bit signed integer.</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The converted value.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int128 Reverse(this Int128 value)
    {
        return BinaryPrimitives.ReverseEndianness(value);
    }

    /// <summary>Converts a 128-bit signed integer to big-endian format (see Remarks).</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The converted value.</returns>
    /// <remarks>The value will be converted according the current value of <see cref="Endianness" />.</remarks>
    [Pure]
    public static Int128 ToBigEndian(this Int128 value)
    {
        return IsBigEndian ? value : value.Reverse();
    }

    /// <summary>Converts a 128-bit signed integer to little-endian format (see Remarks).</summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The converted value.</returns>
    /// <remarks>The value will be converted according the current value of <see cref="Endianness" />.</remarks>
    [Pure]
    public static Int128 ToLittleEndian(this Int128 value)
    {
        return IsLittleEndian ? value : value.Reverse();
    }
#endif
}