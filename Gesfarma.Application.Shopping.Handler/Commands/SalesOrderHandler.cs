using Gesfarma.Application.Shopping.Message.Commands;
using Gesfarma.Application.Shopping.Message.Events;
using Gesfarma.Domain.Clients.Aggregates.Client;
using Gesfarma.Domain.Shoppings.Aggregates.Shopping;
using NServiceBus.Logging;

namespace Gesfarma.Application.Shopping.Handler.Commands;

public class SalesOrderHandler : IHandleMessages<SalesOrderCar>
{
    static readonly ILog log = LogManager.GetLogger<SalesOrderHandler>();
    public async Task Handle(SalesOrderCar command, IMessageHandlerContext context)
    {
        try
        {
            log.Info($"SalesShoppingCarHandler, SalesOrderId = {command.SalesOrderId}");
            var nHibernateSession = context.SynchronizedStorageSession.Session();
            var shoppingCarSaleId = ShoppingId.FromExisting(command.SalesOrderId);
            var clientSaleId = ClientId.FromExisting(command.ClientId);
            var client = nHibernateSession.Get<Client>(clientSaleId);
            if (client == null)
            {
                var clientNotFound = new ClientWasNotFound(clientSaleId.Id);
                await context.Publish(clientNotFound);
                return;
            }

            var shopping = nHibernateSession.Get<Domain.Shoppings.Aggregates.Shopping.Shopping>(shoppingCarSaleId);
            if (shopping == null) { 
                shopping = new Domain.Shoppings.Aggregates.Shopping.Shopping();
                shopping.SetId(shoppingCarSaleId);
            }

            shopping.SetClient(clientSaleId);
            shopping.SetStartSaleProcess();
            nHibernateSession.Save(shopping);
            var saleOrderGenerated = new SaleOrderStarted()
            {
                SaleOrderId = shoppingCarSaleId.Id,
                ClientId = clientSaleId.Id,
                Products = (command.Products.Select(c=>new SaleOrderDetailStarted() { 
                    Quantity = c.Quantity,
                    ProductId = c.ProductId
                })).ToList()
            };
            await context.Publish(saleOrderGenerated);
        }
        catch (Exception ex)
        {
            log.Error(ex.Message + " ** " + ex.StackTrace);
        }
    }
}
