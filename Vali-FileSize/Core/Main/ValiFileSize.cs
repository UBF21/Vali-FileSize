using System.Globalization;
using Vali_FileSize.Core.Enums;
using Vali_FileSize.Core.Utils;

namespace Vali_FileSize.Core.Main;

/// <summary>
/// A helper class for converting file sizes between different units.
/// </summary>
public class ValiFileSize
{
    #region Constants
    
    /// <summary>
    /// Represents the number of bytes in a kilobyte.
    /// </summary>
    private const double BytesInKilobyte = Constants.Kilobyte;
    
    /// <summary>
    /// Represents the number of bytes in a megabyte.
    /// </summary>
    private const double BytesInMegabyte = BytesInKilobyte * Constants.Kilobyte;
    
    /// <summary>
    /// Represents the number of bytes in a gigabyte.
    /// </summary>
    private const double BytesInGigabyte = BytesInMegabyte * Constants.Kilobyte;
    
    /// <summary>
    /// Represents the number of bytes in a terabyte.
    /// </summary>
    private const double BytesInTerabyte = BytesInGigabyte * Constants.Kilobyte;
    
    /// <summary>
    /// Represents the number of bytes in a petabyte.
    /// </summary>
    private const double BytesInPetabyte = BytesInTerabyte * Constants.Kilobyte;
    
    #endregion

    #region Conversion Methods
    
    /// <summary>
    /// Converts a file size from one unit to another.
    /// </summary>
    /// <param name="size">The size to convert.</param>
    /// <param name="fromUnit">The source unit of the size.</param>
    /// <param name="toUnit">The target unit to convert the size to.</param>
    /// <returns>The converted size in the target unit.</returns>
    /// <exception cref="ArgumentException">Thrown if the size is negative.</exception>
    /// <exception cref="NotSupportedException">Thrown if an unsupported unit is provided.</exception>
    public double Convert(double size, FileSizeUnit fromUnit, FileSizeUnit toUnit)
    {
        if (size < Constants.Zero) throw new ArgumentException("The size cannot be negative.", nameof(size));

        double sizeInBytes = fromUnit switch
        {
            FileSizeUnit.Bytes => size,
            FileSizeUnit.Kilobytes => KilobytesToBytes(size),
            FileSizeUnit.Megabytes => MegabytesToBytes(size),
            FileSizeUnit.Gigabytes => GigabytesToBytes(size),
            FileSizeUnit.Terabytes => TerabytesToBytes(size),
            FileSizeUnit.Petabytes => PetabytesToBytes(size),
            _ => throw new NotSupportedException("Source unit not supported.")
        };

        return toUnit switch
        {
            FileSizeUnit.Bytes => sizeInBytes,
            FileSizeUnit.Kilobytes => BytesToKilobytes(sizeInBytes),
            FileSizeUnit.Megabytes => BytesToMegabytes(sizeInBytes),
            FileSizeUnit.Gigabytes => BytesToGigabytes(sizeInBytes),
            FileSizeUnit.Terabytes => BytesToTerabytes(sizeInBytes),
            FileSizeUnit.Petabytes => BytesToPetabytes(sizeInBytes),
            _ => throw new NotSupportedException("Source unit not supported.")
        };
    }
    
    #endregion
    
    #region Formatting Methods
    
    /// <summary>
    /// Formats a file size as a human-readable string based on the specified unit.
    /// </summary>
    /// <param name="size">The size to format.</param>
    /// <param name="unit">The unit in which the size will be expressed.</param>
    /// <param name="decimalPlaces">The number of decimal places to display (default is 2).</param>
    /// <param name="culture">The culture to use for numeric formatting (optional, defaults to current culture).</param>
    /// <returns>A formatted string representing the size (e.g., "1.23 MB").</returns>
    /// <exception cref="NotSupportedException">Thrown if an unsupported unit is provided.</exception>
    public string FormatSize(double size, FileSizeUnit unit, int decimalPlaces = Constants.Two, CultureInfo? culture = null)
    {
        culture ??= CultureInfo.CurrentCulture;
        string suffix = unit switch
        {
            FileSizeUnit.Bytes => Constants.PrefixBytes,
            FileSizeUnit.Kilobytes => Constants.PrefixKilobytes,
            FileSizeUnit.Megabytes => Constants.PrefixMegabytes,
            FileSizeUnit.Gigabytes => Constants.PrefixGigabytes,
            FileSizeUnit.Terabytes => Constants.PrefixTerabytes,
            FileSizeUnit.Petabytes => Constants.PrefixPetabytes,
            _ => throw new NotSupportedException("Unit not supported.")
        };
        return $"{size.ToString($"F{decimalPlaces}", culture)} {suffix}";
    }
    
    /// <summary>
    /// Determines the most appropriate unit for a given size in bytes and returns the converted value along with the unit.
    /// </summary>
    /// <param name="bytes">The size in bytes.</param>
    /// <returns>A tuple containing the converted size and the most appropriate unit.</returns>
    /// <exception cref="ArgumentException">Thrown if the size is negative.</exception>
    public (double size, FileSizeUnit unit) GetBestUnit(double bytes)
    {
        if (bytes < Constants.Zero) throw new ArgumentException("The size cannot be negative.", nameof(bytes));

        if (bytes >= BytesInPetabyte) return (BytesToPetabytes(bytes), FileSizeUnit.Petabytes);
        if (bytes >= BytesInTerabyte) return (BytesToTerabytes(bytes), FileSizeUnit.Terabytes);
        if (bytes >= BytesInGigabyte) return (BytesToGigabytes(bytes), FileSizeUnit.Gigabytes);
        if (bytes >= BytesInMegabyte) return (BytesToMegabytes(bytes), FileSizeUnit.Megabytes);
        if (bytes >= BytesInKilobyte) return (BytesToKilobytes(bytes), FileSizeUnit.Kilobytes);
        return (bytes, FileSizeUnit.Bytes);
    }
    
    #endregion

    #region Private Conversions
    
    /// <summary>
    /// Converts a size in bytes to kilobytes.
    /// </summary>
    /// <param name="bytes">The size in bytes to convert.</param>
    /// <returns>The size in kilobytes.</returns>
    private double BytesToKilobytes(double bytes) => bytes / BytesInKilobyte;
    
    /// <summary>
    /// Converts a size in bytes to megabytes.
    /// </summary>
    /// <param name="bytes">The size in bytes to convert.</param>
    /// <returns>The size in megabytes.</returns>
    private double BytesToMegabytes(double bytes) => bytes / BytesInMegabyte;
    
    /// <summary>
    /// Converts a size in bytes to gigabytes.
    /// </summary>
    /// <param name="bytes">The size in bytes to convert.</param>
    /// <returns>The size in gigabytes.</returns>
    private double BytesToGigabytes(double bytes) => bytes / BytesInGigabyte;
    
    /// <summary>
    /// Converts a size in bytes to terabytes.
    /// </summary>
    /// <param name="bytes">The size in bytes to convert.</param>
    /// <returns>The size in terabytes.</returns>
    private double BytesToTerabytes(double bytes) => bytes / BytesInTerabyte;
    
    /// <summary>
    /// Converts a size in bytes to petabytes.
    /// </summary>
    /// <param name="bytes">The size in bytes to convert.</param>
    /// <returns>The size in petabytes.</returns>
    private double BytesToPetabytes(double bytes) => bytes / BytesInPetabyte;
    
    /// <summary>
    /// Converts a size in kilobytes to bytes.
    /// </summary>
    /// <param name="kilobytes">The size in kilobytes to convert.</param>
    /// <returns>The size in bytes.</returns>
    private double KilobytesToBytes(double kilobytes) => kilobytes * BytesInKilobyte;
    
    /// <summary>
    /// Converts a size in megabytes to bytes.
    /// </summary>
    /// <param name="megabytes">The size in megabytes to convert.</param>
    /// <returns>The size in bytes.</returns>
    private double MegabytesToBytes(double megabytes) => megabytes * BytesInMegabyte;
    
    /// <summary>
    /// Converts a size in gigabytes to bytes.
    /// </summary>
    /// <param name="gigabytes">The size in gigabytes to convert.</param>
    /// <returns>The size in bytes.</returns>
    private double GigabytesToBytes(double gigabytes) => gigabytes * BytesInGigabyte;
    
    /// <summary>
    /// Converts a size in terabytes to bytes.
    /// </summary>
    /// <param name="terabytes">The size in terabytes to convert.</param>
    /// <returns>The size in bytes.</returns>
    private double TerabytesToBytes(double terabytes) => terabytes * BytesInTerabyte;
    
    /// <summary>
    /// Converts a size in petabytes to bytes.
    /// </summary>
    /// <param name="petabytes">The size in petabytes to convert.</param>
    /// <returns>The size in bytes.</returns>
    private double PetabytesToBytes(double petabytes) => petabytes * BytesInPetabyte;

    #endregion
}