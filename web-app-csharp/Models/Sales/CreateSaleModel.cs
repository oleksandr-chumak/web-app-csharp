using System.ComponentModel.DataAnnotations;
using web_app_csharp.Attributes;

namespace web_app_csharp.Models.Sales;

public class CreateSaleModel
{
    [Required(ErrorMessage = "Sale check NO is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Sale check NO must be a positive number.")]
    public int CheckNo { get; set; } = 1;

    [Required(ErrorMessage = "Sale good ID is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Sale good ID must be a positive number.")]
    public int GoodId { get; set; } = 1;

    [Required(ErrorMessage = "Sale date is required.")]
    [DataType(DataType.Date)]
    [DateNotInFuture(ErrorMessage = "Sale date cannot be in the future.")]
    public DateTime DateSale { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "Sale quantity is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Sale quantity must be a positive number.")]
    public int Quantity { get; set; } = 1;
    
    public bool Submitted { get; set; }
    
    public Dictionary<string, List<string>> FieldErrors { get; set; } = new();
}
