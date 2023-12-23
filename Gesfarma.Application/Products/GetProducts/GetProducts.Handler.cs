using Gesfarma.Infrastructure.Persistence;
using MediatR;
using Microsoft.Data.SqlClient;

namespace Gesfarma.Application.Products.GetProducts;

public class GetProductsHandler : IRequestHandler<GetProducts, List<GetProductsResponse>>
{
    private readonly SqlConnectionFactory _connectionFactory;

    public GetProductsHandler(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<List<GetProductsResponse>> Handle(GetProducts request, CancellationToken cancellationToken)
    {
        List<GetProductsResponse> result = new List<GetProductsResponse>();
        string sql = @"
                SELECT 
                    id, product_name, price, stock 
                FROM product";

        using (SqlConnection connection = _connectionFactory.OpenConnection())
        {

            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                connection.Open();
                var reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    var productResponse = new GetProductsResponse();
                    productResponse.Id = reader.GetString(reader.GetOrdinal("id"));
                    productResponse.Name = reader.GetString(reader.GetOrdinal("product_name"));
                    productResponse.Price = reader.GetDecimal(reader.GetOrdinal("price"));
                    productResponse.Stock = reader.GetInt32(reader.GetOrdinal("stock"));
                    result.Add(productResponse);
                }
                connection.Close();
            }
        }
        return result;
    }
}
