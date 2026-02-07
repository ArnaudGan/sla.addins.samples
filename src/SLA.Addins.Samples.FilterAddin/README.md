# Filter Addins

This project contains sample filter addins for SLA.

## Addins in this Project

### KeywordFilterTranslator

**Interface**: `ILineTranslator`

**Description**: Filters or highlights lines containing specific keywords or patterns.

**Configuration**:
- `_keywordPattern`: Regex pattern to match (default: DEBUG|TRACE)
- `_removeMatching`: Set to `true` to remove matching lines, `false` to highlight them

**Example Use Cases**:
- Remove debug or trace level logs
- Highlight error keywords
- Filter sensitive information

### LogFileExplorerFilter

**Interface**: `IExplorerItemFilter`

**Description**: Filters the SLA file explorer to show only log files.

**Supported Extensions**:
- `.log`
- `.txt`
- `.out`

**Features**:
- Always shows folders for navigation
- Only filters files, not directories
- Case-insensitive extension matching

## Customization Examples

### Change Keyword Pattern

```csharp
// In KeywordFilterTranslator constructor
_keywordPattern = new Regex(@"\b(ERROR|FATAL|EXCEPTION)\b", 
    RegexOptions.IgnoreCase | RegexOptions.Compiled);
```

### Add More File Extensions

```csharp
// In LogFileExplorerFilter
private static readonly Regex LogFilePattern = new Regex(
    @"\.(log|txt|out|trace|etl)$",
    RegexOptions.IgnoreCase | RegexOptions.Compiled);
```

## Building

```bash
dotnet build src/SLA.Addins.Samples.FilterAddin
```

## Installation

1. Build the project in Release mode
2. Copy `SLA.Addins.Samples.FilterAddin.dll` to SLA's addins folder
3. Restart SLA
