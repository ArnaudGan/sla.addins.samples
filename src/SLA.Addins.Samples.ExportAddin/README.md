# Visualizer & Controller Addins

This project contains sample visualizer and controller addins for SLA.

## Addins in this Project

### JsonVisualizer

**Interface**: `IVisualizer`

**Description**: Displays JSON content with syntax highlighting and formatting.

**Features**:
- Detects valid JSON objects and arrays
- Pretty-prints JSON with indentation
- Uses monospace font for readability
- Scrollable viewer for large JSON
- Error handling for malformed JSON

**Use Cases**:
- View JSON log entries
- Inspect API responses in logs
- Structured log visualization

### StackTraceVisualizer

**Interface**: `IVisualizer`

**Description**: Displays stack traces with improved formatting and color coding.

**Features**:
- Detects exception stack traces
- Color-codes exception types (red, bold)
- Indents stack frames for hierarchy
- Uses monospace font
- Scrollable viewer

**Visual Elements**:
- **Red, bold text**: Exception types and messages
- **Dark blue, indented**: Stack frames (at ...)
- **Light gray background**: Overall container

### AutoRefreshController

**Interface**: `ISlaController`

**Description**: Demonstrates programmatic control of SLA with auto-refresh capability.

**Features**:
- Monitors startup and shutdown events
- Can automatically refresh open log sources
- Configurable refresh interval
- Safe cleanup on application close

**Note**: Auto-refresh is disabled by default. Uncomment code in `OnStarted()` to enable.

**Configuration**:
```csharp
// Enable auto-refresh every 5 seconds
_refreshTimer = new System.Threading.Timer(
    callback: _ => RefreshAllSources(),
    state: null,
    dueTime: TimeSpan.FromSeconds(5),
    period: TimeSpan.FromSeconds(5)
);
```

## Customization Examples

### Customize JSON Display Colors

```csharp
// In JsonVisualizer.GetVisualizer()
var textBox = new TextBox
{
    // ... existing properties ...
    Foreground = System.Windows.Media.Brushes.DarkGreen,
    Background = System.Windows.Media.Brushes.LightYellow
};
```

### Modify Stack Trace Colors

```csharp
// In StackTraceVisualizer.GetVisualizer()
if (line.Contains("Exception"))
{
    textBlock.Foreground = Brushes.DarkRed;  // Change color
    textBlock.FontSize = 14;                 // Change size
}
```

### Change Auto-Refresh Interval

```csharp
// In AutoRefreshController.OnStarted()
_refreshTimer = new System.Threading.Timer(
    callback: _ => RefreshAllSources(),
    state: null,
    dueTime: TimeSpan.FromSeconds(10),    // Wait 10s before first refresh
    period: TimeSpan.FromSeconds(30)       // Refresh every 30s
);
```

## Building

```bash
dotnet build src/SLA.Addins.Samples.ExportAddin
```

## Installation

1. Build the project in Release mode
2. Copy `SLA.Addins.Samples.ExportAddin.dll` to SLA's addins folder
3. Restart SLA

## Usage Tips

- **JsonVisualizer**: Right-click on JSON content in SLA and select the visualizer
- **StackTraceVisualizer**: Right-click on exception stack traces for formatted view
- **AutoRefreshController**: Runs automatically in background (if enabled)
