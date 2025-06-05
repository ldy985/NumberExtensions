namespace ldy985.NumberExtensions.Tests;

public class NumberExtensionsUInt32Tests
{
    [Theory]
    [InlineData(0u, "00000000000000000000000000000000")]
    [InlineData(1u, "00000000000000000000000000000001")]
    [InlineData(4294967295u, "11111111111111111111111111111111")]
    [InlineData(0b10101010101010101010101010101010u, "10101010101010101010101010101010")]
    public void ToBinary_ReturnsCorrectBinaryString(uint value, string expected)
    {
        Assert.Equal(expected, value.ToBinary());
    }

    [Theory]
    [InlineData(0b1u, 0, true)]
    [InlineData(0b10u, 1, true)]
    [InlineData(0u, 0, false)]
    [InlineData(0x80000000u, 31, true)]
    public void GetBit_ReturnsCorrectBitValue(uint value, byte pos, bool expected)
    {
        Assert.Equal(expected, value.GetBit(pos));
    }

    [Theory]
    [InlineData(0x12345678u, 0x78563412u)]
    [InlineData(0xABCDEF01u, 0x01EFCDABu)]
    public void Reverse_ReturnsReversedEndianness(uint value, uint expected)
    {
        Assert.Equal(expected, value.Reverse());
    }

    [Theory]
    [InlineData(0x12345678u)]
    [InlineData(0xABCDEF01u)]
    public void ToBigEndian_And_ToLittleEndian_AreConsistent(uint value)
    {
        if (NumberExtensions.IsBigEndian)
        {
            Assert.Equal(value, value.ToBigEndian());
            Assert.Equal(value.Reverse(), value.ToLittleEndian());
        }
        else
        {
            Assert.Equal(value, value.ToLittleEndian());
            Assert.Equal(value.Reverse(), value.ToBigEndian());
        }
    }
}