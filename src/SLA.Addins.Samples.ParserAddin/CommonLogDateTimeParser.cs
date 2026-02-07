using Sla.Addins.Core;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;

namespace SLA.Addins.Samples.ParserAddin;

/// <summary>
/// Sample date/time parser for common log timestamp formats
/// Supports formats like: 2024-01-15 10:30:45, 2024/01/15 10:30:45.123, etc.
/// </summary>
public class CommonLogDateTimeParser : IDateTimeParser
{
    private static readonly string[] DateTimeFormats = new[]
    {
        "yyyy-MM-dd HH:mm:ss",
        "yyyy-MM-dd HH:mm:ss.fff",
        "yyyy/MM/dd HH:mm:ss",
        "yyyy/MM/dd HH:mm:ss.fff",
        "dd/MM/yyyy HH:mm:ss",
        "dd-MM-yyyy HH:mm:ss",
        "MM/dd/yyyy HH:mm:ss",
        "yyyy-MM-ddTHH:mm:ss",
        "yyyy-MM-ddTHH:mm:ss.fff",
        "yyyy-MM-ddTHH:mm:ss.fffZ"
    };
    
    public string Name => "Common Log DateTime Parser";
    
    public string Description => "Parses common date/time formats found in log files";
    
    public bool HasConfigurationWindow => false;
    
    public bool TryParse(string input, out DateTime? dateTime)
    {
        // Try standard DateTime.TryParse first
        if (DateTime.TryParse(input, out var dt))
        {
            dateTime = dt;
            return true;
        }
        
        // Try specific formats
        foreach (var format in DateTimeFormats)
        {
            if (DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, 
                DateTimeStyles.None, out dt))
            {
                dateTime = dt;
                return true;
            }
        }
        
        dateTime = null;
        return false;
    }
    
    public Window GetConfigurationWindow(string configurationFolder)
    {
        return null!;
    }
}
