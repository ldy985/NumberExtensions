using System;

namespace SDK.Extensions
{
    public static partial class NumberExtensions
    {
        public static bool GetBit(this long value, byte pos)
        {
            return ((value >> pos) & 1) != 0;
        }

        public static string ToBinary(this long value)
        {
            return Convert.ToString(value, 2).PadLeft(64, _paddingChar);
        }

        /// <summary>
        ///     Reverses the order of bytes in a 64-bit signed integer.
        /// </summary>
        /// <param name="value">
        ///     The value to convert.
        /// </param>
        /// <returns>
        ///     The converted value.
        /// </returns>
        public static long Reverse(this long value)
        {
            ulong temp = (ulong)value;
            temp = (temp >> 32) | (temp << 32);
            temp = ((temp & 0xFFFF0000FFFF0000) >> 16) | ((temp & 0x0000FFFF0000FFFF) << 16);
            return (long)(((temp & 0xFF00FF00FF00FF00) >> 8) | ((temp & 0x00FF00FF00FF00FF) << 8));
        }

        /// <summary>
        ///     Converts a 64-bit signed integer to big-endian format (see Remarks).
        /// </summary>
        /// <param name="value">
        ///     The value to convert.
        /// </param>
        /// <returns>
        ///     The converted value.
        /// </returns>
        /// <remarks>
        ///     The value will be converted according the current value of <see cref="Endianness" />.
        /// </remarks>
        public static long ToBigEndian(this long value)
        {
            return IsBigEndian ? value : value.Reverse();
        }

        /// <summary>
        ///     Converts a 64-bit signed integer to little-endian format (see Remarks).
        /// </summary>
        /// <param name="value">
        ///     The value to convert.
        /// </param>
        /// <returns>
        ///     The converted value.
        /// </returns>
        /// <remarks>
        ///     The value will be converted according the current value of <see cref="Endianness" />.
        /// </remarks>
        public static long ToLittleEndian(this long value)
        {
            return IsLittleEndian ? value : value.Reverse();
        }
    }
}