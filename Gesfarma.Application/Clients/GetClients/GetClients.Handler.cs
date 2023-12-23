using Gesfarma.Infrastructure.Persistence;
using MediatR;
using Microsoft.Data.SqlClient;


namespace Gesfarma.Application.Clients.GetClients;

public class GetClientsHandler : IRequestHandler<GetClients, List<GetClientsResponse>>
{

    private readonly SqlConnectionFactory _connectionFactory;

    public GetClientsHandler(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    public async Task<List<GetClientsResponse>> Handle(GetClients request, CancellationToken cancellationToken)
    {
        List<GetClientsResponse> result = new List<GetClientsResponse>();
        string sql = @"
                SELECT id, first_name, last_name
                FROM clients
                ORDER BY last_name, first_name;";

        using (SqlConnection connection = _connectionFactory.OpenConnection())
        {

            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                connection.Open();
                var reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    var clientsResponse = new GetClientsResponse();
                    clientsResponse.Id = reader.GetString(reader.GetOrdinal("id"));
                    clientsResponse.FirstName = reader.GetString(reader.GetOrdinal("first_name"));
                    clientsResponse.LastName = reader.GetString(reader.GetOrdinal("last_name"));
                    result.Add(clientsResponse);
                }
                connection.Close();
            }
        }
        return result;
    }
}
