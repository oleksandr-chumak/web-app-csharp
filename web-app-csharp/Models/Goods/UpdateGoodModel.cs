using System.ComponentModel.DataAnnotations;

namespace web_app_csharp.Models.Goods;

public class UpdateGoodModel
{
    public int GoodId { get; set; }

    [Required(ErrorMessage = "Good name is required.")]
    [StringLength(20, ErrorMessage = "Good name can't be longer than 20 characters.")]
    public string? Name { get; set; } = "";

    [Required(ErrorMessage = "Good price is required.")]
    [Range(0.01, 10000.00, ErrorMessage = "Good price must be between 0.01 and 10000.")]
    public double? Price { get; set; } = 1;

    [Required(ErrorMessage = "Good producer is required.")]
    [StringLength(20, ErrorMessage = "Good producer can't be longer than 20 characters.")]
    public string Producer { get; set; } = "";

    [Required(ErrorMessage = "Good department ID is required.")]
    [Range(1, 9999, ErrorMessage = "Good department ID must be from 1 to 9999.")]
    public decimal? DeptId { get; set; } = 1;

    [Required(ErrorMessage = "Good description is required.")]
    [StringLength(50, ErrorMessage = "Good description can't be longer than 50 characters.")]
    public string Description { get; set; } = "";

    public bool Submitted { get; set; }

    public Dictionary<string, List<string>> FieldErrors { get; set; } = new();
}