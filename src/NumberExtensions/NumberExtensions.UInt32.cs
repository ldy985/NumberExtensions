using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;

namespace ldy985.NumberExtensions
{
    public static partial class NumberExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GetBit(this uint value, byte pos)
        {
            return ((value >> pos) & 1) != 0;
        }

        public static string ToBinary(this uint value)
        {
            return Convert.ToString((int)value, 2).PadLeft(32, _paddingChar);
        }

        /// <summary>Reverses the order of bytes in a 32-bit unsigned integer.</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Reverse(this uint value)
        {
            return BinaryPrimitives.ReverseEndianness(value);
        }

        /// <summary>Converts a 32-bit unsigned integer to big-endian format (see Remarks).</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        /// <remarks>The value will be converted according the current value of <see cref="Endianness" />.</remarks>
        public static uint ToBigEndian(this uint value)
        {
            return IsBigEndian ? value : value.Reverse();
        }

        /// <summary>Converts a 32-bit unsigned integer to little-endian format (see Remarks).</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        /// <remarks>The value will be converted according the current value of <see cref="Endianness" />.</remarks>
        public static uint ToLittleEndian(this uint value)
        {
            return IsLittleEndian ? value : value.Reverse();
        }
    }
}