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
        /// <param name="pos">The position of the bit to check (in [0, 31] range).</param>
        /// <returns>Whether or not the n-th bit is set.</returns>
        /// <remarks>
        ///     This method doesn't validate <paramref name="pos" /> against the valid range.
        ///     If the parameter is not valid, the result will just be inconsistent.
        ///     Additionally, no conditional branches are used to retrieve the flag.
        /// </remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe bool GetBit(this int value, byte pos)
        {
            // Same logic as the uint version, see that for more info
            byte flag = (byte) ((value >> pos) & 1);

            return *(bool*) &flag;
        }

        public static string ToBinary(this int value)
        {
            return Convert.ToString(value, 2).PadLeft(32, _paddingChar);
        }

        /// <summary>Reverses the order of bytes in a 32-bit signed integer.</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Reverse(this int value)
        {
            return BinaryPrimitives.ReverseEndianness(value);
        }

        /// <summary>Converts a 32-bit signed integer to big-endian format (see Remarks).</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        /// <remarks>The value will be converted according the current value of <see cref="Endianness" />.</remarks>
        public static int ToBigEndian(this int value)
        {
            return IsBigEndian ? value : value.Reverse();
        }

        /// <summary>Converts a 32-bit signed integer to little-endian format (see Remarks).</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        /// <remarks>The value will be converted according the current value of <see cref="Endianness" />.</remarks>
        public static int ToLittleEndian(this int value)
        {
            return IsLittleEndian ? value : value.Reverse();
        }
    }
}