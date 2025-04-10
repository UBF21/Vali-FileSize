namespace Vali_FileSize.Core.Enums;

/// <summary>
/// Enumeration representing supported file size units.
/// </summary>
public enum FileSizeUnit
{
    /// <summary>
    /// Represents a size in bytes (B), the smallest unit of digital information.
    /// </summary>
    Bytes,
    
    /// <summary>
    /// Represents a size in kilobytes (KB), equivalent to 1024 bytes.
    /// </summary>
    Kilobytes,
    
    /// <summary>
    /// Represents a size in megabytes (MB), equivalent to 1024 kilobytes.
    /// </summary>
    Megabytes,
    
    /// <summary>
    /// Represents a size in gigabytes (GB), equivalent to 1024 megabytes.
    /// </summary>
    Gigabytes,
    
    /// <summary>
    /// Represents a size in terabytes (TB), equivalent to 1024 gigabytes.
    /// </summary>
    Terabytes,
    
    /// <summary>
    /// Represents a size in petabytes (PB), equivalent to 1024 terabytes.
    /// </summary>
    Petabytes
}