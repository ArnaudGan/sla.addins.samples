# SLA Addins Samples

Sample Addins for SLA - A comprehensive collection of example addins demonstrating the SLA extension API.

## Overview

This repository contains sample addins for SLA (a log viewer application) that demonstrate how to extend its functionality using the `Arga.Sla.Addins.Core` NuGet package.

## Project Structure

```
src/
├── SLA.Addins.Samples.FilterAddin/     # Filter and explorer samples
├── SLA.Addins.Samples.ParserAddin/     # Parser and translator samples
└── SLA.Addins.Samples.ExportAddin/     # Visualizer and controller samples
```

## Sample Addins

### 1. Filter Addins (`SLA.Addins.Samples.FilterAddin`)

#### KeywordFilterTranslator
- **Type**: `ILineTranslator`
- **Purpose**: Filters or highlights lines containing specific keywords
- **Use Case**: Remove debug/trace lines or highlight important patterns

#### LogFileExplorerFilter
- **Type**: `IExplorerItemFilter`
- **Purpose**: Filters the file explorer to show only log files (.log, .txt, .out)
- **Use Case**: Simplify navigation by hiding non-log files

### 2. Parser Addins (`SLA.Addins.Samples.ParserAddin`)

#### CommonLogDateTimeParser
- **Type**: `IDateTimeParser`
- **Purpose**: Parses common date/time formats found in log files
- **Supported Formats**: 
  - `yyyy-MM-dd HH:mm:ss`
  - `yyyy-MM-dd HH:mm:ss.fff`
  - ISO 8601 formats
  - And more...

#### JsonLogLineTranslator
- **Type**: `ILineTranslator`
- **Purpose**: Detects and reformats JSON fragments in log lines for better readability
- **Use Case**: Make JSON in mixed-format logs more readable

### 3. Visualizer & Controller Addins (`SLA.Addins.Samples.ExportAddin`)

#### JsonVisualizer
- **Type**: `IVisualizer`
- **Purpose**: Displays JSON content with syntax highlighting and formatting
- **Use Case**: View JSON logs in a structured, readable format

#### StackTraceVisualizer
- **Type**: `IVisualizer`
- **Purpose**: Displays stack traces with improved formatting
- **Use Case**: Better readability of exception stack traces with color coding

#### AutoRefreshController
- **Type**: `ISlaController`
- **Purpose**: Demonstrates programmatic control of SLA (auto-refresh functionality)
- **Use Case**: Automation, batch processing, or integration scenarios

## Building the Project

### Prerequisites
- .NET 8.0 SDK or later
- Windows OS (required for WPF dependencies)
- Visual Studio 2022 or JetBrains Rider (recommended)

### Build Instructions

```bash
# Clone the repository
git clone https://github.com/ArnaudGan/sla.addins.samples.git
cd sla.addins.samples

# Restore NuGet packages
dotnet restore

# Build the solution
dotnet build

# Build in Release mode
dotnet build -c Release
```

## Using the Addins

1. Build the solution in Release mode
2. Locate the compiled DLLs in `src/*/bin/Release/net8.0-windows/`
3. Copy the addin DLLs to the SLA addins directory
4. Restart SLA to load the new addins

## Available Interfaces

The `Arga.Sla.Addins.Core` package provides the following interfaces:

- **`IAddin`** - Base interface for all addins
- **`IDateTimeParser`** - Parse date/time strings from logs
- **`IVisualizer`** - Create custom visualizations for content
- **`ILineTranslator`** - Transform or filter individual log lines
- **`IExplorerItemFilter`** - Filter files/folders in the explorer
- **`ISlaController`** - Control SLA programmatically

## Customizing the Samples

Each sample addin can be customized by:

1. **Modifying patterns**: Update regex patterns to match your log formats
2. **Adding configuration**: Implement configuration windows for runtime settings
3. **Extending functionality**: Add new logic to the existing samples
4. **Creating new addins**: Use the samples as templates for your own addins

## Development Notes

- All addins must reference the `Arga.Sla.Addins.Core` NuGet package (v2.1.0 or later)
- Target framework: `net8.0-windows`
- The addin interfaces use Windows Presentation Foundation (WPF) for UI components
- Each addin should implement the `IAddin` interface or one of its derived interfaces

## License

This sample code is provided as-is for demonstration purposes.

## Contributing

Contributions are welcome! Feel free to submit issues or pull requests with:
- Bug fixes
- New sample addins
- Documentation improvements
- Enhancement suggestions

