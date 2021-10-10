using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace ldy985.NumberExtensions
{
    public static partial class NumberExtensions
    {
        public static string ToBinary(this byte value)
        {
            return Convert.ToString(value, 2).PadLeft(8, _paddingChar);
        }

        /// <summary>
        ///     Checks whether or not a given bit is set.
        /// </summary>
        /// <param name="value">The input <see cref="ulong" /> value.</param>
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
            // Same logic as the uint version, see that for more info
            byte flag = (byte)((value >> pos) & 1);

            return *(bool*)&flag;
        }
    }
}