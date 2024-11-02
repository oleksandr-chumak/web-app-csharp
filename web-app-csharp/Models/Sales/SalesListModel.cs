using web_app_csharp.Entities;

namespace web_app_csharp.Models.Sales;

public class SalesListModel
{
    public IEnumerable<SaleEntity> Sales { get; set; }
}