using web_app_csharp.Attributes;
using web_app_csharp.Entities;
using web_app_csharp.Models;

namespace web_app_csharp.Repositories.impl;

[ScopedService]
public class EfGoodRepository : IGoodRepository
{
    private readonly ApplicationDbContext _context;

    public EfGoodRepository(ApplicationDbContext applicationDbContext)
    {
        _context = applicationDbContext;
    }

    public IEnumerable<GoodEntity> GetAll()
    {
        return _context.Goods
            .Select(g => new GoodEntity
            {
                GoodId = g.GoodId,
                Name = g.Name,
                Price = g.Price,
                Quantity = g.Quantity,
                Producer = g.Producer,
                DeptId = g.DeptId,
                Description = g.Description
            })
            .ToList(); 
    }

    public int GetCountOfGoodsBelowAveragePrice()
    {
        var averagePrice = _context.Goods
            .Average(g => g.Price) ?? 0;

        return _context.Goods
            .Count(g => g.Price < averagePrice);
    }
}