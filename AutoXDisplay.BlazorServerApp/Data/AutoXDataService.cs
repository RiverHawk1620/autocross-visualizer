using AutoXDisplay.DAL;

namespace AutoXDisplay.BlazorServerApp.Data;

public class AutoXDataService
{
    private static IAutoXReader? _autoXReader;

    public AutoXDataService(IAutoXReader autoXReader)
    {
        _autoXReader = autoXReader;
    }

    public static async Task ProcessDataAsync()
    {
        // Call the method from AutoXReader
        await _autoXReader?.SyncEventResultsFromWebAsync(); //.GetAwaiter().GetResult();

        // Additional data processing logic here
    }
}