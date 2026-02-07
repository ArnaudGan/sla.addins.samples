using Sla.Addins.Core.Controller;
using System.Windows;

namespace SLA.Addins.Samples.ExportAddin;

/// <summary>
/// Sample controller addin that demonstrates how to control SLA programmatically
/// This could be used for automation, batch processing, or integration with other tools
/// </summary>
public class AutoRefreshController : ISlaController
{
    private IControlable? _controlable;
    private System.Threading.Timer? _refreshTimer;
    
    public string Name => "Auto Refresh Controller";
    
    public string Description => "Automatically refreshes open log sources at regular intervals";
    
    public bool HasConfigurationWindow => false;
    
    public void Initialize(IControlable controlable)
    {
        _controlable = controlable;
        
        // Subscribe to events
        _controlable.Started += OnStarted;
        _controlable.Closing += OnClosing;
    }
    
    private void OnStarted(object? sender, IStartupInfo e)
    {
        // Example: Set up auto-refresh every 5 seconds (disabled by default)
        // Uncomment the following lines to enable:
        
        // _refreshTimer = new System.Threading.Timer(
        //     callback: _ => RefreshAllSources(),
        //     state: null,
        //     dueTime: TimeSpan.FromSeconds(5),
        //     period: TimeSpan.FromSeconds(5)
        // );
    }
    
    private void OnClosing(object? sender, EventArgs e)
    {
        // Clean up timer
        _refreshTimer?.Dispose();
        _refreshTimer = null;
    }
    
    private void RefreshAllSources()
    {
        if (_controlable == null) return;
        
        try
        {
            // Get all open sources
            var openSources = _controlable.GetOpenSources().ToList();
            
            // Refresh each source by closing and reopening
            foreach (var source in openSources)
            {
                Application.Current?.Dispatcher.Invoke(() =>
                {
                    _controlable.CloseSource(source);
                    _controlable.OpenSource(source);
                });
            }
        }
        catch (Exception)
        {
            // Silently handle errors in auto-refresh
        }
    }
    
    public Window GetConfigurationWindow(string configurationFolder)
    {
        return null!;
    }
}
