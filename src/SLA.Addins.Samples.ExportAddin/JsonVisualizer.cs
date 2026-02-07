using Sla.Addins.Core.Visualizer;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace SLA.Addins.Samples.ExportAddin;

/// <summary>
/// Sample visualizer that displays JSON content in a formatted, readable way
/// </summary>
public class JsonVisualizer : IVisualizer
{
    public string Name => "JSON Visualizer";
    
    public string Description => "Displays JSON content with syntax highlighting and formatting";
    
    public bool HasConfigurationWindow => false;
    
    public bool CanVisualize(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return false;
        }
        
        try
        {
            using var doc = JsonDocument.Parse(text);
            return doc.RootElement.ValueKind == JsonValueKind.Object || 
                   doc.RootElement.ValueKind == JsonValueKind.Array;
        }
        catch
        {
            return false;
        }
    }
    
    public Control GetVisualizer(string text)
    {
        try
        {
            // Parse and pretty-print the JSON
            using var doc = JsonDocument.Parse(text);
            var options = new JsonSerializerOptions 
            { 
                WriteIndented = true 
            };
            var formattedJson = JsonSerializer.Serialize(doc, options);
            
            // Create a TextBox to display the formatted JSON
            var textBox = new TextBox
            {
                Text = formattedJson,
                IsReadOnly = true,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                FontFamily = new System.Windows.Media.FontFamily("Consolas"),
                Padding = new Thickness(10),
                Background = System.Windows.Media.Brushes.White
            };
            
            return textBox;
        }
        catch (Exception ex)
        {
            // Return error message if parsing fails
            return new TextBox
            {
                Text = $"Failed to parse JSON: {ex.Message}",
                Foreground = System.Windows.Media.Brushes.Red,
                Padding = new Thickness(10)
            };
        }
    }
    
    public Window GetConfigurationWindow(string configurationFolder)
    {
        return null!;
    }
}
