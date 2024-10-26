using web_app_csharp.Entities;

namespace web_app_csharp.Models.Departments;

public class DepartmentsListModel
{
    public string Title { get; set; }

    public IEnumerable<DepartmentEntity> Departments { get; set; }
}