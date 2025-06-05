namespace ldy985.NumberExtensions.Tests;

public class NumberExtensionsSByteTests
{
    [Theory]
    [InlineData((sbyte)0, "00000000")]
    [InlineData((sbyte)1, "00000001")]
    [InlineData((sbyte)-1, "11111111")]
    [InlineData(unchecked((sbyte)0b10101010), "10101010")]
    public void ToBinary_ReturnsCorrectBinaryString(sbyte value, string expected)
    {
        Assert.Equal(expected, value.ToBinary());
    }

    [Theory]
    [InlineData((sbyte)0b00000001, 0, true)]
    [InlineData((sbyte)0b00000010, 1, true)]
    [InlineData((sbyte)0b00000000, 0, false)]
    [InlineData(unchecked((sbyte)0b10000000), 7, true)]
    public void GetBit_ReturnsCorrectBitValue(sbyte value, byte pos, bool expected)
    {
        Assert.Equal(expected, value.GetBit(pos));
    }
}