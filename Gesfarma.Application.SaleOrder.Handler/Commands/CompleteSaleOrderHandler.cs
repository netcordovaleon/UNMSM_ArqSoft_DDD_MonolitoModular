using Gesfarma.Application.SaleOrder.Message.Commands;
using Gesfarma.Domain.Shoppings.Aggregates.Shopping;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Gesfarma.Application.SaleOrder.Handler.Commands;

public class CompleteSaleOrderHandler : IHandleMessages<CompleteSaleOrder>
{
    static readonly ILog log = LogManager.GetLogger<CompleteSaleOrderHandler>();

    public async Task Handle(CompleteSaleOrder message, IMessageHandlerContext context)
    {
        try
        {
            log.Info($"CompleteSaleOrderHandler, SaleOrderId = {message.SaleOrderId}");
            var saleOrderId = ShoppingId.FromExisting(message.SaleOrderId);
            var nHibernateSession = context.SynchronizedStorageSession.Session();
            var shopping = nHibernateSession.Get<Gesfarma.Domain.Shoppings.Aggregates.Shopping.Shopping>(saleOrderId);
            shopping.SetStartSaleCompleted();
            nHibernateSession.Save(shopping);
        }
        catch (Exception ex)
        {
            log.Error(ex.Message + " ** " + ex.StackTrace);
        }
    }
}
