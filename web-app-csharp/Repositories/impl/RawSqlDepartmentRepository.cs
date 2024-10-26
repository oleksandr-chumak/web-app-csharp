using System.Data;
using System.Data.SqlClient;
using web_app_csharp.Attributes;
using web_app_csharp.Entities;

namespace web_app_csharp.Repositories.impl;

[ScopedService]
public class RawSqlDepartmentRepository : RawSqlRepository, IDepartmentRepository
{
    public RawSqlDepartmentRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public IEnumerable<DepartmentEntity> GetAll()
    {
        var connection = CreateSqlConnection();
        connection.Open();
        var departments = new List<DepartmentEntity>();
        var command = new SqlCommand("SELECT * FROM dbo.Departments", connection);
        var reader = command.ExecuteReader();
        
        while (reader.Read())
        {
            departments.Add(new DepartmentEntity()
            {
                DeptId = Convert.ToInt32(reader["DeptId"]),
                Name = (string)reader["Name"],
                Info = reader["Info"] is DBNull ? null : (string)reader["Info"]
            });
        }
        
        reader.Close();
        
        return departments;
    }

    public void Add(DepartmentEntity department)
    {
        var connection = CreateSqlConnection();
        connection.Open();
        var command = new SqlCommand("usp_insert_depd", connection);
        command.CommandType = CommandType.StoredProcedure;
        
        command.Parameters.Add(new SqlParameter("@new_name", department.Name));
        command.Parameters.Add(new SqlParameter("@new_info", department.Info));
        
        command.ExecuteNonQuery();
    }
}