using Microsoft.AspNetCore.Mvc;
using web_app_csharp.Entities;
using web_app_csharp.Models.Goods;
using web_app_csharp.Repositories;

namespace web_app_csharp.Controllers;

public class GoodsController : Controller
{
    private readonly IGoodRepository _goodRepository;
    private readonly ISaleRepository _saleRepository;

    public GoodsController(IGoodRepository goodRepository, ISaleRepository saleRepository)
    {
        _goodRepository = goodRepository;
        _saleRepository = saleRepository;
    }


    [HttpGet]
    public IActionResult Index()
    {
        var goods = _goodRepository.GetAll();
        var countOfGoodsBelowAveragePrice = _goodRepository.GetCountOfGoodsBelowAveragePrice();
        var goodEntities = goods.ToList();
        var goodName = goodEntities.ElementAt(0).Name;
        var largestSaleForFirstGood = goodName != null
            ? _saleRepository.GetLargestSaleByGoodName(goodName)
            : null;

        var showGoodsModel = new GoodsListModel()
        {
            Title = "Goods from API",
            Goods = goodEntities,
            ContOfGoodsBelowAveragePrice = countOfGoodsBelowAveragePrice,
            LargestSaleForFirstGood = largestSaleForFirstGood
        };

        return View(showGoodsModel);
    }
}