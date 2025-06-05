namespace ldy985.NumberExtensions.Tests;

public class NumberExtensionsInt16Tests
{
    [Theory]
    [InlineData((short)0, "0000000000000000")]
    [InlineData((short)1, "0000000000000001")]
    [InlineData((short)-1, "1111111111111111")]
    [InlineData(unchecked((short)0b1010101010101010), "1010101010101010")]
    public void ToBinary_ReturnsCorrectBinaryString(short value, string expected)
    {
        Assert.Equal(expected, value.ToBinary());
    }

    [Theory]
    [InlineData((short)0b0000000000000001, 0, true)]
    [InlineData((short)0b0000000000000010, 1, true)]
    [InlineData((short)0b0000000000000000, 0, false)]
    [InlineData(unchecked((short)0b1000000000000000), 15, true)]
    public void GetBit_ReturnsCorrectBitValue(short value, byte pos, bool expected)
    {
        Assert.Equal(expected, value.GetBit(pos));
    }

    [Theory]
    [InlineData((short)0x1234, (short)0x3412)]
    [InlineData(unchecked((short)0xABCD), unchecked((short)0xCDAB))]
    public void Reverse_ReturnsReversedEndianness(short value, short expected)
    {
        Assert.Equal(expected, value.Reverse());
    }
}