using Sla.Addins.Core.Processor;
using System.Text.RegularExpressions;
using System.Windows;

namespace SLA.Addins.Samples.ParserAddin;

/// <summary>
/// Sample line translator that extracts and reformats JSON fragments from log lines
/// Useful for making JSON more readable in mixed-format logs
/// </summary>
public class JsonLogLineTranslator : ILineTranslator
{
    // Using non-greedy quantifier to match individual JSON objects
    // Handles balanced braces to avoid matching across multiple JSON objects
    private static readonly Regex JsonPattern = new Regex(
        @"\{(?:[^{}]|(?<open>\{)|(?<-open>\}))+(?(open)(?!))\}",
        RegexOptions.Compiled);
    
    public string Name => "JSON Line Formatter";
    
    public string Description => "Detects and reformats JSON fragments in log lines for better readability";
    
    public bool HasConfigurationWindow => false;
    
    public bool CanTranslate(string text)
    {
        // Check if the line contains JSON-like content
        return JsonPattern.IsMatch(text);
    }
    
    public string Translate(string line)
    {
        try
        {
            var match = JsonPattern.Match(line);
            if (!match.Success)
            {
                return line;
            }
            
            var jsonPart = match.Value;
            
            // Try to parse and prettify the JSON
            using var doc = System.Text.Json.JsonDocument.Parse(jsonPart);
            var options = new System.Text.Json.JsonSerializerOptions 
            { 
                WriteIndented = true 
            };
            var formattedJson = System.Text.Json.JsonSerializer.Serialize(doc, options);
            
            // Replace the JSON part with formatted version
            return line.Replace(jsonPart, $"\n{formattedJson}");
        }
        catch
        {
            // If JSON parsing fails, return the original line
            return line;
        }
    }
    
    public Window GetConfigurationWindow(string configurationFolder)
    {
        return null!;
    }
}
