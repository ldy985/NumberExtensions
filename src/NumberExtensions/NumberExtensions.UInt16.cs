using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;

namespace ldy985.NumberExtensions
{
    public static partial class NumberExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GetBit(this ushort value, byte pos)
        {
            return ((value >> pos) & 1) != 0;
        }

        public static string ToBinary(this ushort value)
        {
            return Convert.ToString((short)value, 2).PadLeft(16, _paddingChar);
        }

        /// <summary>Reverses the order of bytes in a 16-bit unsigned integer.</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort Reverse(this ushort value)
        {
            return BinaryPrimitives.ReverseEndianness(value);
        }

        /// <summary>Converts a 16-bit unsigned integer to big-endian format (see Remarks).</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        /// <remarks>The value will be converted according the current value of <see cref="Endianness" />.</remarks>
        public static ushort ToBigEndian(this ushort value)
        {
            return IsBigEndian ? value : value.Reverse();
        }

        /// <summary>Converts a 16-bit unsigned integer to little-endian format (see Remarks).</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        /// <remarks>The value will be converted according the current value of <see cref="Endianness" />.</remarks>
        public static ushort ToLittleEndian(this ushort value)
        {
            return IsLittleEndian ? value : value.Reverse();
        }
    }
}