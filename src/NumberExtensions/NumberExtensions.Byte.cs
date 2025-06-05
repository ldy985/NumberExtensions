using System.Diagnostics;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace ldy985.NumberExtensions;

public static partial class NumberExtensions
{
    /// <summary>
    ///     Returns the binary representation of the <see cref="byte" /> value as a string.
    /// </summary>
    /// <param name="value">The input <see cref="byte" /> value.</param>
    /// <returns>The binary string representation.</returns>
    public static string ToBinary(this byte value)
    {
        return Convert.ToString(value, 2).PadLeft(8, _paddingChar);
    }

    /// <summary>
    ///     Checks whether or not a given bit is set.
    /// </summary>
    /// <param name="value">The input <see cref="byte" /> value.</param>
    /// <param name="pos">The position of the bit to check (in [0, 7] range).</param>
    /// <returns>Whether or not the n-th bit is set.</returns>
    /// <remarks>
    ///     This method doesn't validate <paramref name="pos" /> against the valid range.
    ///     If the parameter is not valid, the result will just be inconsistent.
    ///     Additionally, no conditional branches are used to retrieve the flag.
    /// </remarks>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe bool GetBit(this byte value, byte pos)
    {
#if DEBUG
        Debug.Assert(pos < 8, "Bit position out of range for byte (0-7)");
#endif
        byte flag = (byte)((value >> pos) & 1);
        return *(bool*)&flag;
    }
}