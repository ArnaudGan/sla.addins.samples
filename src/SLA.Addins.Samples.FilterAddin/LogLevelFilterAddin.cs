using Sla.Addins.Core.Explorer;
using System.Text.RegularExpressions;
using System.Windows;

namespace SLA.Addins.Samples.FilterAddin;

/// <summary>
/// Sample explorer filter that filters files and folders in the SLA file explorer
/// This demonstrates how to create custom file/folder filters
/// </summary>
public class LogFileExplorerFilter : IExplorerItemFilter
{
    private static readonly Regex LogFilePattern = new Regex(
        @"\.(log|txt|out)$",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);
    
    public string Name => "Log File Filter";
    
    public string Description => "Filters the explorer to show only log files (.log, .txt, .out)";
    
    public bool HasConfigurationWindow => false;
    
    public bool PassesFilter(ExplorerItemType type, string itemName, bool passesDefaultFilter)
    {
        // Always show folders to allow navigation
        if (type == ExplorerItemType.Folder)
        {
            return true;
        }
        
        // For files, check if they match log file extensions
        if (type == ExplorerItemType.File)
        {
            return LogFilePattern.IsMatch(itemName);
        }
        
        return passesDefaultFilter;
    }
    
    public Window GetConfigurationWindow(string configurationFolder)
    {
        // No configuration window for this simple example
        return null!;
    }
}
