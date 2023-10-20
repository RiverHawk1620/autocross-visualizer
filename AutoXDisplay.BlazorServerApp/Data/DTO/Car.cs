using System.ComponentModel.DataAnnotations;

namespace AutoXDisplay.BlazorServerApp.Data.DTO;

public class Car
{
    public int? Year { get; set; }
    [Required]
    public string Make { get; set; }
    public string? Model { get; set; }
    public string? Color { get; set; }
    [Required]
    public string? Class { get; set; }
    [Required]
    public int Number { get; set; }
    public string? NumberSuffix { get; set; }
}