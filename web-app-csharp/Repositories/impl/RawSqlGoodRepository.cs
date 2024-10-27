using System.Data.SqlClient;
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
        
        return goods;
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