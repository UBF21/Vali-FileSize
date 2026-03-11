using System.Globalization;
using ValiFileSize.Core.Enums;
using ValiFileSize.Core.Interfaces;
using ValiFileSize.Core.Utils;

namespace ValiFileSize.Core.Main;

/// <summary>
/// Default implementation of <see cref="IValiFileSize"/>.
/// Stateless — safe to register as a singleton in dependency injection containers.
/// </summary>
public sealed class ValiFileSize : IValiFileSize
{
    private const double BytesInKilobyte = Constants.Kilobyte;
    private const double BytesInMegabyte = BytesInKilobyte * Constants.Kilobyte;
    private const double BytesInGigabyte = BytesInMegabyte * Constants.Kilobyte;
    private const double BytesInTerabyte = BytesInGigabyte * Constants.Kilobyte;
    private const double BytesInPetabyte = BytesInTerabyte * Constants.Kilobyte;
    private const double BytesInExabyte  = BytesInPetabyte * Constants.Kilobyte;

    /// <inheritdoc/>
    public double Convert(double size, FileSizeUnit fromUnit, FileSizeUnit toUnit)
    {
        if (size < 0) throw new ArgumentException("The size cannot be negative.", nameof(size));

        double bytes = fromUnit switch
        {
            FileSizeUnit.Bytes      => size,
            FileSizeUnit.Kilobytes  => size * BytesInKilobyte,
            FileSizeUnit.Megabytes  => size * BytesInMegabyte,
            FileSizeUnit.Gigabytes  => size * BytesInGigabyte,
            FileSizeUnit.Terabytes  => size * BytesInTerabyte,
            FileSizeUnit.Petabytes  => size * BytesInPetabyte,
            FileSizeUnit.Exabytes   => size * BytesInExabyte,
            FileSizeUnit.Kibibytes  => size * BytesInKilobyte,
            FileSizeUnit.Mebibytes  => size * BytesInMegabyte,
            FileSizeUnit.Gibibytes  => size * BytesInGigabyte,
            FileSizeUnit.Tebibytes  => size * BytesInTerabyte,
            FileSizeUnit.Pebibytes  => size * BytesInPetabyte,
            FileSizeUnit.Exbibytes  => size * BytesInExabyte,
            _ => throw new NotSupportedException($"Source unit '{fromUnit}' is not supported.")
        };

        return toUnit switch
        {
            FileSizeUnit.Bytes      => bytes,
            FileSizeUnit.Kilobytes  => bytes / BytesInKilobyte,
            FileSizeUnit.Megabytes  => bytes / BytesInMegabyte,
            FileSizeUnit.Gigabytes  => bytes / BytesInGigabyte,
            FileSizeUnit.Terabytes  => bytes / BytesInTerabyte,
            FileSizeUnit.Petabytes  => bytes / BytesInPetabyte,
            FileSizeUnit.Exabytes   => bytes / BytesInExabyte,
            FileSizeUnit.Kibibytes  => bytes / BytesInKilobyte,
            FileSizeUnit.Mebibytes  => bytes / BytesInMegabyte,
            FileSizeUnit.Gibibytes  => bytes / BytesInGigabyte,
            FileSizeUnit.Tebibytes  => bytes / BytesInTerabyte,
            FileSizeUnit.Pebibytes  => bytes / BytesInPetabyte,
            FileSizeUnit.Exbibytes  => bytes / BytesInExabyte,
            _ => throw new NotSupportedException($"Target unit '{toUnit}' is not supported.")
        };
    }

    /// <inheritdoc/>
    public string FormatSize(double size, FileSizeUnit unit, int decimalPlaces = 2, CultureInfo? culture = null)
    {
        if (decimalPlaces < 0)
            throw new ArgumentOutOfRangeException(nameof(decimalPlaces), "Decimal places cannot be negative.");

        culture ??= CultureInfo.CurrentCulture;

        string suffix = unit switch
        {
            FileSizeUnit.Bytes      => Constants.PrefixBytes,
            FileSizeUnit.Kilobytes  => Constants.PrefixKilobytes,
            FileSizeUnit.Megabytes  => Constants.PrefixMegabytes,
            FileSizeUnit.Gigabytes  => Constants.PrefixGigabytes,
            FileSizeUnit.Terabytes  => Constants.PrefixTerabytes,
            FileSizeUnit.Petabytes  => Constants.PrefixPetabytes,
            FileSizeUnit.Exabytes   => Constants.PrefixExabytes,
            FileSizeUnit.Kibibytes  => Constants.PrefixKibibytes,
            FileSizeUnit.Mebibytes  => Constants.PrefixMebibytes,
            FileSizeUnit.Gibibytes  => Constants.PrefixGibibytes,
            FileSizeUnit.Tebibytes  => Constants.PrefixTebibytes,
            FileSizeUnit.Pebibytes  => Constants.PrefixPebibytes,
            FileSizeUnit.Exbibytes  => Constants.PrefixExbibytes,
            _ => throw new NotSupportedException($"Unit '{unit}' is not supported.")
        };

        return $"{size.ToString($"F{decimalPlaces}", culture)} {suffix}";
    }

    /// <inheritdoc/>
    public (double size, FileSizeUnit unit) GetBestUnit(double bytes, bool useIec = false)
    {
        if (bytes < 0) throw new ArgumentException("The size cannot be negative.", nameof(bytes));

        if (useIec)
        {
            if (bytes >= BytesInExabyte)  return (bytes / BytesInExabyte,  FileSizeUnit.Exbibytes);
            if (bytes >= BytesInPetabyte) return (bytes / BytesInPetabyte, FileSizeUnit.Pebibytes);
            if (bytes >= BytesInTerabyte) return (bytes / BytesInTerabyte, FileSizeUnit.Tebibytes);
            if (bytes >= BytesInGigabyte) return (bytes / BytesInGigabyte, FileSizeUnit.Gibibytes);
            if (bytes >= BytesInMegabyte) return (bytes / BytesInMegabyte, FileSizeUnit.Mebibytes);
            if (bytes >= BytesInKilobyte) return (bytes / BytesInKilobyte, FileSizeUnit.Kibibytes);
            return (bytes, FileSizeUnit.Bytes);
        }

        if (bytes >= BytesInExabyte)  return (bytes / BytesInExabyte,  FileSizeUnit.Exabytes);
        if (bytes >= BytesInPetabyte) return (bytes / BytesInPetabyte, FileSizeUnit.Petabytes);
        if (bytes >= BytesInTerabyte) return (bytes / BytesInTerabyte, FileSizeUnit.Terabytes);
        if (bytes >= BytesInGigabyte) return (bytes / BytesInGigabyte, FileSizeUnit.Gigabytes);
        if (bytes >= BytesInMegabyte) return (bytes / BytesInMegabyte, FileSizeUnit.Megabytes);
        if (bytes >= BytesInKilobyte) return (bytes / BytesInKilobyte, FileSizeUnit.Kilobytes);
        return (bytes, FileSizeUnit.Bytes);
    }

    /// <inheritdoc/>
    public string FormatBestSize(double bytes, int decimalPlaces = 2, CultureInfo? culture = null, bool useIec = false)
    {
        var (size, unit) = GetBestUnit(bytes, useIec);
        return FormatSize(size, unit, decimalPlaces, culture);
    }
}
