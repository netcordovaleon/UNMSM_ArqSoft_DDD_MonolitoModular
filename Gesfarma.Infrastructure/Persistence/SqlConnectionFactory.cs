using Microsoft.Data.SqlClient;
namespace Gesfarma.Infrastructure.Persistence;

public class SqlConnectionFactory
{
    readonly string? connectionString = Environment.GetEnvironmentVariable("SQL_CONNECTION"); //"Server=localhost\\SQLEXPRESS;Database=Gesfarma;Integrated Security=true;TrustServerCertificate=true;";//

    public SqlConnection OpenConnection()
    {
        return new SqlConnection(connectionString);
    }
}
