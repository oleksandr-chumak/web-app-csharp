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
}