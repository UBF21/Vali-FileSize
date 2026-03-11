namespace Vali_FileSize.Tests;

public class GetBestUnitTests
{
    private readonly FileSizeConverter _sut = new();

    [Fact]
    public void GetBestUnit_ZeroBytes_ReturnsBytes()
    {
        var (_, unit) = _sut.GetBestUnit(0);
        unit.Should().Be(FileSizeUnit.Bytes);
    }

    [Fact]
    public void GetBestUnit_BelowKilobyte_ReturnsBytes()
    {
        var (_, unit) = _sut.GetBestUnit(512);
        unit.Should().Be(FileSizeUnit.Bytes);
    }

    [Fact]
    public void GetBestUnit_ExactlyOneKilobyte_ReturnsKilobytes()
    {
        var (size, unit) = _sut.GetBestUnit(1024);
        unit.Should().Be(FileSizeUnit.Kilobytes);
        size.Should().Be(1.0);
    }

    [Fact]
    public void GetBestUnit_MegabyteRange_ReturnsMegabytes()
    {
        var (_, unit) = _sut.GetBestUnit(1024 * 1024);
        unit.Should().Be(FileSizeUnit.Megabytes);
    }

    [Fact]
    public void GetBestUnit_GigabyteRange_ReturnsGigabytes()
    {
        var (_, unit) = _sut.GetBestUnit(1_234_567_890);
        unit.Should().Be(FileSizeUnit.Gigabytes);
    }

    [Fact]
    public void GetBestUnit_TerabyteRange_ReturnsTerabytes()
    {
        double oneTerabyte = 1024.0 * 1024 * 1024 * 1024;
        var (size, unit) = _sut.GetBestUnit(oneTerabyte);
        unit.Should().Be(FileSizeUnit.Terabytes);
        size.Should().Be(1.0);
    }

    [Fact]
    public void GetBestUnit_PetabyteRange_ReturnsPetabytes()
    {
        double onePetabyte = 1024.0 * 1024 * 1024 * 1024 * 1024;
        var (size, unit) = _sut.GetBestUnit(onePetabyte);
        unit.Should().Be(FileSizeUnit.Petabytes);
        size.Should().Be(1.0);
    }

    [Fact]
    public void GetBestUnit_NegativeValue_ThrowsArgumentException()
    {
        Action act = () => _sut.GetBestUnit(-1);
        act.Should().Throw<ArgumentException>().WithParameterName("bytes");
    }

    [Fact]
    public void GetBestUnit_UseIec_ReturnsMebibytes()
    {
        var (_, unit) = _sut.GetBestUnit(1024 * 1024, useIec: true);
        unit.Should().Be(FileSizeUnit.Mebibytes);
    }

    [Fact]
    public void GetBestUnit_UseIec_BelowKilobyte_ReturnsBytes()
    {
        var (_, unit) = _sut.GetBestUnit(512, useIec: true);
        unit.Should().Be(FileSizeUnit.Bytes);
    }
}
