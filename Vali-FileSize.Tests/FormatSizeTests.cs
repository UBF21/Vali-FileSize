using System.Globalization;

namespace Vali_FileSize.Tests;

public class FormatSizeTests
{
    private readonly FileSizeConverter _sut = new();

    [Fact]
    public void FormatSize_DefaultDecimalPlaces_ReturnsTwoDecimals()
    {
        string result = _sut.FormatSize(1.5, FileSizeUnit.Megabytes);
        result.Should().Be("1.50 MB");
    }

    [Fact]
    public void FormatSize_CustomDecimalPlaces_ReturnsCorrectPrecision()
    {
        string result = _sut.FormatSize(1.23456, FileSizeUnit.Gigabytes, decimalPlaces: 4);
        result.Should().Be("1.2346 GB");
    }

    [Fact]
    public void FormatSize_ZeroDecimalPlaces_ReturnsNoDecimals()
    {
        string result = _sut.FormatSize(512, FileSizeUnit.Bytes, decimalPlaces: 0);
        result.Should().Be("512 B");
    }

    [Fact]
    public void FormatSize_GermanCulture_UsesCommaAsSeparator()
    {
        var culture = new CultureInfo("de-DE");
        string result = _sut.FormatSize(1234.567, FileSizeUnit.Kilobytes, 2, culture);
        result.Should().Be("1234,57 KB");
    }

    [Fact]
    public void FormatSize_NegativeDecimalPlaces_ThrowsArgumentOutOfRangeException()
    {
        Action act = () => _sut.FormatSize(100, FileSizeUnit.Megabytes, decimalPlaces: -1);
        act.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("decimalPlaces");
    }

    [Fact]
    public void FormatSize_IecUnit_ReturnsIecSuffix()
    {
        string result = _sut.FormatSize(1.0, FileSizeUnit.Mebibytes);
        result.Should().Be("1.00 MiB");
    }

    [Theory]
    [InlineData(FileSizeUnit.Bytes,     "B")]
    [InlineData(FileSizeUnit.Kilobytes, "KB")]
    [InlineData(FileSizeUnit.Megabytes, "MB")]
    [InlineData(FileSizeUnit.Gigabytes, "GB")]
    [InlineData(FileSizeUnit.Terabytes, "TB")]
    [InlineData(FileSizeUnit.Petabytes, "PB")]
    [InlineData(FileSizeUnit.Kibibytes, "KiB")]
    [InlineData(FileSizeUnit.Mebibytes, "MiB")]
    [InlineData(FileSizeUnit.Gibibytes, "GiB")]
    [InlineData(FileSizeUnit.Tebibytes, "TiB")]
    [InlineData(FileSizeUnit.Pebibytes, "PiB")]
    public void FormatSize_AllUnits_CorrectSuffix(FileSizeUnit unit, string expectedSuffix)
    {
        string result = _sut.FormatSize(1.0, unit);
        result.Should().EndWith(expectedSuffix);
    }
}
