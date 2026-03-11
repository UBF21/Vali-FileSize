namespace Vali_FileSize.Tests;

public class ConvertTests
{
    private readonly FileSizeConverter _sut = new();

    [Fact]
    public void Convert_BytesToKilobytes_ReturnsCorrectValue()
    {
        double result = _sut.Convert(1024, FileSizeUnit.Bytes, FileSizeUnit.Kilobytes);
        result.Should().Be(1.0);
    }

    [Fact]
    public void Convert_KilobytesToMegabytes_ReturnsCorrectValue()
    {
        double result = _sut.Convert(1024, FileSizeUnit.Kilobytes, FileSizeUnit.Megabytes);
        result.Should().Be(1.0);
    }

    [Fact]
    public void Convert_GigabytesToKilobytes_ReturnsCorrectValue()
    {
        double result = _sut.Convert(2.5, FileSizeUnit.Gigabytes, FileSizeUnit.Kilobytes);
        result.Should().Be(2_621_440.0);
    }

    [Fact]
    public void Convert_TerabytesToBytes_LargeValue_ReturnsCorrectValue()
    {
        double result = _sut.Convert(1.5, FileSizeUnit.Terabytes, FileSizeUnit.Bytes);
        result.Should().Be(1_649_267_441_664.0);
    }

    [Fact]
    public void Convert_SameUnit_ReturnsSameValue()
    {
        double result = _sut.Convert(512, FileSizeUnit.Megabytes, FileSizeUnit.Megabytes);
        result.Should().Be(512);
    }

    [Fact]
    public void Convert_ZeroBytes_ReturnsZero()
    {
        double result = _sut.Convert(0, FileSizeUnit.Bytes, FileSizeUnit.Gigabytes);
        result.Should().Be(0);
    }

    [Fact]
    public void Convert_NegativeSize_ThrowsArgumentException()
    {
        Action act = () => _sut.Convert(-1, FileSizeUnit.Bytes, FileSizeUnit.Kilobytes);
        act.Should().Throw<ArgumentException>().WithParameterName("size");
    }

    [Fact]
    public void Convert_IecKibibytesToBytes_ReturnsCorrectValue()
    {
        double result = _sut.Convert(1, FileSizeUnit.Kibibytes, FileSizeUnit.Bytes);
        result.Should().Be(1024);
    }

    [Fact]
    public void Convert_KilobytesToKibibytes_ReturnsSameValue()
    {
        // Both use 1024 internally — result must be identical
        double result = _sut.Convert(1, FileSizeUnit.Kilobytes, FileSizeUnit.Kibibytes);
        result.Should().Be(1.0);
    }
}
