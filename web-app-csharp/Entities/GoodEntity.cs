namespace web_app_csharp.Entities;

public class GoodEntity
{
    public int GoodId { get; set; }

    public string? Name { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public string Producer { get; set; }

    public int DeptId { get; set; }

    public string Description { get; set; }
}