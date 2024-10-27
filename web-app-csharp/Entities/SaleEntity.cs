namespace web_app_csharp.Entities;

public class SaleEntity
{
    public int SalesId { get; set; }

    public int CheckNo { get; set; }

    public int GoodId { get; set; }

    public DateTime DateSale { get; set; }

    public int? Quantity { get; set; }
}