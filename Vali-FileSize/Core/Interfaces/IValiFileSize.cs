using System.Globalization;
using ValiFileSize.Core.Enums;

namespace ValiFileSize.Core.Interfaces;

/// <summary>
/// Defines operations for converting and formatting file sizes.
/// Register this interface in your DI container to decouple your code from the concrete implementation.
/// </summary>
public interface IValiFileSize
{
    /// <summary>
    /// Converts a file size from one unit to another.
    /// </summary>
    /// <param name="size">The size value to convert. Must be ≥ 0.</param>
    /// <param name="fromUnit">The unit of the input value.</param>
    /// <param name="toUnit">The desired output unit.</param>
    /// <returns>The converted value expressed in <paramref name="toUnit"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="size"/> is negative.</exception>
    /// <exception cref="NotSupportedException">Thrown when an unrecognised unit is supplied.</exception>
    double Convert(double size, FileSizeUnit fromUnit, FileSizeUnit toUnit);

    /// <summary>
    /// Formats a size value as a human-readable string.
    /// </summary>
    /// <param name="size">The size value to format.</param>
    /// <param name="unit">The unit that <paramref name="size"/> is expressed in.</param>
    /// <param name="decimalPlaces">Number of decimal places (default 2). Must be ≥ 0.</param>
    /// <param name="culture">Culture used for number formatting (defaults to <see cref="CultureInfo.CurrentCulture"/>).</param>
    /// <returns>A string such as <c>"1.23 MB"</c> or <c>"512.00 KiB"</c>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="decimalPlaces"/> is negative.</exception>
    /// <exception cref="NotSupportedException">Thrown when an unrecognised unit is supplied.</exception>
    string FormatSize(double size, FileSizeUnit unit, int decimalPlaces = 2, CultureInfo? culture = null);

    /// <summary>
    /// Selects the most appropriate unit for the given byte count and returns the converted value.
    /// </summary>
    /// <param name="bytes">The size in bytes. Must be ≥ 0.</param>
    /// <param name="useIec">
    /// When <see langword="true"/> returns IEC units (KiB, MiB, …);
    /// when <see langword="false"/> (default) returns traditional units (KB, MB, …).
    /// </param>
    /// <returns>A tuple of the converted value and the selected unit.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="bytes"/> is negative.</exception>
    (double size, FileSizeUnit unit) GetBestUnit(double bytes, bool useIec = false);

    /// <summary>
    /// Converts <paramref name="bytes"/> to the most appropriate unit and returns a formatted string in one call.
    /// Equivalent to calling <see cref="GetBestUnit"/> followed by <see cref="FormatSize"/>.
    /// </summary>
    /// <param name="bytes">The size in bytes. Must be ≥ 0.</param>
    /// <param name="decimalPlaces">Number of decimal places (default 2). Must be ≥ 0.</param>
    /// <param name="culture">Culture used for number formatting (defaults to <see cref="CultureInfo.CurrentCulture"/>).</param>
    /// <param name="useIec">When <see langword="true"/> uses IEC suffixes (KiB, MiB, …).</param>
    /// <returns>A formatted string such as <c>"1.15 GB"</c>.</returns>
    string FormatBestSize(double bytes, int decimalPlaces = 2, CultureInfo? culture = null, bool useIec = false);
}
