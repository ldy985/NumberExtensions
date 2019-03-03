using System;

namespace SDK.Extensions
{
    public static partial class NumberExtensions
    {
        public static bool GetBit(this uint value, byte pos)
        {
            return ((value >> pos) & 1) != 0;
        }

        public static string ToBinary(this uint value)
        {
            return Convert.ToString((int)value, 2).PadLeft(32, _paddingChar);
        }

        /// <summary>
        ///     Reverses the order of bytes in a 32-bit unsigned integer.
        /// </summary>
        /// <param name="value">
        ///     The value to convert.
        /// </param>
        /// <returns>
        ///     The converted value.
        /// </returns>
        public static uint Reverse(this uint value)
        {
            value = (value >> 16) | (value << 16);
            return ((value & 0xFF00FF00) >> 8) | ((value & 0x00FF00FF) << 8);
        }

        /// <summary>
        ///     Converts a 32-bit unsigned integer to big-endian format (see Remarks).
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
        public static uint ToBigEndian(this uint value)
        {
            return IsBigEndian ? value : value.Reverse();
        }

        /// <summary>
        ///     Converts a 32-bit unsigned integer to little-endian format (see Remarks).
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
        public static uint ToLittleEndian(this uint value)
        {
            return IsLittleEndian ? value : value.Reverse();
        }
    }
}