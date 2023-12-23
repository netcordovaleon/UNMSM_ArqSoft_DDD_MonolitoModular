using Carter;
using Gesfarma.Application.Shopping;
using Gesfarma.Application.Shopping.Message.Commands;
using Gesfarma.Application.Shopping.SaleOrder;
using System.Net;

namespace Gesfarma.Api.Shopping;

public class SaleOrderAPI : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/order/sale", async (SaleOrderRequest request, IMessageSession _messageSession) =>
        {
            var saleOrderId = Guid.NewGuid().ToString();
            var command = new SalesOrderCar
            {
                SalesOrderId = saleOrderId,
                ClientId = request.ClientId,
                Products = (request.Products.Select(c => new SalesOrderCarDetail() {
                    ProductId = c.ProductId,
                    Quantity = c.Quantity
                })).ToList()
            };
            try
            {
                await _messageSession.Send(command).ConfigureAwait(false);
                var response = new SaleOrderResponse
                {
                    SalesOrderId = command.SalesOrderId,
                    ClientId = command.ClientId

                };
                return Results.Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Results.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        });
    }
}
