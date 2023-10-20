using AutoXDisplay.DAL.Entities;

namespace AutoXDisplay.DAL
{
    public interface IAutoXWriter
    {
        Task WriteCsvFileAsyc(IEnumerable<RaceResultEntity> raceResultEntities, string filePath = "/artifacts/");
    }
}
