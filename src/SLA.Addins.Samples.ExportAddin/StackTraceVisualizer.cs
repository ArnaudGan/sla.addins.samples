using Sla.Addins.Core.Visualizer;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SLA.Addins.Samples.ExportAddin;

/// <summary>
/// Sample visualizer that displays stack traces in a more readable format
/// </summary>
public class StackTraceVisualizer : IVisualizer
{
    private static readonly Regex StackTracePattern = new Regex(
        @"at\s+[\w\.<>]+\(.*?\)\s+in\s+.*?:line\s+\d+",
        RegexOptions.Multiline | RegexOptions.Compiled);
    
    public string Name => "Stack Trace Visualizer";
    
    public string Description => "Displays stack traces with improved formatting and readability";
    
    public bool HasConfigurationWindow => false;
    
    public bool CanVisualize(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return false;
        }
        
        // Check if text contains stack trace patterns
        return text.Contains("at ") && 
               (text.Contains("Exception") || text.Contains(".cs:line") || StackTracePattern.IsMatch(text));
    }
    
    public Control GetVisualizer(string text)
    {
        try
        {
            var stackPanel = new StackPanel
            {
                Background = new SolidColorBrush(Color.FromRgb(248, 248, 248)),
                Margin = new Thickness(5)
            };
            
            var lines = text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            
            foreach (var line in lines)
            {
                var textBlock = new TextBlock
                {
                    Text = line.Trim(),
                    FontFamily = new FontFamily("Consolas"),
                    FontSize = 12,
                    Margin = new Thickness(5, 2, 5, 2),
                    TextWrapping = TextWrapping.Wrap
                };
                
                // Highlight exception types
                if (line.Contains("Exception"))
                {
                    textBlock.Foreground = Brushes.Red;
                    textBlock.FontWeight = FontWeights.Bold;
                }
                // Highlight stack frames
                else if (line.Trim().StartsWith("at "))
                {
                    textBlock.Foreground = Brushes.DarkBlue;
                    textBlock.Margin = new Thickness(20, 2, 5, 2);
                }
                
                stackPanel.Children.Add(textBlock);
            }
            
            var scrollViewer = new ScrollViewer
            {
                Content = stackPanel,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Auto
            };
            
            return scrollViewer;
        }
        catch (Exception ex)
        {
            return new TextBox
            {
                Text = $"Failed to visualize stack trace: {ex.Message}",
                Foreground = Brushes.Red,
                Padding = new Thickness(10)
            };
        }
    }
    
    public Window GetConfigurationWindow(string configurationFolder)
    {
        return null!;
    }
}
