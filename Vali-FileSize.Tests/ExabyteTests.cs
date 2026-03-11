namespace Vali_FileSize.Tests;

public class ExabyteTests
{
    private readonly FileSizeConverter _sut = new();

    private const double BytesInExabyte = 1024.0 * 1024 * 1024 * 1024 * 1024 * 1024;

    // ── Convert ──────────────────────────────────────────────────────────────

    [Fact]
    public void Convert_ExabytesToBytes_ReturnsCorrectValue()
    {
        double result = _sut.Convert(1, FileSizeUnit.Exabytes, FileSizeUnit.Bytes);
        result.Should().Be(BytesInExabyte);
    }

    [Fact]
    public void Convert_BytesToExabytes_ReturnsCorrectValue()
    {
        double result = _sut.Convert(BytesInExabyte, FileSizeUnit.Bytes, FileSizeUnit.Exabytes);
        result.Should().Be(1.0);
    }

    [Fact]
    public void Convert_ExbibytesToBytes_SameAsExabytes()
    {
        double eb  = _sut.Convert(1, FileSizeUnit.Exabytes,  FileSizeUnit.Bytes);
        double eib = _sut.Convert(1, FileSizeUnit.Exbibytes, FileSizeUnit.Bytes);
        eb.Should().Be(eib);
    }

    // ── FormatSize ───────────────────────────────────────────────────────────

    [Fact]
    public void FormatSize_Exabytes_ReturnsEBSuffix()
    {
        string result = _sut.FormatSize(1.0, FileSizeUnit.Exabytes);
        result.Should().Be("1.00 EB");
    }

    [Fact]
    public void FormatSize_Exbibytes_ReturnsEiBSuffix()
    {
        string result = _sut.FormatSize(1.0, FileSizeUnit.Exbibytes);
        result.Should().Be("1.00 EiB");
    }

    // ── GetBestUnit ──────────────────────────────────────────────────────────

    [Fact]
    public void GetBestUnit_ExabyteRange_ReturnsExabytes()
    {
        var (size, unit) = _sut.GetBestUnit(BytesInExabyte);
        unit.Should().Be(FileSizeUnit.Exabytes);
        size.Should().Be(1.0);
    }

    [Fact]
    public void GetBestUnit_ExabyteRange_UseIec_ReturnsExbibytes()
    {
        var (size, unit) = _sut.GetBestUnit(BytesInExabyte, useIec: true);
        unit.Should().Be(FileSizeUnit.Exbibytes);
        size.Should().Be(1.0);
    }

    // ── FormatBestSize ───────────────────────────────────────────────────────

    [Fact]
    public void FormatBestSize_ExabyteRange_ReturnsFormattedEB()
    {
        string result = _sut.FormatBestSize(BytesInExabyte);
        result.Should().Be("1.00 EB");
    }

    [Fact]
    public void FormatBestSize_ExabyteRange_UseIec_ReturnsFormattedEiB()
    {
        string result = _sut.FormatBestSize(BytesInExabyte, useIec: true);
        result.Should().Be("1.00 EiB");
    }
}
