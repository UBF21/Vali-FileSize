#  <img src="https://github.com/UBF21/Vali-FileSize/blob/main/Vali-FileSize/logo-vali-filesize.png?raw=true" alt="Logo de Vali Mediator" style="width: 46px; height: 46px; max-width: 300px;">  Vali-FileSize - File Size Conversion and Formatting for .NET


## Introduction 🚀

Welcome to **Vali-FileSize** , a lightweight .NET library designed to simplify file size conversions and formatting. Whether you're working with bytes, kilobytes, megabytes, gigabytes, terabytes, or petabytes, **Vali-FileSize** provides a fluent and intuitive API to convert sizes, format them into human-readable strings, and automatically detect the most suitable unit. Perfect for applications requiring precise file size handling, this library integrates seamlessly into any .NET project.

## Installation 📦
To add **Vali-FileSize** to your .NET project, install it via NuGet with the following command:

```sh
dotnet add package Vali-FileSize
```

Ensure your project targets a compatible .NET version (e.g., .NET Standard 2.0 or later). **Vali-FileSize** is lightweight and has minimal dependencies, making it an easy addition to your application.

## Usage 🛠️

**Vali-FileSize** focuses on converting file sizes between units and formatting them for display. The library provides a simple class, **ValiFileSize** , with methods to perform conversions, format sizes with cultural support, and determine the best unit for a given size.

### Basic Example

Here’s how you can convert and format a file size:

```csharp
using Shared.Converters;
using Shared.Enums.Data;

var converter = new ValiFileSize();

// Convert 1.5 terabytes to bytes
double sizeInBytes = converter.Convert(1.5, FileSizeUnit.Terabytes, FileSizeUnit.Bytes);
Console.WriteLine(sizeInBytes); // Outputs: 1,610,612,736,000

// Format a size in megabytes
string formattedSize = converter.FormatSize(123.456, FileSizeUnit.Megabytes);
Console.WriteLine(formattedSize); // Outputs: "123.46 MB"
```

## Key Methods 📝

**Vali-FileSize** offers a straightforward API for file size management. Below are the key methods provided by the **ValiFileSize** class:

### Convert 🏗️

Converts a file size from one unit to another with precision:

```csharp
var converter = new ValiFileSize();

// Convert 2.5 gigabytes to kilobytes
double sizeInKilobytes = converter.Convert(2.5, FileSizeUnit.Gigabytes, FileSizeUnit.Kilobytes);
Console.WriteLine(sizeInKilobytes); // Outputs: 2,621,440
```

### FormatSize 🎨

Formats a size into a human-readable string with customizable decimal places and cultural formatting:

```csharp
var converter = new ValiFileSize();

// Format 1,234,567 bytes as megabytes
string formatted = converter.FormatSize(1234567, FileSizeUnit.Megabytes, decimalPlaces: 3);
Console.WriteLine(formatted); // Outputs: "1.177 MB"
```
### GetBestUnit 🔍

Automatically determines the most appropriate unit for a given size in bytes:

```csharp
var converter = new ValiFileSize();

// Find the best unit for 1,234,567,890 bytes
var (size, unit) = converter.GetBestUnit(1234567890);
string bestFormat = converter.FormatSize(size, unit);
Console.WriteLine(bestFormat); // Outputs: "1.15 GB"
```

## Working with Advanced Features 🧩

**Vali-FileSize** supports advanced use cases like cultural formatting and precise conversions:

### Cultural Formatting

Format sizes according to specific cultures:

```csharp
using System.Globalization;

var converter = new ValiFileSize();
var germanCulture = new CultureInfo("de-DE");

string formatted = converter.FormatSize(1234.567, FileSizeUnit.Kilobytes, 2, germanCulture);
Console.WriteLine(formatted); // Outputs: "1234,57 KB" (uses comma as decimal separator)
```

### Combining Features

Convert, detect the best unit, and format in one flow:

```csharp
var converter = new ValiFileSize();
double bytes = converter.Convert(5.75, FileSizeUnit.Terabytes, FileSizeUnit.Bytes);

var (size, unit) = converter.GetBestUnit(bytes);
string result = converter.FormatSize(size, unit);

Console.WriteLine(result); // Outputs: "5.75 TB"
```

## Comparison: Without vs. With Vali-FileSize ⚖️

### Without Vali-FileSize (Manual Conversion)

Manually handling file size conversions can be tedious and error-prone:

```csharp
double bytes = 2.5 * 1024 * 1024 * 1024; // GB to bytes
double kilobytes = bytes / 1024;
Console.WriteLine($"{kilobytes:F2} KB"); // Outputs: "2621440.00 KB"
```

### With Vali-FileSize (Simplified Conversion)

**Vali-FileSize** streamlines the process with a clean API:

```csharp
var converter = new ValiFileSize();
double kilobytes = converter.Convert(2.5, FileSizeUnit.Gigabytes, FileSizeUnit.Kilobytes);
string formatted = converter.FormatSize(kilobytes, FileSizeUnit.Kilobytes);
Console.WriteLine(formatted); // Outputs: "2621440.00 KB"
```
## Features and Enhancements 🌟

### Recent Updates

- Initial release (v1.0.0) with support for conversions across bytes, KB, MB, GB, TB, and PB.
- Added **FormatSize** method with customizable decimal precision and cultural number formatting.
- Introduced **GetBestUnit** for automatic unit selection, improving usability.
- Ensured robust validation with negative size checks and comprehensive exception handling.

### Planned Features

- Support for additional units like exabytes (EB) and beyond.
- Enhanced formatting options, such as binary prefixes (KiB, MiB, etc.) for IEC standards.

Follow the project on GitHub for updates on new features and improvements!

## Donations 💖
If you find **Vali-FileSize** useful and would like to support its development, consider making a donation:

- **For Latin America**: [Donate via MercadoPago](https://link.mercadopago.com.pe/felipermm)
- **For International Donations**: [Donate via PayPal](https://paypal.me/felipeRMM?country.x=PE&locale.x=es_XC)


Your contributions help keep this project alive and improve its development! 🚀

## License 📜
This project is licensed under the [Apache License 2.0](https://www.apache.org/licenses/LICENSE-2.0).

## Contributions 🤝
Feel free to open issues and submit pull requests to improve this library!
