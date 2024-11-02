using Microsoft.Data.SqlClient;
using web_app_csharp.Entities;

namespace web_app_csharp.Repositories.impl;

public class RawSqlSaleRepository : RawSqlRepository, ISaleRepository
{
    public RawSqlSaleRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public SaleEntity? GetLargestSaleByGoodName(string goodName)
    {
        var connection = CreateSqlConnection();
        connection.Open();
        var command = new SqlCommand("SELECT * FROM GetLargestOrderByProductName(@productName)", connection);
        command.Parameters.AddWithValue("@productName", goodName);
        
        var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new SaleEntity()
            {
                SalesId = Convert.ToInt32(reader["SalesId"]),
                CheckNo = Convert.ToInt32(reader["CheckNo"]),
                GoodId = Convert.ToInt32(reader["GoodId"]),
                DateSale = Convert.ToDateTime(reader["DateSale"]),
                Quantity = Convert.ToInt32(reader["Quantity"]),
            };
        }
        
        reader.Close();
        connection.Close();

        return null;
    }

    public IEnumerable<SaleEntity> GetAll()
    {
        var connection = CreateSqlConnection();
        connection.Open();
        var sales = new List<SaleEntity>();
        var command = new SqlCommand("SELECT * FROM dbo.Sales", connection);
        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            sales.Add(new SaleEntity()
            {
                SalesId = Convert.ToInt32(reader["SalesId"]),
                CheckNo = Convert.ToInt32(reader["CheckNo"]),
                GoodId = Convert.ToInt32(reader["GoodId"]),
                DateSale = Convert.ToDateTime(reader["DateSale"]),
                Quantity = Convert.ToInt32(reader["Quantity"]),
            });
        }

        reader.Close();
        connection.Close();
        return sales;
    }

    public SaleEntity? GetById(decimal id)
    {
        var connection = CreateSqlConnection();
        connection.Open();
        var command = new SqlCommand("SELECT * FROM dbo.Sales WHERE SalesId = @id", connection);
        command.Parameters.Add(new SqlParameter("@id", id));
        var reader = command.ExecuteReader();

        if (reader.Read())
        {
            var sale = new SaleEntity()
            {
                SalesId = Convert.ToInt32(reader["SalesId"]),
                CheckNo = Convert.ToInt32(reader["CheckNo"]),
                GoodId = Convert.ToInt32(reader["GoodId"]),
                DateSale = Convert.ToDateTime(reader["DateSale"]),
                Quantity = Convert.ToInt32(reader["Quantity"]),
            };

            reader.Close();
            connection.Close(); // Close the connection after reading
            return sale;
        }

        reader.Close();
        connection.Close(); // Close the connection after reading
        return null; // Not found
    }

    public void Add(SaleEntity obj)
    {
        var connection = CreateSqlConnection();
        connection.Open();
        var command = new SqlCommand("INSERT INTO dbo.Sales (CheckNo, GoodId, DateSale, Quantity) VALUES (@new_check_no, @new_good_id, @new_date_sale, @new_quantity)", connection);

        command.Parameters.Add(new SqlParameter("@new_check_no", obj.CheckNo));
        command.Parameters.Add(new SqlParameter("@new_good_id", obj.GoodId));
        command.Parameters.Add(new SqlParameter("@new_date_sale", obj.DateSale));
        command.Parameters.Add(new SqlParameter("@new_quantity", obj.Quantity));

        command.ExecuteNonQuery();
        connection.Close();
    }


    public void Update(SaleEntity model)
    {
        var connection = CreateSqlConnection();
        connection.Open();

        var command = new SqlCommand(
            "UPDATE dbo.Sales SET CheckNo = @new_check_no, GoodId = @new_good_id, DateSale = @new_date_sale, Quantity = @new_quantity WHERE SalesId = @sales_id",
            connection
        );

        command.Parameters.Add(new SqlParameter("@sales_id", model.SalesId));
        command.Parameters.Add(new SqlParameter("@new_check_no", model.CheckNo));
        command.Parameters.Add(new SqlParameter("@new_good_id", model.GoodId));
        command.Parameters.Add(new SqlParameter("@new_date_sale", model.DateSale));
        command.Parameters.Add(new SqlParameter("@new_quantity", model.Quantity));

        command.ExecuteNonQuery();
        connection.Close();
    }
    public void DeleteById(int id)
    {
        var connection = CreateSqlConnection();
        connection.Open();

        var command = new SqlCommand("DELETE FROM dbo.Sales WHERE SalesId = @salesId", connection);
        command.Parameters.Add(new SqlParameter("@salesId", id));

        command.ExecuteNonQuery();
        connection.Close();
    }
}