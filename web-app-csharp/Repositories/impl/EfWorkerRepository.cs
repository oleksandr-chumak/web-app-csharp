using web_app_csharp.Attributes;
using web_app_csharp.Models;

namespace web_app_csharp.Repositories.impl;

[ScopedService]
public class EfWorkerRepository : IWorkerRepository
{
    private readonly ApplicationDbContext _context;

    public EfWorkerRepository(ApplicationDbContext applicationDbContext)
    {
        _context = applicationDbContext;
    }

    public IEnumerable<Worker> GetAll()
    {
        return _context.Workers
            .Select(w => new Worker
            {
                WorkersId = w.WorkersId,
                Name = w.Name,
                Address = w.Address,
                DeptId = w.DeptId,
                Information = w.Information,
                Dept = w.Dept
            })
            .ToList();
    }

    public Worker? GetById(decimal id)
    {
        return _context.Workers
            .Where(w => w.WorkersId == id)
            .Select(w => new Worker
            {
                WorkersId = w.WorkersId,
                Name = w.Name,
                Address = w.Address,
                DeptId = w.DeptId,
                Information = w.Information,
                Dept = w.Dept
            })
            .FirstOrDefault();
    }

    public void Add(Worker obj)
    {
        _context.Workers.Add(obj);
        _context.SaveChanges();
    }

    public void Update(Worker model)
    {
        var existingWorker = _context.Workers
            .FirstOrDefault(w => w.WorkersId == model.WorkersId);
        
        if (existingWorker != null)
        {
            existingWorker.Name = model.Name;
            existingWorker.Address = model.Address;
            existingWorker.DeptId = model.DeptId;
            existingWorker.Information = model.Information;
            
            _context.SaveChanges();
        }
        else
        {
            throw new KeyNotFoundException($"Worker with ID {model.WorkersId} not found.");
        }
    }

    public void DeleteById(int id)
    {
        var workerToDelete = _context.Workers.Find(id);
        if (workerToDelete != null)
        {
            _context.Workers.Remove(workerToDelete);
            _context.SaveChanges();
        }
        else
        {
            throw new KeyNotFoundException($"Worker with ID {id} not found.");
        }
    }
}