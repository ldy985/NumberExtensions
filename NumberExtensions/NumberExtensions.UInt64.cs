using System;

namespace SDK.Extensions
{
    public static partial class NumberExtensions
    {
        public static bool GetBit(this ulong value, byte pos)
        {
            return ((value >> pos) & 1) != 0;
        }

        public static string ToBinary(this ulong value)
        {
            return Convert.ToString((long)value, 2).PadLeft(64, _paddingChar);
        }

        /// <summary>
        ///     Reverses the order of bytes in a 64-bit unsigned integer.
        /// </summary>
        /// <param name="value">
        ///     The value to convert.
        /// </param>
        /// <returns>
        ///     The converted value.
        /// </returns>
        public static ulong Reverse(this ulong value)
        {
            value = (value >> 32) | (value << 32);
            value = ((value & 0xFFFF0000FFFF0000) >> 16) | ((value & 0x0000FFFF0000FFFF) << 16);
            return ((value & 0xFF00FF00FF00FF00) >> 8) | ((value & 0x00FF00FF00FF00FF) << 8);
        }

        /// <summary>
        ///     Converts a 64-bit unsigned integer to big-endian format (see Remarks).
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
        public static ulong ToBigEndian(this ulong value)
        {
            return IsBigEndian ? value : value.Reverse();
        }

        /// <summary>
        ///     Converts a 64-bit unsigned integer to little-endian format (see Remarks).
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
        public static ulong ToLittleEndian(this ulong value)
        {
            return IsLittleEndian ? value : value.Reverse();
        }
    }
}