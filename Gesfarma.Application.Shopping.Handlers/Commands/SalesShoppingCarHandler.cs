using Gesfarma.Application.Shopping.Message.Commands;
using Gesfarma.Application.Shopping.Message.Events;
using Gesfarma.Domain.Clients.Aggregates.Client;
using Gesfarma.Domain.Shoppings.Aggregates.Shopping;
using NServiceBus.Logging;

namespace Gesfarma.Application.Shopping.Handlers.Commands;

public class SalesShoppingCarHandler : IHandleMessages<SalesShoppingCar>
{
    static readonly ILog log = LogManager.GetLogger<SalesShoppingCarHandler>();
    public async Task Handle(SalesShoppingCar command, IMessageHandlerContext context)
    {
        try
        {
            log.Info($"SalesShoppingCarHandler, SalesId = {command.SalesId}");
            var nHibernateSession = context.SynchronizedStorageSession.Session();
            var shoppingCarSaleId = ShoppingId.FromExisting(command.SalesId);
            var clientSaleId = ClientId.FromExisting(command.ClientId);
            var client = nHibernateSession.Get<Client>(clientSaleId);
            if (client == null)
            {
                var clientNotFound = new ClientNotFound(clientSaleId.Id);
                await context.Publish(clientNotFound);
                return;
            }

            //var shopping = nHibernateSession.Get<Domain.Shoppings.Aggregates.Shopping.Shopping>(shoppingCarSaleId);
            //shopping.SetStartSaleProcess();
            //await context.Publish(accountCredited);
        }
        catch (Exception ex)
        {
            log.Error(ex.Message + " ** " + ex.StackTrace);
        }
    }
}
