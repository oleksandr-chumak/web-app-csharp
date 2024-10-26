using Microsoft.AspNetCore.Mvc;
using web_app_csharp.Entities;
using web_app_csharp.Models.Departments;
using web_app_csharp.Repositories;

namespace web_app_csharp.Controllers;

[Route("/departments")]
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

    [HttpGet]
    [Route("[controller]/create")]
    public IActionResult Create()
    {
        var model = new CreateDepartmentModel();
        return View(model);
    }

    [HttpPost]
    [Route("[controller]/create")]
    public IActionResult Create(CreateDepartmentModel viewModel)
    {
        if (ModelState.IsValid)
        {
            _departmentRepository.Add(new DepartmentEntity()
            {
                Name = viewModel.Name,
                Info = viewModel.Info
            });
            return RedirectToAction("Index");
        }

        foreach (var key in ModelState.Keys)
        {
            var errors = ModelState[key]?.Errors;
            if (errors == null || errors.Count == 0) continue;

            viewModel.FieldErrors[key] = new List<string>();
            foreach (var error in errors)
            {
                viewModel.FieldErrors[key].Add(error.ErrorMessage);
            }
        }

        return View(viewModel);
    }
}