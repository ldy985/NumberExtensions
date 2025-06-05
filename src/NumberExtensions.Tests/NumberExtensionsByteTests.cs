namespace ldy985.NumberExtensions.Tests;

public class NumberExtensionsByteTests
{
    [Theory]
    [InlineData((byte)0, "00000000")]
    [InlineData((byte)1, "00000001")]
    [InlineData((byte)255, "11111111")]
    [InlineData((byte)170, "10101010")]
    public void ToBinary_ReturnsCorrectBinaryString(byte value, string expected)
    {
        Assert.Equal(expected, value.ToBinary());
    }

    [Theory]
    [InlineData((byte)0b_00000001, 0, true)]
    [InlineData((byte)0b_00000010, 1, true)]
    [InlineData((byte)0b_00000100, 2, true)]
    [InlineData((byte)0b_00000000, 0, false)]
    [InlineData((byte)0b_00000000, 7, false)]
    [InlineData((byte)0b_10000000, 7, true)]
    public void GetBit_ReturnsCorrectBitValue(byte value, byte pos, bool expected)
    {
        Assert.Equal(expected, value.GetBit(pos));
    }
}