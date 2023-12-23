using Carter;
using Gesfarma.Application.Clients.GetClients;
using MediatR;

namespace Gesfarma.Api.Clients;

public class ClientAPI : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/clients", async (ISender sender) =>
        {
            var query = new GetClients();
            var result = await sender.Send(query);
            return Results.Ok(result);
        });
    }
}
