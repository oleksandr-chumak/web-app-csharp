using Microsoft.Data.SqlClient;
using web_app_csharp.Entities;

namespace web_app_csharp.Repositories.impl;

public class RawSqlGoodRepository : RawSqlRepository, IGoodRepository
{
    public RawSqlGoodRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public IEnumerable<GoodEntity> GetAll()
    {
        var connection = CreateSqlConnection();
        connection.Open();
        var goods = new List<GoodEntity>();
        var command = new SqlCommand("SELECT * FROM dbo.Goods", connection);
        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            goods.Add(new GoodEntity()
            {
                GoodId = Convert.ToInt32(reader["GoodId"]),
                Name = (string)reader["Name"],
                Price = Convert.ToDouble(reader["Price"]),
                Quantity = Convert.ToInt32(reader["Quantity"]),
                Producer = (string)reader["Producer"],
                DeptId = Convert.ToInt32(reader["DeptId"]),
                Description = (string) reader["Description"],
            });
        }
        
        reader.Close();
        connection.Close();
        return goods;
    }

    public GoodEntity? GetById(decimal id)
    {
        var connection = CreateSqlConnection();
        connection.Open();
        var command = new SqlCommand("SELECT * FROM dbo.Goods WHERE GoodId = @id", connection);
        command.Parameters.Add(new SqlParameter("@id", id));
        var reader = command.ExecuteReader();

        if (reader.Read())
        {
            var good = new GoodEntity()
            {
                GoodId = Convert.ToInt32(reader["GoodId"]),
                Name = (string)reader["Name"],
                Price = Convert.ToDouble(reader["Price"]),
                Quantity = Convert.ToInt32(reader["Quantity"]),
                Producer = (string)reader["Producer"],
                DeptId = Convert.ToInt32(reader["DeptId"]),
                Description = reader["Description"] is DBNull ? null : (string)reader["Description"],
            };

            reader.Close();
            connection.Close();
            return good;
        }

        reader.Close();
        connection.Close(); 
        return null; 
    }

    public void Add(GoodEntity obj)
    {
        var connection = CreateSqlConnection();
        connection.Open();
        var command = new SqlCommand("INSERT INTO dbo.Goods (Name, Price, Quantity, Producer, DeptId, Description) VALUES (@new_name, @new_price, @new_quantity, @new_producer, @new_dept_id, @new_description)", connection);

        command.Parameters.Add(new SqlParameter("@new_name", obj.Name));
        command.Parameters.Add(new SqlParameter("@new_price", obj.Price));
        command.Parameters.Add(new SqlParameter("@new_quantity", obj.Quantity));
        command.Parameters.Add(new SqlParameter("@new_producer", obj.Producer));
        command.Parameters.Add(new SqlParameter("@new_dept_id", obj.DeptId));
        command.Parameters.Add(new SqlParameter("@new_description", obj.Description ?? (object)DBNull.Value)); // Handle null Description

        command.ExecuteNonQuery();
        connection.Close(); // Close the connection after execution
    }

    public void Update(GoodEntity model)
    {
        var connection = CreateSqlConnection();
        connection.Open();

        var command = new SqlCommand(
            "UPDATE dbo.Goods SET Name = @new_name, Price = @new_price, Quantity = @new_quantity, Producer = @new_producer, DeptId = @new_dept_id, Description = @new_description WHERE GoodId = @good_id",
            connection
        );

        command.Parameters.Add(new SqlParameter("@good_id", model.GoodId));
        command.Parameters.Add(new SqlParameter("@new_name", model.Name));
        command.Parameters.Add(new SqlParameter("@new_price", model.Price));
        command.Parameters.Add(new SqlParameter("@new_quantity", model.Quantity));
        command.Parameters.Add(new SqlParameter("@new_producer", model.Producer));
        command.Parameters.Add(new SqlParameter("@new_dept_id", model.DeptId));
        command.Parameters.Add(new SqlParameter("@new_description", model.Description ?? (object)DBNull.Value)); 

        command.ExecuteNonQuery();
        connection.Close(); 
    }

    public void DeleteById(int id)
    {
        var connection = CreateSqlConnection();
        connection.Open();

        var command = new SqlCommand("DELETE FROM dbo.Goods WHERE GoodId = @good_id", connection);
        command.Parameters.Add(new SqlParameter("@good_id", id));

        command.ExecuteNonQuery();
        connection.Close();
    }

    public int GetCountOfGoodsBelowAveragePrice()
    {
        var connection = CreateSqlConnection();
        connection.Open();
        var command = new SqlCommand("SELECT dbo.LESS_THAN_AVG() as CountOfGoodsBelowAveragePrice;", connection);
        var reader  = command.ExecuteReader();
        
        return reader.Read() ? Convert.ToInt32(reader["CountOfGoodsBelowAveragePrice"]) : 0;
    }
}