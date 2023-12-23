using Gesfarma.Application.SaleOrder.Handler.Sagas.SagaData;
using Gesfarma.Application.SaleOrder.Message.Commands;
using Gesfarma.Application.Shopping.Message.Commands;
using Gesfarma.Application.Shopping.Message.Events;
using Microsoft.Extensions.Logging;
using NServiceBus.Logging;

namespace Gesfarma.Application.SaleOrder.Handler.Sagas;

public class SaleOrderSaga : Saga<SaleOrderSagaData>,
    IAmStartedByMessages<SaleOrderStarted>
{

    static readonly ILog log = LogManager.GetLogger<SaleOrderSaga>();
    private readonly ILogger<SaleOrderSaga> _logger;

    public SaleOrderSaga(ILogger<SaleOrderSaga> logger)
        => _logger = logger;
    public async Task Handle(SaleOrderStarted message, IMessageHandlerContext context)
    {
        try
        {
            _logger.LogInformation($"Saga SaleOrderGenerated, SaleOrderId = {message.SaleOrderId}");
            Data.SaleOrderId = message.SaleOrderId;
            Data.ClientId = message.ClientId;

            var command = new ProductsToSaleOrder()
            {
                SalesOrderId = message.SaleOrderId,
                Products = message.Products.Select(c => new ProductToSaleOrder()
                {
                    ProductId = c.ProductId,
                    Quantity = c.Quantity
                }).ToList()
            };
            await context.Send(command).ConfigureAwait(false);
            MarkAsComplete();
        }
        catch (Exception ex)
        {
            log.Error(ex.Message + " ** " + ex.StackTrace);
        }
    }


    protected override void ConfigureHowToFindSaga(SagaPropertyMapper<SaleOrderSagaData> mapper)
    {
        mapper.MapSaga(saga => saga.SaleOrderId)
            .ToMessage<SaleOrderStarted>(message => message.SaleOrderId);
    }
}
