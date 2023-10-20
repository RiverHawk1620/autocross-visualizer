using AutoXDisplay.DAL.Entities;

namespace AutoXDisplay.DAL;

public interface IAutoXReader
{
    Task<IEnumerable<RaceResultEntity>> SyncEventResultsFromWebAsync();
    
    //TODO: Implement a function that will use .CSV Uploads
    Task SyncEventResultsFromCsvAsync();
}