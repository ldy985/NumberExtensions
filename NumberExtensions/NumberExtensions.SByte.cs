using System;

namespace SDK.Extensions
{
    public static partial class NumberExtensions
    {
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