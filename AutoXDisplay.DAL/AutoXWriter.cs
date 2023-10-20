using AutoXDisplay.DAL.Entities;
using CsvHelper;
using System.Globalization;

namespace AutoXDisplay.DAL
{
    public class AutoXWriter : IAutoXWriter
    {
        public Task WriteCsvFileAsyc(IEnumerable<RaceResultEntity> raceResultEntities, string filePath = "_artifacts/autoXtest2.csv")
        {
            //TODO: Check if the folder exists, create if possible
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                return csv.WriteRecordsAsync(raceResultEntities);
            }
        }
    }
}
