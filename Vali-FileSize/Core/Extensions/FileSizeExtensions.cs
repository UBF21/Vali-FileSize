using System.Globalization;
using ValiFileSize.Core.Enums;

namespace ValiFileSize.Core.Extensions;

/// <summary>
/// Extension methods on <see cref="double"/> and <see cref="long"/> for ergonomic file size formatting.
/// All methods are stateless and use a shared singleton internally.
/// </summary>
public static class FileSizeExtensions
{
    private static readonly Main.ValiFileSize Instance = new();

    /// <summary>
    /// Formats a numeric value as a human-readable file size string using the specified unit.
    /// </summary>
    /// <param name="size">The size value to format.</param>
    /// <param name="unit">The unit that <paramref name="size"/> is expressed in.</param>
    /// <param name="decimalPlaces">Number of decimal places (default 2). Must be ≥ 0.</param>
    /// <param name="culture">Culture used for number formatting (defaults to <see cref="CultureInfo.CurrentCulture"/>).</param>
    /// <returns>A formatted string such as <c>"1.23 MB"</c>.</returns>
    public static string ToFormattedSize(this double size, FileSizeUnit unit, int decimalPlaces = 2, CultureInfo? culture = null)
        => Instance.FormatSize(size, unit, decimalPlaces, culture);

    /// <inheritdoc cref="ToFormattedSize(double, FileSizeUnit, int, CultureInfo?)"/>
    public static string ToFormattedSize(this long size, FileSizeUnit unit, int decimalPlaces = 2, CultureInfo? culture = null)
        => Instance.FormatSize(size, unit, decimalPlaces, culture);

    /// <summary>
    /// Selects the most appropriate unit for the value (treated as bytes) and returns a formatted string.
    /// Equivalent to calling <c>FormatBestSize</c> on a <see cref="Main.ValiFileSize"/> instance.
    /// </summary>
    /// <param name="bytes">The size in bytes. Must be ≥ 0.</param>
    /// <param name="decimalPlaces">Number of decimal places (default 2). Must be ≥ 0.</param>
    /// <param name="culture">Culture used for number formatting (defaults to <see cref="CultureInfo.CurrentCulture"/>).</param>
    /// <param name="useIec">When <see langword="true"/> uses IEC suffixes (KiB, MiB, …).</param>
    /// <returns>A formatted string such as <c>"1.15 GB"</c>.</returns>
    public static string FormatBestSize(this double bytes, int decimalPlaces = 2, CultureInfo? culture = null, bool useIec = false)
        => Instance.FormatBestSize(bytes, decimalPlaces, culture, useIec);

    /// <inheritdoc cref="FormatBestSize(double, int, CultureInfo?, bool)"/>
    public static string FormatBestSize(this long bytes, int decimalPlaces = 2, CultureInfo? culture = null, bool useIec = false)
        => Instance.FormatBestSize(bytes, decimalPlaces, culture, useIec);

    /// <summary>
    /// Selects the most appropriate unit for the value (treated as bytes) and returns the converted size and unit.
    /// </summary>
    /// <param name="bytes">The size in bytes. Must be ≥ 0.</param>
    /// <param name="useIec">When <see langword="true"/> returns IEC units (KiB, MiB, …).</param>
    /// <returns>A tuple of the converted size and the selected unit.</returns>
    public static (double size, FileSizeUnit unit) GetBestUnit(this double bytes, bool useIec = false)
        => Instance.GetBestUnit(bytes, useIec);

    /// <inheritdoc cref="GetBestUnit(double, bool)"/>
    public static (double size, FileSizeUnit unit) GetBestUnit(this long bytes, bool useIec = false)
        => Instance.GetBestUnit(bytes, useIec);
}
