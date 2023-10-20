namespace AutoXDisplay.BlazorServerApp.Data.DTO;

public class RunResult
{
    public decimal Time { get; set; }
    public int PenaltySeconds { get; set; }
    public bool DidNotFinish { get; set; }
}