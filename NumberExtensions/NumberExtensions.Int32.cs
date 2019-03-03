using System;

namespace SDK.Extensions
{
    public static partial class NumberExtensions
    {
        public static bool GetBit(this int value, byte pos)
        {
            return ((value >> pos) & 1) != 0;
        }

        public static string ToBinary(this int value)
        {
            return Convert.ToString(value, 2).PadLeft(32, _paddingChar);
        }

        /// <summary>
        ///     Reverses the order of bytes in a 32-bit signed integer.
        /// </summary>
        /// <param name="value">
        ///     The value to convert.
        /// </param>
        /// <returns>
        ///     The converted value.
        /// </returns>
        public static int Reverse(this int value)
        {
            uint temp = (uint)value;
            temp = (temp >> 16) | (temp << 16);
            return (int)(((temp & 0xFF00FF00) >> 8) | ((temp & 0x00FF00FF) << 8));
        }

        /// <summary>
        ///     Converts a 32-bit signed integer to big-endian format (see Remarks).
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
        public static int ToBigEndian(this int value)
        {
            return IsBigEndian ? value : value.Reverse();
        }

        /// <summary>
        ///     Converts a 32-bit signed integer to little-endian format (see Remarks).
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
        public static int ToLittleEndian(this int value)
        {
            return IsLittleEndian ? value : value.Reverse();
        }
    }
}