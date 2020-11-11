﻿using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;

namespace ldy985.NumberExtensions
{
    public static partial class NumberExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GetBit(this int value, byte pos)
        {
            return ((value >> pos) & 1) != 0;
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