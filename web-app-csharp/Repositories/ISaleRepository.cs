using web_app_csharp.Entities;
using web_app_csharp.Interfaces;

namespace web_app_csharp.Repositories;

public interface ISaleRepository : ICrudRepository<SaleEntity>
{
    SaleEntity? GetLargestSaleByGoodName(string goodName);
}