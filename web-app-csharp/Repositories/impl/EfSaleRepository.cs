using web_app_csharp.Attributes;
using web_app_csharp.Entities;
using web_app_csharp.Models;
using System.Linq;

namespace web_app_csharp.Repositories.impl;

[ScopedService]
public class EfSaleRepository : ISaleRepository
{
    private readonly ApplicationDbContext _context;

    public EfSaleRepository(ApplicationDbContext applicationDbContext)
    {
        _context = applicationDbContext;
    }

    public SaleEntity? GetLargestSaleByGoodName(string goodName)
    {
        var largestSale = _context.Sales
            .Where(s => s.Good.Name == goodName)
            .OrderByDescending(s => s.Quantity)
            .FirstOrDefault();

        if (largestSale == null)
        {
            return null;
        }

        return new SaleEntity
        {
            SalesId = largestSale.SalesId,
            CheckNo = largestSale.CheckNo,
            GoodId = largestSale.GoodId,
            DateSale = largestSale.DateSale,
            Quantity = largestSale.Quantity
        };
    }

    public IEnumerable<SaleEntity> GetAll()
    {
        return _context.Sales
            .Select(s => new SaleEntity
            {
                SalesId = s.SalesId,
                CheckNo = s.CheckNo,
                GoodId = s.GoodId,
                DateSale = s.DateSale,
                Quantity = s.Quantity
            })
            .ToList();
    }

    public SaleEntity? GetById(decimal id)
    {
        var sale = _context.Sales
            .FirstOrDefault(s => s.SalesId == id);

        if (sale == null)
        {
            return null; // Or handle as per your requirements
        }

        return new SaleEntity
        {
            SalesId = sale.SalesId,
            CheckNo = sale.CheckNo,
            GoodId = sale.GoodId,
            DateSale = sale.DateSale,
            Quantity = sale.Quantity
        };
    }

    public void Add(SaleEntity obj)
    {
        var newSale = new Sale
        {
            CheckNo = obj.CheckNo,
            GoodId = obj.GoodId,
            DateSale = obj.DateSale,
            Quantity = obj.Quantity
        };

        _context.Sales.Add(newSale);
        _context.SaveChanges();
    }

    public void Update(SaleEntity model)
    {
        var existingSale = _context.Sales
            .FirstOrDefault(s => s.SalesId == model.SalesId);

        if (existingSale != null)
        {
            existingSale.CheckNo = model.CheckNo;
            existingSale.GoodId = model.GoodId;
            existingSale.DateSale = model.DateSale;
            existingSale.Quantity = model.Quantity;

            _context.SaveChanges();
        }
        else
        {
            throw new KeyNotFoundException($"Sale with ID {model.SalesId} not found.");
        }
    }

    public void DeleteById(int id)
    {
        var saleToDelete = _context.Sales
            .FirstOrDefault(s => s.SalesId == id);

        if (saleToDelete != null)
        {
            _context.Sales.Remove(saleToDelete);
            _context.SaveChanges();
        }
        else
        {
            throw new KeyNotFoundException($"Sale with ID {id} not found.");
        }
    }
}