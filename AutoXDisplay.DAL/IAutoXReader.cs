using AutoXDisplay.DAL.Entities;

namespace AutoXDisplay.DAL;

public interface IAutoXReader
{
    Task SyncEventResultsFromWebAsync();
    
    //TODO: Implement a function that will use .CSV Uploads
    Task SyncEventResultsFromCsvAsync();
}