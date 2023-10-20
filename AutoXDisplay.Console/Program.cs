using AutoXDisplay.DAL;

namespace AutoXDisplay.Console
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var reader = new AutoXReader();
            var raceResultEntities = await reader.SyncEventResultsFromWebAsync();
            var writer = new AutoXWriter();
            await writer.WriteCsvFileAsyc(raceResultEntities);
        }
    }
}