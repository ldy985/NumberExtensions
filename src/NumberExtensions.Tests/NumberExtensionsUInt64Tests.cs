namespace ldy985.NumberExtensions.Tests;

public class NumberExtensionsUInt64Tests
{
    [Theory]
    [InlineData(0UL, "0000000000000000000000000000000000000000000000000000000000000000")]
    [InlineData(1UL, "0000000000000000000000000000000000000000000000000000000000000001")]
    [InlineData(18446744073709551615UL, "1111111111111111111111111111111111111111111111111111111111111111")]
    [InlineData(0b1010101010101010101010101010101010101010101010101010101010101010UL, "1010101010101010101010101010101010101010101010101010101010101010")]
    public void ToBinary_ReturnsCorrectBinaryString(ulong value, string expected)
    {
        Assert.Equal(expected, value.ToBinary());
    }

    [Theory]
    [InlineData(0b1UL, 0, true)]
    [InlineData(0b10UL, 1, true)]
    [InlineData(0UL, 0, false)]
    [InlineData(0x8000000000000000UL, 63, true)]
    public void GetBit_ReturnsCorrectBitValue(ulong value, byte pos, bool expected)
    {
        Assert.Equal(expected, value.GetBit(pos));
    }

    [Theory]
    [InlineData(0x0123456789ABCDEFUL, 0xEFCDAB8967452301UL)]
    [InlineData(0xFEDCBA9876543210UL, 0x1032547698BADCFEUL)]
    public void Reverse_ReturnsReversedEndianness(ulong value, ulong expected)
    {
        Assert.Equal(expected, value.Reverse());
    }
}