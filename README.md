#  <img src="https://github.com/UBF21/Vali-FileSize/blob/main/Vali-FileSize/logo-vali-filesize.png?raw=true" alt="Logo Vali-FileSize" style="width: 46px; height: 46px; vertical-align: middle;">  Vali-FileSize

> Lightweight .NET library for file size conversion, formatting and automatic unit detection.

[![NuGet](https://img.shields.io/nuget/v/Vali-FileSize)](https://www.nuget.org/packages/Vali-FileSize)
[![License](https://img.shields.io/badge/license-Apache%202.0-blue)](LICENSE.txt)
[![.NET](https://img.shields.io/badge/.NET-Standard%202.0%2B%20%7C%206%20%7C%207%20%7C%208%20%7C%209-purple)](https://dotnet.microsoft.com)

---

## Table of Contents

- [Installation](#installation)
- [Quick Start](#quick-start)
- [API Reference](#api-reference)
  - [Convert](#convert)
  - [FormatSize](#formatsize)
  - [GetBestUnit](#getbestunit)
  - [FormatBestSize](#formatbestsize)
- [Extension Methods](#extension-methods)
- [IEC / Binary Prefixes](#iec--binary-prefixes)
- [Dependency Injection](#dependency-injection)
- [Supported Units](#supported-units)
- [Donations](#donations)
- [License](#license)

---

## Installation

```sh
dotnet add package Vali-FileSize
```

Supports **.NET Standard 2.0 and 2.1**, **.NET 6, 7, 8 and 9**.

| Target | Compatible runtimes |
|--------|-------------------|
| `netstandard2.0` | .NET Framework 4.6.1+, .NET Core 2.0+, .NET 5+ |
| `netstandard2.1` | .NET Core 3.0+, .NET 5+ |
| `net6.0` – `net9.0` | .NET 6, 7, 8, 9 |

---

## Quick Start

```csharp
using ValiFileSize.Core.Main;
using ValiFileSize.Core.Enums;

var fs = new ValiFileSize();

// Convert 2.5 GB → KB
double kb = fs.Convert(2.5, FileSizeUnit.Gigabytes, FileSizeUnit.Kilobytes);
Console.WriteLine(kb); // 2621440

// Format a raw byte count with automatic unit selection
string label = fs.FormatBestSize(1_234_567_890);
Console.WriteLine(label); // "1.15 GB"
```

Or even shorter with **extension methods**:

```csharp
using ValiFileSize.Core.Extensions;

string label = 1_234_567_890L.FormatBestSize(); // "1.15 GB"
```

---

## API Reference

### Convert

Converts a value from one unit to another. All units use the binary base (1 024).

```csharp
double Convert(double size, FileSizeUnit fromUnit, FileSizeUnit toUnit)
```

```csharp
// 1.5 TB → bytes
double bytes = fs.Convert(1.5, FileSizeUnit.Terabytes, FileSizeUnit.Bytes);
// 1 649 267 441 664

// 512 MB → GB
double gb = fs.Convert(512, FileSizeUnit.Megabytes, FileSizeUnit.Gigabytes);
// 0.5

// Exabytes
double eb = fs.Convert(2, FileSizeUnit.Exabytes, FileSizeUnit.Petabytes);
// 2 048

// IEC units follow the same 1 024 base, different display suffix
double b = fs.Convert(1, FileSizeUnit.Mebibytes, FileSizeUnit.Bytes);
// 1 048 576
```

**Exceptions**

| Condition | Exception |
|-----------|-----------|
| `size < 0` | `ArgumentException` |
| Unknown unit | `NotSupportedException` |

---

### FormatSize

Formats a value as a human-readable string.

```csharp
string FormatSize(double size, FileSizeUnit unit, int decimalPlaces = 2, CultureInfo? culture = null)
```

```csharp
fs.FormatSize(1.5, FileSizeUnit.Megabytes);                          // "1.50 MB"
fs.FormatSize(1.23456, FileSizeUnit.Gigabytes, decimalPlaces: 4);   // "1.2346 GB"
fs.FormatSize(512, FileSizeUnit.Bytes, decimalPlaces: 0);            // "512 B"
fs.FormatSize(1.0, FileSizeUnit.Exabytes);                           // "1.00 EB"

// IEC suffix
fs.FormatSize(1.0, FileSizeUnit.Gibibytes);                          // "1.00 GiB"
fs.FormatSize(1.0, FileSizeUnit.Exbibytes);                          // "1.00 EiB"

// Cultural formatting
var de = new CultureInfo("de-DE");
fs.FormatSize(1234.5, FileSizeUnit.Kilobytes, 2, de);               // "1234,50 KB"
```

**Exceptions**

| Condition | Exception |
|-----------|-----------|
| `decimalPlaces < 0` | `ArgumentOutOfRangeException` |
| Unknown unit | `NotSupportedException` |

---

### GetBestUnit

Selects the most appropriate unit for a byte count and returns the converted value.

```csharp
(double size, FileSizeUnit unit) GetBestUnit(double bytes, bool useIec = false)
```

```csharp
var (size, unit) = fs.GetBestUnit(1_234_567_890);
// size = 1.15, unit = FileSizeUnit.Gigabytes

var (size2, unit2) = fs.GetBestUnit(1_048_576, useIec: true);
// size2 = 1.0, unit2 = FileSizeUnit.Mebibytes

double oneEB = 1024.0 * 1024 * 1024 * 1024 * 1024 * 1024;
var (size3, unit3) = fs.GetBestUnit(oneEB);
// size3 = 1.0, unit3 = FileSizeUnit.Exabytes
```

| Range | Returned unit (default) | Returned unit (`useIec: true`) |
|-------|------------------------|-------------------------------|
| < 1 024 B | Bytes | Bytes |
| 1 024 B – 1 MB | Kilobytes | Kibibytes |
| 1 MB – 1 GB | Megabytes | Mebibytes |
| 1 GB – 1 TB | Gigabytes | Gibibytes |
| 1 TB – 1 PB | Terabytes | Tebibytes |
| 1 PB – 1 EB | Petabytes | Pebibytes |
| ≥ 1 EB | Exabytes | Exbibytes |

---

### FormatBestSize

Combines `GetBestUnit` + `FormatSize` in a single call — the most convenient method for displaying raw byte counts.

```csharp
string FormatBestSize(double bytes, int decimalPlaces = 2, CultureInfo? culture = null, bool useIec = false)
```

```csharp
fs.FormatBestSize(0);                                   // "0.00 B"
fs.FormatBestSize(2048);                                // "2.00 KB"
fs.FormatBestSize(1_234_567_890);                       // "1.15 GB"
fs.FormatBestSize(1_234_567_890, decimalPlaces: 4);     // "1.1498 GB"
fs.FormatBestSize(1_048_576, useIec: true);             // "1.00 MiB"

var de = new CultureInfo("de-DE");
fs.FormatBestSize(1_234_567_890, culture: de);          // "1,15 GB"
```

**Before vs. After**

```csharp
// Before — manual, error-prone
double bytes  = 2.5 * 1024 * 1024 * 1024;
string result = $"{bytes / 1024 / 1024 / 1024:F2} GB"; // "2.50 GB"

// After — one line
string result = fs.FormatBestSize(2.5 * 1024 * 1024 * 1024); // "2.50 GB"
```

---

## Extension Methods

Add `using ValiFileSize.Core.Extensions;` to use extension methods directly on `double` and `long` values — no need to instantiate `ValiFileSize` manually.

### `ToFormattedSize`

Formats a known-unit value as a string.

```csharp
1.5.ToFormattedSize(FileSizeUnit.Megabytes);                    // "1.50 MB"
1024L.ToFormattedSize(FileSizeUnit.Kilobytes, decimalPlaces: 0); // "1024 KB"
1.0.ToFormattedSize(FileSizeUnit.Exbibytes);                    // "1.00 EiB"

var de = new CultureInfo("de-DE");
1.5.ToFormattedSize(FileSizeUnit.Megabytes, 2, de);             // "1,50 MB"
```

### `FormatBestSize`

Treats the value as bytes, auto-detects the best unit, and returns a formatted string.

```csharp
512L.FormatBestSize();                       // "512.00 B"
2048L.FormatBestSize();                      // "2.00 KB"
1_234_567_890L.FormatBestSize();             // "1.15 GB"
1_234_567_890L.FormatBestSize(useIec: true); // "1.15 GiB"
```

### `GetBestUnit`

Treats the value as bytes and returns the best unit as a tuple.

```csharp
var (size, unit) = 1_048_576L.GetBestUnit();
// size = 1.0, unit = FileSizeUnit.Megabytes

var (size2, unit2) = 1_048_576L.GetBestUnit(useIec: true);
// size2 = 1.0, unit2 = FileSizeUnit.Mebibytes
```

---

## IEC / Binary Prefixes

The IEC 80000-13 standard distinguishes between **decimal** (SI) and **binary** prefixes.
Vali-FileSize uses **binary (1 024-based)** math for all units.
Pass `useIec: true` to display the unambiguous IEC suffixes (KiB, MiB, GiB…).

```csharp
// Same data, two display conventions
fs.FormatBestSize(1_048_576);               // "1.00 MB"  ← traditional
fs.FormatBestSize(1_048_576, useIec: true); // "1.00 MiB" ← IEC
```

You can also use IEC units explicitly in `Convert` and `FormatSize`:

```csharp
fs.FormatSize(fs.Convert(500, FileSizeUnit.Mebibytes, FileSizeUnit.Gibibytes), FileSizeUnit.Gibibytes, 3);
// "0.488 GiB"
```

---

## Dependency Injection

`ValiFileSize` implements `IValiFileSize`. Register it in your DI container to decouple your code from the concrete class.

```csharp
// ASP.NET Core / Generic Host
builder.Services.AddSingleton<IValiFileSize, ValiFileSize>();
```

```csharp
// Usage via constructor injection
public class FileService(IValiFileSize fileSize)
{
    public string Describe(long byteCount) => fileSize.FormatBestSize(byteCount);
}
```

---

## Supported Units

| Enum value | Suffix | Base |
|------------|--------|------|
| `FileSizeUnit.Bytes` | B | — |
| `FileSizeUnit.Kilobytes` | KB | 1 024 |
| `FileSizeUnit.Megabytes` | MB | 1 024² |
| `FileSizeUnit.Gigabytes` | GB | 1 024³ |
| `FileSizeUnit.Terabytes` | TB | 1 024⁴ |
| `FileSizeUnit.Petabytes` | PB | 1 024⁵ |
| `FileSizeUnit.Exabytes` | EB | 1 024⁶ |
| `FileSizeUnit.Kibibytes` | KiB | 1 024 |
| `FileSizeUnit.Mebibytes` | MiB | 1 024² |
| `FileSizeUnit.Gibibytes` | GiB | 1 024³ |
| `FileSizeUnit.Tebibytes` | TiB | 1 024⁴ |
| `FileSizeUnit.Pebibytes` | PiB | 1 024⁵ |
| `FileSizeUnit.Exbibytes` | EiB | 1 024⁶ |

---

## Donations

If Vali-FileSize is useful to you, consider supporting its development:

- **Latin America** — [MercadoPago](https://link.mercadopago.com.pe/felipermm)
- **International** — [PayPal](https://paypal.me/felipeRMM?country.x=PE&locale.x=es_XC)

---

## License

[Apache License 2.0](https://www.apache.org/licenses/LICENSE-2.0)

## Contributions

Issues and pull requests are welcome on [GitHub](https://github.com/UBF21/Vali-FileSize).
