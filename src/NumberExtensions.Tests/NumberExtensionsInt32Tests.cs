namespace ldy985.NumberExtensions.Tests;

public class NumberExtensionsInt32Tests
{
    [Theory]
    [InlineData(0, "00000000000000000000000000000000")]
    [InlineData(1, "00000000000000000000000000000001")]
    [InlineData(-1, "11111111111111111111111111111111")]
    [InlineData(unchecked((int)0b10101010101010101010101010101010), "10101010101010101010101010101010")]
    public void ToBinary_ReturnsCorrectBinaryString(int value, string expected)
    {
        Assert.Equal(expected, value.ToBinary());
    }

    [Theory]
    [InlineData(0b00000000000000000000000000000001, 0, true)]
    [InlineData(0b00000000000000000000000000000010, 1, true)]
    [InlineData(0, 0, false)]
    [InlineData(unchecked((int)0b10000000000000000000000000000000), 31, true)]
    public void GetBit_ReturnsCorrectBitValue(int value, byte pos, bool expected)
    {
        Assert.Equal(expected, value.GetBit(pos));
    }

    [Theory]
    [InlineData(0x12345678, 0x78563412)]
    [InlineData(unchecked((int)0xABCDEF01), 0x01EFCDAB)]
    public void Reverse_ReturnsReversedEndianness(int value, int expected)
    {
        Assert.Equal(expected, value.Reverse());
    }
}