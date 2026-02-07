# Quick Start Guide for SLA Addins

This guide will help you quickly get started with the SLA addin samples.

## What You'll Find Here

This repository contains **7 complete, ready-to-use sample addins** that demonstrate every interface type in the SLA Addins API:

1. **KeywordFilterTranslator** - Filter/transform lines based on keywords
2. **LogFileExplorerFilter** - Filter file explorer to show only log files
3. **CommonLogDateTimeParser** - Parse various date/time formats
4. **JsonLogLineTranslator** - Format JSON fragments in log lines
5. **JsonVisualizer** - Visualize JSON content
6. **StackTraceVisualizer** - Format and colorize stack traces
7. **AutoRefreshController** - Programmatically control SLA

## 5-Minute Quick Start

### Step 1: Clone and Build

```bash
git clone https://github.com/ArnaudGan/sla.addins.samples.git
cd sla.addins.samples
dotnet build -c Release
```

### Step 2: Copy Addins to SLA

```powershell
# Windows PowerShell
$slaAddinsPath = "$env:APPDATA\SLA\Addins"  # Adjust path as needed
Copy-Item "src\SLA.Addins.Samples.FilterAddin\bin\Release\net8.0-windows\*.dll" $slaAddinsPath
Copy-Item "src\SLA.Addins.Samples.ParserAddin\bin\Release\net8.0-windows\*.dll" $slaAddinsPath
Copy-Item "src\SLA.Addins.Samples.ExportAddin\bin\Release\net8.0-windows\*.dll" $slaAddinsPath
```

### Step 3: Restart SLA

Restart the SLA application to load the new addins.

## Testing the Addins

### Test the Keyword Filter
1. Open a log file in SLA
2. The KeywordFilterTranslator will automatically filter/highlight DEBUG and TRACE lines

### Test the File Explorer Filter
1. Navigate to a folder with mixed file types
2. Only .log, .txt, and .out files will be visible

### Test the Date Parser
1. SLA will automatically use the parser for common date formats
2. Check that timestamps are correctly parsed

### Test the JSON Visualizer
1. Right-click on a line containing JSON
2. Select "JSON Visualizer" from the context menu
3. View the formatted JSON

### Test the Stack Trace Visualizer
1. Find a log entry with an exception stack trace
2. Right-click and select "Stack Trace Visualizer"
3. View the color-coded, formatted stack trace

## Customizing for Your Needs

Each sample is designed to be easily customized:

### Change Filtered Keywords
Edit `KeywordFilterTranslator.cs`:
```csharp
// Line 15: Change the pattern
_keywordPattern = new Regex(@"\b(ERROR|FATAL)\b", ...);
```

### Add More File Extensions
Edit `LogFileExplorerFilter.cs`:
```csharp
// Line 13: Add extensions
@"\.(log|txt|out|trace|etl)$"
```

### Add Custom Date Formats
Edit `CommonLogDateTimeParser.cs`:
```csharp
// Line 14: Add your format
"MM-dd-yyyy HH:mm:ss.fff"
```

### Enable Auto-Refresh
Edit `AutoRefreshController.cs`:
```csharp
// Line 35: Uncomment these lines
_refreshTimer = new System.Threading.Timer(...);
```

## Next Steps

1. **Explore the Code**: Each addin is well-commented and easy to understand
2. **Read the READMEs**: Each project has detailed documentation
3. **Create Your Own**: Use these samples as templates for your custom addins
4. **Share**: Contribute your addins back to the community!

## Interface Reference

| Interface | Purpose | Sample |
|-----------|---------|--------|
| `ILineTranslator` | Transform/filter individual lines | KeywordFilterTranslator, JsonLogLineTranslator |
| `IExplorerItemFilter` | Filter files/folders | LogFileExplorerFilter |
| `IDateTimeParser` | Parse timestamps | CommonLogDateTimeParser |
| `IVisualizer` | Create custom views | JsonVisualizer, StackTraceVisualizer |
| `ISlaController` | Control SLA programmatically | AutoRefreshController |

## Troubleshooting

**Build fails on Linux/Mac**: This is expected. The addins use WPF which requires Windows. Build on Windows or use a Windows VM.

**Addins not loading**: Ensure you copied both the addin DLLs and the `Arga.Sla.Addins.Core.dll` to the SLA addins directory.

**Can't find addins in SLA**: Check SLA's addins folder path in the application settings.

## Getting Help

- Check the individual README files in each project folder
- Review the inline code comments
- Submit an issue on GitHub

## Contributing

Found a bug? Have an idea for a new sample? Pull requests are welcome!
