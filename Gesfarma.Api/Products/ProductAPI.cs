using Carter;
using Gesfarma.Application.Products.GetProducts;
using MediatR;

namespace Gesfarma.Api.Products;

public class ProductAPI : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/products", async (ISender sender) =>
        {
            var query = new GetProducts();
            var result = await sender.Send(query);
            return Results.Ok(result);
        });
    }
}
