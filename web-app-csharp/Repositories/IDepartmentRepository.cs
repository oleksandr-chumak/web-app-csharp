using web_app_csharp.Entities;

namespace web_app_csharp.Repositories;

public interface IDepartmentRepository
{
    IEnumerable<DepartmentEntity> GetAll();
    
    void Add(DepartmentEntity department);
}