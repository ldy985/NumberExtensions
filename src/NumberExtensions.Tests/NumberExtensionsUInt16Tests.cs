namespace ldy985.NumberExtensions.Tests;

public class NumberExtensionsUInt16Tests
{
    [Theory]
    [InlineData((ushort)0, "0000000000000000")]
    [InlineData((ushort)1, "0000000000000001")]
    [InlineData((ushort)65535, "1111111111111111")]
    [InlineData((ushort)0b1010101010101010, "1010101010101010")]
    public void ToBinary_ReturnsCorrectBinaryString(ushort value, string expected)
    {
        Assert.Equal(expected, value.ToBinary());
    }

    [Theory]
    [InlineData((ushort)0b0000000000000001, 0, true)]
    [InlineData((ushort)0b0000000000000010, 1, true)]
    [InlineData((ushort)0b0000000000000000, 0, false)]
    [InlineData((ushort)0b1000000000000000, 15, true)]
    public void GetBit_ReturnsCorrectBitValue(ushort value, byte pos, bool expected)
    {
        Assert.Equal(expected, value.GetBit(pos));
    }

    [Theory]
    [InlineData((ushort)0x1234, (ushort)0x3412)]
    [InlineData((ushort)0xABCD, (ushort)0xCDAB)]
    public void Reverse_ReturnsReversedEndianness(ushort value, ushort expected)
    {
        Assert.Equal(expected, value.Reverse());
    }
}