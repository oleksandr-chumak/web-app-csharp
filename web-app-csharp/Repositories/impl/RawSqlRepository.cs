using System.Data.SqlClient;

namespace web_app_csharp.Repositories.impl;

public class RawSqlRepository 
{
    private string ConnectionString { get; }

    public RawSqlRepository(IConfiguration configuration)
    {
        ConnectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException();
    }

    public SqlConnection CreateSqlConnection()
    {
        return new SqlConnection(ConnectionString);
    }
        
}