using AutoXDisplay.DAL.Entities;
//using AutoXDisplay.BlazorServerApp.Data.DTO;
using HtmlAgilityPack;
namespace AutoXDisplay.DAL;

public class AutoXReader : IAutoXReader
{
    public async Task SyncEventResultsFromWebAsync()
    {
        // Create an instance of HttpClient
        using var httpClient = new HttpClient();
        httpClient.Timeout = TimeSpan.FromMinutes(2);
        // Specify the URL
        var url = "https://www.wmclub.org/wp-content/uploads/2023/08/2023event6results.htm";
        HttpResponseMessage responseMessage = null;
        
        try
        {
            // Send a GET request to the URL and retrieve the HTML content
            responseMessage = await httpClient.GetAsync(url);
            responseMessage.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            // Handle the exception accordingly
        }

        // Load the HTML content into HtmlDocument
        var htmlContent = await responseMessage.Content.ReadAsStringAsync();
        var doc = new HtmlDocument();
        doc.LoadHtml(htmlContent);

        // Get all table elements on the page
        HtmlNodeCollection tables = doc.DocumentNode.SelectNodes("//table");
        
        var runCount = GetRunCount(tables);
        
        // Now you can use XPath or LINQ queries to extract the desired information from the HTML document
        // For example, to extract all the text within <h1> elements:
        //var tdElements = doc.DocumentNode.Descendants("td");

        IList<RaceResultEntity> raceResultEntities = new List<RaceResultEntity>();

        // Get the 2nd Row of the Table because the first is the header
        HtmlNodeCollection tableRows = tables[1].SelectNodes(".//tr");
        foreach (var row in tableRows)
        {
            var columns = row.Descendants("td").ToList();
            
            if (columns.Count > 0)
                raceResultEntities.Add(new RaceResultEntity()
                {
                    Position = columns[0].InnerText,
                    Class = columns[1].InnerText,
                    Number = columns[2].InnerText,
                    Name = columns[3].InnerText,
                    Car = columns[4].InnerText,
                    CarColor = columns[5].InnerText,
                    Runs = ExtractRunData(columns, (int)runCount!),
                    Total = columns[^2].InnerText, //same as columns[columns.Count - 1]
                    Diff = columns[^1].InnerText
                });
        }
        
        //TODO: I have all of the data in raceResultEntities, need to:
        //      1. Write Mappings From Entities to DTOs
        //      2. Decide where I will store the data
        //      3. Write the data out to wherever that might be
    }

    private static List<string> ExtractRunData(List<HtmlNode> columns, int numberOfRuns)
    {
        if (numberOfRuns == 0)
            return new List<string>();
        
        //TODO: Might need to be a constant eventually
        var startingColumn = 7;
        var runList = new List<string>();
        for (var i = startingColumn - 1; i < startingColumn - 1 + numberOfRuns; i++)
        {
            runList.Add(columns[i].InnerText);
        }

        return runList;
    }
    
    private static int? GetRunCount(HtmlNodeCollection tables)
    {
        if (tables is { Count: >= 2 })
        {
            // Get the second table element
            HtmlNode secondTable = tables[1];

            // Get the 2nd Row of the Table because the first is the header
            HtmlNode secondRow = secondTable.SelectNodes(".//tr")[1];

            if (secondRow != null)
            {
                // Count the number of columns (td or th elements) in the second row
                return secondRow.SelectNodes(".//td|.//th").Count - 8;

                //We assume that the total number -8 is the number of runs completed for the event.
                //Console.WriteLine($"Number of Runs in the table: {columnCount - 8}");
            }
            //Handle errors id there is no second row?
        }
        else
        {
            Console.WriteLine("No table found in the HTML document.");
        }

        return null;
    }

    public Task SyncEventResultsFromCsvAsync()
    {
        throw new NotImplementedException();
    }
}