using System.Text;
using System.Text.RegularExpressions;
using AutoXDisplay.BlazorServerApp.Data.DTO;
using AutoXDisplay.DAL.Entities;

namespace AutoXDisplay.BlazorServerApp.Data.Mapping;

public static class RaceResultExtensions
{
    public static RaceResult ToRaceResult(this RaceResultEntity raceResultEntity)
    {
    // public Driver Driver { get; set; }
    // public Car Car { get; set; }
    // public List<RunResult> Runs { get; set; }
    // public RunResult Total { get; set; }
    // public decimal Difference { get; set; }
    
        var carInfo = ParseCarString(raceResultEntity.Car);
        return new RaceResult
        {
            Driver = new Driver() { Name = raceResultEntity.Name },
            Car = new Car()
            {
                Year = carInfo.year,
                Make = carInfo.make,
                Model = carInfo.model,
                Color = raceResultEntity.CarColor,
                Class = raceResultEntity.Class,
                Number = RemoveNonNumericalCharacters(raceResultEntity.Number),
                NumberSuffix = GetTrailingLetters(raceResultEntity.Number)
            },
            //TODO: Extract runs into RunResult
            //Runs = raceResultEntity.Runs,
            //TODO: This is also an extraction into RunResult, But really, it can be based upon the field above.
            //Total = raceResultEntity.Total,
            //TODO: Need to Find a Way to extract Diffs
            //[-]1.544 <-Means they beat 2nd by this much
            //+1.544
            //+2.768
            //There is just a dash if there are no other racers in the category
            //Difference = raceResultEntity.Diff
        };
            
    }
    
    private static (int? year, string make, string model) ParseCarString(string? carString)
    {
        if (carString == null)
            return (null, string.Empty, string.Empty); 
        
        int? carYear = null;
        string carMake = string.Empty;
        string carModel = string.Empty;
        
        // Split the carString by spaces
        string[] parts = carString.Split(' ');

        // Extract the year
        if (int.TryParse(parts[0], out int year))
        {
            carYear = year;
        }

        // Extract the make and model
        if (parts.Length > 1)
        {
            carMake = parts[1];
            carModel = string.Join(' ', parts.Skip(2));
        }

        return (carYear, carMake, carModel);
    }
    
    private static string GetTrailingLetters(string? input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;
        
        string trailingLetters = string.Empty;

        // Check if the string starts with up to 4 numbers
        if (Regex.IsMatch(input, @"^\d{1,4}"))
        {
            // Extract any trailing letters
            trailingLetters = Regex.Match(input, @"\D+$").Value;
        }

        return trailingLetters;
    }
    
    private static int RemoveNonNumericalCharacters(string? input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return 0;
        }
        
        StringBuilder result = new StringBuilder();

        foreach (char c in input)
        {
            if (Char.IsDigit(c))
            {
                result.Append(c);
            }
        }

        int parsedResult;
        if (int.TryParse(result.ToString(), out parsedResult))
        {
            return parsedResult;
        }

        return 0; // or any default value you prefer
    }
}