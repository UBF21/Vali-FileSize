namespace ValiFileSize.Core.Utils;

/// <summary>
/// Static class containing common constants used in file size conversions.
/// </summary>
internal static class Constants
{
    /// <summary>
    /// Represents the number of bytes in a kilobyte (1024).
    /// </summary>
    public const int Kilobyte = 1024;

    // Traditional binary prefixes (KB, MB, GB…)
    public const string PrefixBytes      = "B";
    public const string PrefixKilobytes  = "KB";
    public const string PrefixMegabytes  = "MB";
    public const string PrefixGigabytes  = "GB";
    public const string PrefixTerabytes  = "TB";
    public const string PrefixPetabytes  = "PB";
    public const string PrefixExabytes   = "EB";

    // IEC binary prefixes (KiB, MiB, GiB…)
    public const string PrefixKibibytes  = "KiB";
    public const string PrefixMebibytes  = "MiB";
    public const string PrefixGibibytes  = "GiB";
    public const string PrefixTebibytes  = "TiB";
    public const string PrefixPebibytes  = "PiB";
    public const string PrefixExbibytes  = "EiB";
}
