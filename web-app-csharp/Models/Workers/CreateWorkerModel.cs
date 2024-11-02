using System.ComponentModel.DataAnnotations;

namespace web_app_csharp.Models.Workers;

public class CreateWorkerModel
{
    [Required(ErrorMessage = "Worker name is required.")]
    [StringLength(20, ErrorMessage = "Worker name can't be longer than 20 characters.")]
    public string Name { get; set; } = "";

    [StringLength(40, ErrorMessage = "Worker address can't be longer than 40 characters.")]
    public string? Address { get; set; } = null;

    [Range(1, 9999, ErrorMessage = "Worker department ID must be from 1 to 9999.")]
    public decimal? DeptId { get; set; }
    
    [StringLength(20, ErrorMessage = "Worker information can't be longer than 20 characters.")]
    public string? Information { get; set; }
    
    public bool Submitted { get; set; } = false;
    
    public Dictionary<string, List<string>> FieldErrors { get; set; } = new();
}