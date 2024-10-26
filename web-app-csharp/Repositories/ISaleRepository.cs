using web_app_csharp.Entities;

namespace web_app_csharp.Repositories;

public interface ISaleRepository
{
    SaleEntity? GetLargestSaleByGoodName(string goodName);
}