namespace ValiFileSize.Core.Enums;

/// <summary>
/// Enumeration representing supported file size units.
/// </summary>
public enum FileSizeUnit
{
    // ── Traditional binary units (display: KB / MB / GB…) ──────────────────

    /// <summary>Bytes (B) — base unit.</summary>
    Bytes,

    /// <summary>Kilobytes (KB) — 1 024 bytes.</summary>
    Kilobytes,

    /// <summary>Megabytes (MB) — 1 024 KB.</summary>
    Megabytes,

    /// <summary>Gigabytes (GB) — 1 024 MB.</summary>
    Gigabytes,

    /// <summary>Terabytes (TB) — 1 024 GB.</summary>
    Terabytes,

    /// <summary>Petabytes (PB) — 1 024 TB.</summary>
    Petabytes,

    /// <summary>Exabytes (EB) — 1 024 PB.</summary>
    Exabytes,

    // ── IEC binary units (display: KiB / MiB / GiB…) ───────────────────────
    // Same 1 024-based math; different suffix to follow the IEC 80000-13 standard.

    /// <summary>Kibibytes (KiB) — 1 024 bytes.</summary>
    Kibibytes,

    /// <summary>Mebibytes (MiB) — 1 024 KiB.</summary>
    Mebibytes,

    /// <summary>Gibibytes (GiB) — 1 024 MiB.</summary>
    Gibibytes,

    /// <summary>Tebibytes (TiB) — 1 024 GiB.</summary>
    Tebibytes,

    /// <summary>Pebibytes (PiB) — 1 024 TiB.</summary>
    Pebibytes,

    /// <summary>Exbibytes (EiB) — 1 024 PiB.</summary>
    Exbibytes
}
