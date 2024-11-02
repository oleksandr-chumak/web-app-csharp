using Microsoft.AspNetCore.Mvc;
using web_app_csharp.Entities;
using web_app_csharp.Models.Departments;
using web_app_csharp.Repositories;
using web_app_csharp.Utils;

namespace web_app_csharp.Controllers;

[Route("departments")]
public class DepartmentsController : Controller
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentsController(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var departments = _departmentRepository.GetAll();
        var model = new DepartmentsListModel()
        {
            Title = "Departments",
            Departments = departments
        };

        return View(model);
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        var model = new CreateDepartmentModel();
        return View(model);
    }

    [HttpPost("create")]
    public IActionResult Create(CreateDepartmentModel viewModel)
    {
        viewModel.Submitted = true;
        
        if (!ModelState.IsValid)
        {
            viewModel.FieldErrors = ModelErrorUtil.GetErrors(ModelState);
            return View(viewModel);
        }
        
        _departmentRepository.Add(new DepartmentEntity()
        {
            Name = viewModel.Name,
            Info = viewModel.Info
        });
        
        return RedirectToAction("Index");
    }

    [HttpGet("update/{id:int}")]
    public IActionResult Update(int id)
    {
        var department = _departmentRepository.GetById(id);

        if (department == null) return RedirectToAction("Index");

        var model = new UpdateDepartmentModel()
        {
            DeptId = department.DeptId,
            Name = department.Name,
            Info = department.Info
        };

        return View(model);
    }

    [HttpPost("update/{id:int}")]
    public IActionResult Update(int id, UpdateDepartmentModel model)
    {
        model.Submitted = true;
        var department = _departmentRepository.GetById(id);

        if (department == null) return RedirectToAction("Index");

        if (!ModelState.IsValid)
        {
            model.FieldErrors = ModelErrorUtil.GetErrors(ModelState);
            return View();
        }

        _departmentRepository.Update(new DepartmentEntity()
        {
            DeptId = id,
            Name = model.Name,
            Info = model.Info,
        });

        return RedirectToAction("Index");
    }
    
    [HttpGet("delete/{id:int}")]
    public IActionResult Delete(int id)
    {
        var department = _departmentRepository.GetById(id);
        
        if(department == null) return RedirectToAction("Index");
        
        _departmentRepository.DeleteById(id);
        
        return RedirectToAction("Index");
    }
}