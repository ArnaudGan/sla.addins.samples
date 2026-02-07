using Sla.Addins.Core.Processor;
using System.Text.RegularExpressions;
using System.Windows;

namespace SLA.Addins.Samples.FilterAddin;

/// <summary>
/// Sample line translator that filters/translates lines based on keywords
/// This demonstrates how to create a custom line processor for SLA
/// </summary>
public class KeywordFilterTranslator : ILineTranslator
{
    private readonly Regex _keywordPattern;
    private readonly bool _removeMatching;
    
    public string Name => "Keyword Filter Translator";
    
    public string Description => "Filters or highlights lines containing specific keywords. " +
                                "Configure keywords and behavior in the configuration window.";
    
    public bool HasConfigurationWindow => false;
    
    /// <summary>
    /// Creates a keyword filter with default settings
    /// You can modify the pattern and behavior in the constructor
    /// </summary>
    public KeywordFilterTranslator()
    {
        // Example: Filter out lines containing "DEBUG" or "TRACE"
        _keywordPattern = new Regex(@"\b(DEBUG|TRACE)\b", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        _removeMatching = false; // Set to true to remove matching lines, false to keep them
    }
    
    public bool CanTranslate(string text)
    {
        // Check if the line matches our keyword pattern
        return _keywordPattern.IsMatch(text);
    }
    
    public string Translate(string line)
    {
        if (_removeMatching)
        {
            // Remove the line by returning empty string or null
            return string.Empty;
        }
        
        // Optionally highlight or modify the line
        // For this example, we'll prefix it with a marker
        return $"[FILTERED] {line}";
    }
    
    public Window GetConfigurationWindow(string configurationFolder)
    {
        // No configuration window for this simple example
        return null!;
    }
}
