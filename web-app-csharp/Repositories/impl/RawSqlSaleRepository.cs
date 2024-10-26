using System.Data.SqlClient;
using web_app_csharp.Attributes;
using web_app_csharp.Entities;

namespace web_app_csharp.Repositories.impl;

[ScopedService]
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

        return null;
    }
}