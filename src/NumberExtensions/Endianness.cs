using JetBrains.Annotations;

namespace ldy985.NumberExtensions;

/// <summary>
///     Defines the byte order (endianness) used to represent multibyte values in memory.
/// </summary>
/// <remarks>
///     Use <see cref="Endianness.BigEndian" /> for big-endian and <see cref="Endianness.LittleEndian" /> for little-endian
///     systems.
/// </remarks>
[PublicAPI]
public enum Endianness
{
    /// <summary>
    ///     Most significant byte is stored at the lowest memory address.
    /// </summary>
    BigEndian,

    /// <summary>
    ///     Least significant byte is stored at the lowest memory address.
    /// </summary>
    LittleEndian
}