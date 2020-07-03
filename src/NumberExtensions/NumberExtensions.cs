using System;
using JetBrains.Annotations;

namespace ldy985.NumberExtensions
{
    [PublicAPI]
    public static partial class NumberExtensions
    {
        private const char _paddingChar = '0';

        /// <summary>Gets the environment endianness.</summary>
        public static readonly Endianness Endianness = BitConverter.IsLittleEndian ? Endianness.LittleEndian : Endianness.BigEndian;

        /// <summary>Gets if the environment endianness is big-endian.</summary>
        public static readonly bool IsBigEndian = Endianness == Endianness.BigEndian;

        /// <summary>Gets if the environment endianness is little-endian.</summary>
        public static readonly bool IsLittleEndian = Endianness == Endianness.LittleEndian;
    }
}