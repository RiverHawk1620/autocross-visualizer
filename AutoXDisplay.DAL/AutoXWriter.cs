using AutoXDisplay.DAL.Entities;
using CsvHelper;
using System.Globalization;

namespace AutoXDisplay.DAL
{
    public class AutoXWriter : IAutoXWriter
    {
        public Task WriteCsvFileAsyc(IEnumerable<RaceResultEntity> raceResultEntities, string filePath = "/artifacts/")
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                return csv.WriteRecordsAsync(raceResultEntities);
            }
        }
    }
}
