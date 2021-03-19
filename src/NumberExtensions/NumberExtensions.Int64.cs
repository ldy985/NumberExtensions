using System;
using System.Buffers.Binary;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace ldy985.NumberExtensions
{
    public static partial class NumberExtensions
    {
        /// <summary>
        ///     Checks whether or not a given bit is set.
        /// </summary>
        /// <param name="value">The input <see cref="ulong" /> value.</param>
        /// <param name="pos">The position of the bit to check (in [0, 63] range).</param>
        /// <returns>Whether or not the n-th bit is set.</returns>
        /// <remarks>
        ///     This method doesn't validate <paramref name="pos" /> against the valid range.
        ///     If the parameter is not valid, the result will just be inconsistent.
        ///     Additionally, no conditional branches are used to retrieve the flag.
        /// </remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe bool GetBit(this long value, byte pos)
        {
            // Same logic as the uint version, see that for more info
            byte flag = (byte) ((value >> pos) & 1);

            return *(bool*) &flag;
        }

        public static string ToBinary(this long value)
        {
            return Convert.ToString(value, 2).PadLeft(64, _paddingChar);
        }

        /// <summary>Reverses the order of bytes in a 64-bit signed integer.</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Reverse(this long value)
        {
            return BinaryPrimitives.ReverseEndianness(value);
        }

        /// <summary>Converts a 64-bit signed integer to big-endian format (see Remarks).</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        /// <remarks>The value will be converted according the current value of <see cref="Endianness" />.</remarks>
        public static long ToBigEndian(this long value)
        {
            return IsBigEndian ? value : value.Reverse();
        }

        /// <summary>Converts a 64-bit signed integer to little-endian format (see Remarks).</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        /// <remarks>The value will be converted according the current value of <see cref="Endianness" />.</remarks>
        public static long ToLittleEndian(this long value)
        {
            return IsLittleEndian ? value : value.Reverse();
        }
    }
}