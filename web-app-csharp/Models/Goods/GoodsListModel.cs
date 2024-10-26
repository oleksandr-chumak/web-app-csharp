using web_app_csharp.Entities;

namespace web_app_csharp.Models.Goods;

public class GoodsListModel
{
    public string Title { get; set; } = "";
    
    public IEnumerable<GoodEntity> Goods { get; set; } = new List<GoodEntity>();

    public int ContOfGoodsBelowAveragePrice;

    public SaleEntity? LargestSaleForFirstGood;

}