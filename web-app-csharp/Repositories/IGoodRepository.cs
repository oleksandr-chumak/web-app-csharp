using web_app_csharp.Entities;

namespace web_app_csharp.Repositories;

public interface IGoodRepository
{
    IEnumerable<GoodEntity> GetAll();

    int GetCountOfGoodsBelowAveragePrice();
}