# Parser Addins

This project contains sample parser addins for SLA.

## Addins in this Project

### CommonLogDateTimeParser

**Interface**: `IDateTimeParser`

**Description**: Parses common date/time formats found in log files.

**Supported Formats**:
- `yyyy-MM-dd HH:mm:ss`
- `yyyy-MM-dd HH:mm:ss.fff`
- `yyyy/MM/dd HH:mm:ss`
- `yyyy/MM/dd HH:mm:ss.fff`
- `dd/MM/yyyy HH:mm:ss`
- `dd-MM-yyyy HH:mm:ss`
- `MM/dd/yyyy HH:mm:ss`
- `yyyy-MM-ddTHH:mm:ss` (ISO 8601)
- `yyyy-MM-ddTHH:mm:ss.fff`
- `yyyy-MM-ddTHH:mm:ss.fffZ` (ISO 8601 UTC)

**Features**:
- Falls back to standard .NET DateTime parsing
- Tries multiple formats in order
- Culture-invariant parsing

### JsonLogLineTranslator

**Interface**: `ILineTranslator`

**Description**: Detects and reformats JSON fragments in log lines.

**Features**:
- Detects JSON objects in mixed-format logs
- Pretty-prints JSON for readability
- Gracefully handles malformed JSON

**Use Cases**:
- Mixed logs with JSON payloads
- Structured logging in plain text files
- API request/response logging

## Customization Examples

### Add Custom Date Format

```csharp
// In CommonLogDateTimeParser
private static readonly string[] DateTimeFormats = new[]
{
    // ... existing formats ...
    "MM-dd-yyyy HH:mm:ss",  // Add your custom format
};
```

### Customize JSON Formatting

```csharp
// In JsonLogLineTranslator.Translate()
var options = new System.Text.Json.JsonSerializerOptions 
{ 
    WriteIndented = true,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
};
```

## Building

```bash
dotnet build src/SLA.Addins.Samples.ParserAddin
```

## Installation

1. Build the project in Release mode
2. Copy `SLA.Addins.Samples.ParserAddin.dll` to SLA's addins folder
3. Restart SLA
