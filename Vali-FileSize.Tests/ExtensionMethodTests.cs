using System.Globalization;
using ValiFileSize.Core.Extensions;

namespace Vali_FileSize.Tests;

public class ExtensionMethodTests
{
    // ── ToFormattedSize ──────────────────────────────────────────────────────

    [Fact]
    public void ToFormattedSize_Double_FormatsWithCorrectSuffix()
    {
        string result = 1.5.ToFormattedSize(FileSizeUnit.Megabytes);
        result.Should().Be("1.50 MB");
    }

    [Fact]
    public void ToFormattedSize_Long_FormatsWithCorrectSuffix()
    {
        string result = 1024L.ToFormattedSize(FileSizeUnit.Kilobytes);
        result.Should().Be("1024.00 KB");
    }

    [Fact]
    public void ToFormattedSize_IecUnit_ReturnsIecSuffix()
    {
        string result = 1.0.ToFormattedSize(FileSizeUnit.Gibibytes);
        result.Should().Be("1.00 GiB");
    }

    [Fact]
    public void ToFormattedSize_CustomDecimalPlaces_ReturnsCorrectPrecision()
    {
        string result = 1.23456.ToFormattedSize(FileSizeUnit.Gigabytes, decimalPlaces: 3);
        result.Should().Be("1.235 GB");
    }

    [Fact]
    public void ToFormattedSize_GermanCulture_UsesCommaAsSeparator()
    {
        var de = new CultureInfo("de-DE");
        string result = 1.5.ToFormattedSize(FileSizeUnit.Megabytes, 2, de);
        result.Should().Be("1,50 MB");
    }

    // ── FormatBestSize ───────────────────────────────────────────────────────

    [Fact]
    public void FormatBestSize_Double_SelectsBestUnit()
    {
        string result = 1_234_567_890.0.FormatBestSize();
        result.Should().Be("1.15 GB");
    }

    [Fact]
    public void FormatBestSize_Long_SelectsBestUnit()
    {
        string result = 2048L.FormatBestSize();
        result.Should().Be("2.00 KB");
    }

    [Fact]
    public void FormatBestSize_Long_UseIec_ReturnsIecSuffix()
    {
        string result = (1024L * 1024).FormatBestSize(useIec: true);
        result.Should().Be("1.00 MiB");
    }

    // ── GetBestUnit ──────────────────────────────────────────────────────────

    [Fact]
    public void GetBestUnit_Double_ReturnsCorrectUnit()
    {
        var (_, unit) = (1024.0 * 1024).GetBestUnit();
        unit.Should().Be(FileSizeUnit.Megabytes);
    }

    [Fact]
    public void GetBestUnit_Long_ReturnsCorrectUnit()
    {
        var (size, unit) = 1024L.GetBestUnit();
        unit.Should().Be(FileSizeUnit.Kilobytes);
        size.Should().Be(1.0);
    }

    [Fact]
    public void GetBestUnit_Long_UseIec_ReturnsIecUnit()
    {
        var (_, unit) = (1024L * 1024 * 1024).GetBestUnit(useIec: true);
        unit.Should().Be(FileSizeUnit.Gibibytes);
    }
}
