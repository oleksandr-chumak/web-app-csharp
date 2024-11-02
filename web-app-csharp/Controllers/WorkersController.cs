using Microsoft.AspNetCore.Mvc;
using web_app_csharp.Models;
using web_app_csharp.Models.Workers;
using web_app_csharp.Repositories;
using web_app_csharp.Services;
using web_app_csharp.Utils;

namespace web_app_csharp.Controllers;

[Route("workers")]
public class WorkersController : Controller
{
    private readonly IWorkerRepository _workerRepository;

    private readonly IDepartmentRepository _departmentRepository;
    
    public event WorkerCreatedEventHandler WorkerCreated;

    public WorkersController(IWorkerRepository workerRepository, IDepartmentRepository departmentRepository,  WorkerLoggingService loggingService)
    {
        _workerRepository = workerRepository;
        _departmentRepository = departmentRepository;
        
        WorkerCreated += loggingService.OnWorkerCreated;
    }

    public IActionResult Index()
    {
        var workers = _workerRepository.GetAll();
        var model = new WorkersListModel()
        {
            Workers = workers
        };

        return View(model);
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        var model = new CreateWorkerModel();
        return View(model);
    }

    [HttpPost("create")]
    public IActionResult Create(CreateWorkerModel model)
    {
        model.Submitted = true;


        if (!ModelState.IsValid)
        {
            model.FieldErrors = ModelErrorUtil.GetErrors(ModelState);
            return View(model);
        }
        
        var department = model.DeptId != null ? _departmentRepository.GetById(model.DeptId.Value) : null;

        if (model.DeptId != null && department == null)
        {
            model.FieldErrors["DeptId"] = [$"Department with id {model.DeptId} not found."];
            return View(model);
        }

        var newWorker = new Worker()
        {
            Name = model.Name,
            Address = model.Address,
            DeptId = model.DeptId,
            Information = model.Information,
        };

        _workerRepository.Add(newWorker);
        
        WorkerCreated?.Invoke(this, newWorker);

        return RedirectToAction("Index");
    }

    [HttpGet("update/{id:int}")]
    public IActionResult Update(int id)
    {
        var worker = _workerRepository.GetById(id);

        if (worker == null) return RedirectToAction("Index");

        var model = new UpdateWorkerModel()
        {
            WorkersId = worker.WorkersId,
            Name = worker.Name,
            Address = worker.Address,
            DeptId = worker.DeptId,
            Information = worker.Information,
        };

        return View(model);
    }

    [HttpPost("update/{id:int}")]
    public IActionResult Update(int id, UpdateWorkerModel model)
    {
        model.Submitted = true;
        model.WorkersId = id;
        var worker = _workerRepository.GetById(id);

        if (worker == null) return RedirectToAction("Index");

        if (!ModelState.IsValid)
        {
            model.FieldErrors = ModelErrorUtil.GetErrors(ModelState);
            return View(model);
        }

        var department = model.DeptId != null ? _departmentRepository.GetById(model.DeptId.Value) : null;

        if (model.DeptId != null && department == null)
        {
            model.FieldErrors["DeptId"] = [$"Department with id {model.DeptId} not found."];
            return View(model);
        }

        _workerRepository.Update(new Worker()
        {
            WorkersId = model.WorkersId,
            Name = model.Name,
            Address = model.Address,
            DeptId = model.DeptId,
            Information = model.Information,
        });

        return RedirectToAction("Index");
    }
    
    [HttpGet("delete/{id:int}")]
    public IActionResult Delete(int id)
    {
        var worker = _workerRepository.GetById(id);
        
        if(worker == null) return RedirectToAction("Index");
        
        _workerRepository.DeleteById(id);
        
        return RedirectToAction("Index");
    }
}