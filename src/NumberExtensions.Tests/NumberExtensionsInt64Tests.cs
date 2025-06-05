namespace ldy985.NumberExtensions.Tests;

public class NumberExtensionsInt64Tests
{
    [Theory]
    [InlineData(0L, "0000000000000000000000000000000000000000000000000000000000000000")]
    [InlineData(1L, "0000000000000000000000000000000000000000000000000000000000000001")]
    [InlineData(-1L, "1111111111111111111111111111111111111111111111111111111111111111")]
    [InlineData(unchecked((long)0b1010101010101010101010101010101010101010101010101010101010101010), "1010101010101010101010101010101010101010101010101010101010101010")]
    public void ToBinary_ReturnsCorrectBinaryString(long value, string expected)
    {
        Assert.Equal(expected, value.ToBinary());
    }

    [Theory]
    [InlineData(0b1L, 0, true)]
    [InlineData(0b10L, 1, true)]
    [InlineData(0L, 0, false)]
    [InlineData(unchecked((long)0x8000000000000000), 63, true)]
    public void GetBit_ReturnsCorrectBitValue(long value, byte pos, bool expected)
    {
        Assert.Equal(expected, value.GetBit(pos));
    }

    [Theory]
    [InlineData(0x0123456789ABCDEF, unchecked((long)0xEFCDAB8967452301))]
    [InlineData(unchecked((long)0xFEDCBA9876543210), 0x1032547698BADCFE)]
    public void Reverse_ReturnsReversedEndianness(long value, long expected)
    {
        Assert.Equal(expected, value.Reverse());
    }
}