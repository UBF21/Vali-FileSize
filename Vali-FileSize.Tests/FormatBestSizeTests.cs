using System.Globalization;

namespace Vali_FileSize.Tests;

public class FormatBestSizeTests
{
    private readonly FileSizeConverter _sut = new();

    [Fact]
    public void FormatBestSize_GigabyteRange_ReturnsFormattedGB()
    {
        string result = _sut.FormatBestSize(1_234_567_890);
        result.Should().Be("1.15 GB");
    }

    [Fact]
    public void FormatBestSize_ByteRange_ReturnsFormattedBytes()
    {
        string result = _sut.FormatBestSize(512);
        result.Should().Be("512.00 B");
    }

    [Fact]
    public void FormatBestSize_KilobyteRange_ReturnsFormattedKB()
    {
        string result = _sut.FormatBestSize(2048);
        result.Should().Be("2.00 KB");
    }

    [Fact]
    public void FormatBestSize_UseIec_ReturnsIecSuffix()
    {
        string result = _sut.FormatBestSize(1024 * 1024, useIec: true);
        result.Should().Be("1.00 MiB");
    }

    [Fact]
    public void FormatBestSize_CustomDecimalPlaces_ReturnsCorrectPrecision()
    {
        string result = _sut.FormatBestSize(1_234_567_890, decimalPlaces: 4);
        result.Should().Be("1.1498 GB");
    }

    [Fact]
    public void FormatBestSize_GermanCulture_UsesCommaAsSeparator()
    {
        var culture = new CultureInfo("de-DE");
        string result = _sut.FormatBestSize(1_234_567_890, culture: culture);
        result.Should().Be("1,15 GB");
    }

    [Fact]
    public void FormatBestSize_Zero_ReturnsZeroBytes()
    {
        string result = _sut.FormatBestSize(0);
        result.Should().Be("0.00 B");
    }
}
