using Gesfarma.Application.Shopping.Message.Commands;
using Gesfarma.Application.Shopping.Message.Events;
using Gesfarma.Domain.Clients.Aggregates.Client;
using Gesfarma.Domain.Shoppings.Aggregates.Shopping;
using NServiceBus.Logging;

namespace Gesfarma.Application.Shopping.Handler.Commands;

public class ProductsToSaleOrderHandler : IHandleMessages<ProductsToSaleOrder>
{
    static readonly ILog log = LogManager.GetLogger<SalesOrderHandler>();

    public async Task Handle(ProductsToSaleOrder command, IMessageHandlerContext context)
    {
        try
        {
            log.Info($"ProductsToSaleOrderHandler, ProductsToSaleOrder = {command.SalesOrderId}");
            var nHibernateSession = context.SynchronizedStorageSession.Session();

            foreach (var item in command.Products)
            {
                var shoppingDetail = new Domain.Shoppings.Aggregates.ShoppingDetail.ShoppingDetail();
                shoppingDetail.GenerateId();
                shoppingDetail.SetShoppingReference(command.SalesOrderId, item.ProductId, item.Quantity);
                nHibernateSession.Save(shoppingDetail);
            }

            var saleOrderGenerated = new ProductsInSaleOrderAdded()
            {
                SaleOrderId = command.SalesOrderId
            };
            await context.Publish(saleOrderGenerated);
        }
        catch (Exception ex)
        {
            log.Error(ex.Message + " ** " + ex.StackTrace);
        }
    }
}
