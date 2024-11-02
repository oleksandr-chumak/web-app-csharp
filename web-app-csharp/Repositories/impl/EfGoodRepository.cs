using web_app_csharp.Attributes;
using web_app_csharp.Entities;
using web_app_csharp.Models;
using System.Linq;

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

    public GoodEntity? GetById(decimal id)
    {
        var good = _context.Goods
            .FirstOrDefault(g => g.GoodId == id);

        if (good == null)
        {
            return null; // Or handle as per your requirements
        }

        return new GoodEntity
        {
            GoodId = good.GoodId,
            Name = good.Name,
            Price = good.Price,
            Quantity = good.Quantity,
            Producer = good.Producer,
            DeptId = good.DeptId,
            Description = good.Description
        };
    }

    public void Add(GoodEntity obj)
    {
        var newGood = new Good
        {
            Name = obj.Name,
            Price = obj.Price,
            Quantity = obj.Quantity,
            Producer = obj.Producer,
            DeptId = obj.DeptId,
            Description = obj.Description
        };

        _context.Goods.Add(newGood);
        _context.SaveChanges();
    }

    public void Update(GoodEntity model)
    {
        var existingGood = _context.Goods
            .FirstOrDefault(g => g.GoodId == model.GoodId);

        if (existingGood != null)
        {
            existingGood.Name = model.Name;
            existingGood.Price = model.Price;
            existingGood.Quantity = model.Quantity;
            existingGood.Producer = model.Producer;
            existingGood.DeptId = model.DeptId;
            existingGood.Description = model.Description;

            _context.SaveChanges();
        }
        else
        {
            throw new KeyNotFoundException($"Good with ID {model.GoodId} not found.");
        }
    }

    public void DeleteById(int id)
    {
        var goodToDelete = _context.Goods
            .FirstOrDefault(g => g.GoodId == id);

        if (goodToDelete != null)
        {
            _context.Goods.Remove(goodToDelete);
            _context.SaveChanges();
        }
        else
        {
            throw new KeyNotFoundException($"Good with ID {id} not found.");
        }
    }

    public int GetCountOfGoodsBelowAveragePrice()
    {
        var averagePrice = _context.Goods
            .Average(g => g.Price) ?? 0;

        return _context.Goods
            .Count(g => g.Price < averagePrice);
    }
}