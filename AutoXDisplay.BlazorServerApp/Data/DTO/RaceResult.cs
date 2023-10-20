namespace AutoXDisplay.BlazorServerApp.Data.DTO;

public class RaceResult
{
    public Driver Driver { get; set; }
    public Car Car { get; set; }
    public List<RunResult> Runs { get; set; }
    public RunResult Total { get; set; }
    public decimal Difference { get; set; }
}