using System.Data;
using Microsoft.Data.SqlClient;
using web_app_csharp.Entities;

namespace web_app_csharp.Repositories.impl;

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
        connection.Close();
        
        return departments;
    }

    public DepartmentEntity? GetById(decimal id)
    {
        var connection = CreateSqlConnection();
        connection.Open();
        var command = new SqlCommand("SELECT * FROM dbo.Departments WHERE DeptId = @id", connection);
        command.Parameters.Add(new SqlParameter("@id", id));
        var reader = command.ExecuteReader();

        if (reader.Read())
        {
            var department = new DepartmentEntity()
            {
                DeptId = Convert.ToInt32(reader["DeptId"]),
                Name = (string)reader["Name"],
                Info = reader["Info"] is DBNull ? null : (string)reader["Info"]
            };

            reader.Close();
            connection.Close();
            return department;
        }

        reader.Close();
        connection.Close(); 
        return null; 
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
        connection.Close();
    }

    public void Update(DepartmentEntity model)
    {
        var connection = CreateSqlConnection();
        connection.Open();

        var command = new SqlCommand(
            "UPDATE dbo.Departments SET Name = @new_name, Info = @new_info WHERE DeptId = @dept_id", 
            connection
        );

        command.Parameters.Add(new SqlParameter("@dept_id", model.DeptId));
        command.Parameters.Add(new SqlParameter("@new_name", model.Name));
        command.Parameters.Add(new SqlParameter("@new_info", model.Info ?? (object)DBNull.Value)); // Handle null Info

        command.ExecuteNonQuery();
        connection.Close();
    }

    public void DeleteById(int id)
    {
        var connection = CreateSqlConnection();
        connection.Open();

        var command = new SqlCommand("DELETE FROM dbo.Departments WHERE DeptId = @dept_id", connection);
        command.Parameters.Add(new SqlParameter("@dept_id", id));

        command.ExecuteNonQuery();
        connection.Close();
    }
}