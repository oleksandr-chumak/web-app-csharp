using web_app_csharp.Attributes;
using web_app_csharp.Entities;
using web_app_csharp.Models;

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
}