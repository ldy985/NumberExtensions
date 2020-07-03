using System;
using System.Runtime.CompilerServices;

namespace ldy985.NumberExtensions
{
    public static partial class NumberExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GetBit(this sbyte value, byte pos)
        {
            return ((value >> pos) & 1) != 0;
        }

        public static string ToBinary(this sbyte value)
        {
            return Convert.ToString((byte)value, 2).PadLeft(8, _paddingChar);
        }
    }
}