using Microsoft.AspNetCore.Mvc;
using web_app_csharp.Entities;
using web_app_csharp.Models.Goods;
using web_app_csharp.Repositories;
using web_app_csharp.Utils;

namespace web_app_csharp.Controllers;

public class GoodsController : Controller
{
    private readonly IGoodRepository _goodRepository;

    private readonly ISaleRepository _saleRepository;

    private readonly IDepartmentRepository _departmentRepository;


    public GoodsController(IGoodRepository goodRepository, ISaleRepository saleRepository,
        IDepartmentRepository departmentRepository)
    {
        _goodRepository = goodRepository;
        _saleRepository = saleRepository;
        _departmentRepository = departmentRepository;
    }


    [HttpGet]
    public IActionResult Index()
    {
        var goods = _goodRepository.GetAll();
        var countOfGoodsBelowAveragePrice = _goodRepository.GetCountOfGoodsBelowAveragePrice();
        var goodEntities = goods.ToList();
        var goodName = goodEntities.Count != 0 ? goodEntities[0].Name : null;
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

    [HttpGet("create")]
    public IActionResult Create()
    {
        var model = new CreateGoodModel();
        return View(model);
    }

    [HttpPost("create")]
    public IActionResult Create(CreateGoodModel model)
    {
        model.Submitted = true;

        if (!ModelState.IsValid)
        {
            model.FieldErrors["DeptId"] = [$"Department with id {model.DeptId} not found."];
            return View(model);
        }

        var foundDepartment = _departmentRepository.GetById(model.DeptId);

        if (foundDepartment == null)
        {
            model.FieldErrors["DeptId"] = [$"Department with id {model.DeptId} not found."];
            return View(model);
        }

        _goodRepository.Add(new GoodEntity()
        {
            Name = model.Name,
            Price = model.Price,
            Producer = model.Producer,
            DeptId = model.DeptId,
            Description = model.Description,
        });

        return RedirectToAction("Index");
    }

    [HttpGet("update/{id:int}")]
    public IActionResult Update(int id)
    {
        var good = _goodRepository.GetById(id);

        if (good == null) return RedirectToAction("Index");

        var model = new UpdateGoodModel()
        {
            GoodId = good.GoodId,
            Name = good.Name,
            Price = good.Price,
            Producer = good.Producer,
            DeptId = good.DeptId,
            Description = good.Description,
        };
        return View(model);
    }

    [HttpPost("update/{id:int}")]
    public IActionResult Update(int id, UpdateGoodModel model)
    {
        model.Submitted = true;
        model.GoodId = id;
        var good = _goodRepository.GetById(id);

        if (good == null) return RedirectToAction("Index");

        if (!ModelState.IsValid)
        {
            model.FieldErrors = ModelErrorUtil.GetErrors(ModelState);
            return View(model);
        }

        var deptId = model.DeptId;
        var foundDepartment = deptId != null ? _departmentRepository.GetById(deptId ?? 0) : null;

        if (foundDepartment == null && model.DeptId != null)
        {
            model.FieldErrors["DeptId"] = [$"Department with id {model.DeptId} not found."];
            return View(model);
        }

        _goodRepository.Update(new GoodEntity()
        {
            GoodId = id,
            Name = model.Name,
            Price = model.Price,
            Producer = model.Producer,
            DeptId = model.DeptId,
            Description = model.Description,
        });

        return RedirectToAction("Index");
    }
    
    [HttpGet("delete/{id:int}")]
    public IActionResult Delete(int id)
    {
        var good = _goodRepository.GetById(id);
        
        if(good == null) return RedirectToAction("Index");
        
        _goodRepository.DeleteById(id);
        
        return RedirectToAction("Index");
    }
}