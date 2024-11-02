using Microsoft.AspNetCore.Mvc;
using web_app_csharp.Entities;
using web_app_csharp.Models.Sales;
using web_app_csharp.Repositories;
using web_app_csharp.Utils;

namespace web_app_csharp.Controllers;

[Route("sales")]
public class SalesController : Controller
{
    private readonly ISaleRepository _saleRepository;

    private readonly IGoodRepository _goodRepository;

    public SalesController(ISaleRepository saleRepository, IGoodRepository goodRepository)
    {
        _saleRepository = saleRepository;
        _goodRepository = goodRepository;
    }

    public IActionResult Index()
    {
        var sales = _saleRepository.GetAll();
        var model = new SalesListModel()
        {
            Sales = sales
        };

        return View(model);
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        var model = new CreateSaleModel();
        return View(model);
    }

    [HttpPost("create")]
    public IActionResult Create(CreateSaleModel model)
    {
        model.Submitted = true;

        if (ModelState.IsValid)
        {
            var foundGood = _goodRepository.GetById(model.GoodId);

            if (foundGood != null)
            {
                _saleRepository.Add(new SaleEntity()
                {
                    CheckNo = model.CheckNo,
                    GoodId = model.GoodId,
                    DateSale = model.DateSale,
                    Quantity = model.Quantity
                });
                return RedirectToAction("Index");
            }

            model.FieldErrors["GoodId"] = new List<string> { $"Good with id {model.GoodId} not found." };

            return View(model);
        }

        model.FieldErrors = ModelErrorUtil.GetErrors(ModelState);

        return View(model);
    }

    [HttpGet("update/{id:int}")]
    public IActionResult Update(int id)
    {
        var sale = _saleRepository.GetById(id);

        if (sale == null) return RedirectToAction("Index");

        var model = new UpdateSaleModel()
        {
            SaleId = sale.SalesId,
            CheckNo = sale.CheckNo,
            GoodId = sale.GoodId,
            DateSale = sale.DateSale,
            Quantity = sale.Quantity,
        };

        return View(model);
    }

    [HttpPost("update/{id:int}")]
    public IActionResult Update(int id, UpdateSaleModel model)
    {
        model.Submitted = true;
        model.SaleId = id;
        var sale = _saleRepository.GetById(id);

        if (sale == null) return RedirectToAction("Index");

        if (!ModelState.IsValid)
        {
            model.FieldErrors = ModelErrorUtil.GetErrors(ModelState);
            return View(model);
        }

        var foundGood = _goodRepository.GetById(model.GoodId);

        if (foundGood == null)
        {
            model.FieldErrors["GoodId"] = [$"Good with id {model.GoodId} not found."];
            return View(model);
        }

        _saleRepository.Update(new SaleEntity()
        {
            SalesId = id,
            CheckNo = model.CheckNo,
            GoodId = model.GoodId,
            DateSale = model.DateSale,
            Quantity = model.Quantity,
        });

        return RedirectToAction("Index");
    }
    
    [HttpGet("delete/{id:int}")]
    public IActionResult Delete(int id)
    {
        var sale = _saleRepository.GetById(id);
        
        if(sale == null) return RedirectToAction("Index");
        
        _saleRepository.DeleteById(id);
        
        return RedirectToAction("Index");
    }
}