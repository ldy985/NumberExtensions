using System;

namespace SDK.Extensions
{
    public static partial class NumberExtensions
    {
        public static string ToBinary(this byte value)
        {
            return Convert.ToString(value, 2).PadLeft(8, _paddingChar);
        }

        public static bool GetBit(this byte value, byte pos)
        {
            return ((value >> pos) & 1) != 0;
        }
    }
}