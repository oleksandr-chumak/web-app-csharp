using web_app_csharp.Attributes;
using web_app_csharp.Entities;
using web_app_csharp.Models;

namespace web_app_csharp.Repositories.impl;

[ScopedService]
public class EfDepartmentRepository : IDepartmentRepository
{
    private readonly ApplicationDbContext _context;

    public EfDepartmentRepository(ApplicationDbContext applicationDbContext)
    {
        _context = applicationDbContext;
    }

    public IEnumerable<DepartmentEntity> GetAll()
    {
        return _context.Departments
            .Select(d => new DepartmentEntity
            {
                DeptId = d.DeptId,
                Name = d.Name,
                Info = d.Info
            })
            .ToList();
    }

    public DepartmentEntity? GetById(decimal id)
    {
        var department = _context.Departments
            .FirstOrDefault(d => d.DeptId == id);

        if (department == null)
        {
            return null; 
        }

        return new DepartmentEntity
        {
            DeptId = department.DeptId,
            Name = department.Name,
            Info = department.Info
        };
    }

    public void Add(DepartmentEntity department)
    {
        var newDepartment = new Department
        {
            Name = department.Name,
            Info = department.Info
        };

        _context.Departments.Add(newDepartment);
        _context.SaveChanges();
    }

    public void Update(DepartmentEntity model)
    {
        var existingDepartment = _context.Departments
            .FirstOrDefault(d => d.DeptId == model.DeptId);

        if (existingDepartment != null)
        {
            existingDepartment.Name = model.Name;
            existingDepartment.Info = model.Info;

            _context.SaveChanges();
        }
        else
        {
            throw new KeyNotFoundException($"Department with ID {model.DeptId} not found.");
        }
    }

    public void DeleteById(int id)
    {
        var departmentToDelete = _context.Departments
            .FirstOrDefault(d => d.DeptId == id);

        if (departmentToDelete != null)
        {
            _context.Departments.Remove(departmentToDelete);
            _context.SaveChanges();
        }
        else
        {
            throw new KeyNotFoundException($"Department with ID {id} not found.");
        }
    }
}