namespace ldy985.NumberExtensions.Tests;

#if NET8_0_OR_GREATER
public class NumberExtensionsUInt128Tests
{
    [Theory]
    [InlineData("0", 0, false)]
    [InlineData("1", 0, true)]
    [InlineData("170141183460469231731687303715884105728", 127, true)] // (UInt128)1 << 127
    [InlineData("0", 127, false)]
    public void GetBit_ReturnsCorrectBitValue(string valueStr, byte pos, bool expected)
    {
        UInt128 value = UInt128.Parse(valueStr);
        Assert.Equal(expected, value.GetBit(pos));
    }

    [Theory]
    [InlineData("22774453838368691933757882222884355840", "1328880485197782561564485803532558865")] // 0x112233445566778899AABBCCDDEEFF00, 0x00FFEEDDCCBBAA998877665544332211
    public void Reverse_ReturnsReversedEndianness(string valueStr, string expectedStr)
    {
        UInt128 value = UInt128.Parse(valueStr);
        UInt128 expected = UInt128.Parse(expectedStr);
        Assert.Equal(expected, value.Reverse());
    }
}
#endif