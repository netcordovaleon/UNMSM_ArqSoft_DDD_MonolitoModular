using Gesfarma.Application.SaleOrder.Handler.Sagas.SagaData;
using Gesfarma.Application.SaleOrder.Message.Commands;
using Gesfarma.Application.Shopping.Message.Events;
using Microsoft.Extensions.Logging;
using NServiceBus.Logging;


namespace Gesfarma.Application.SaleOrder.Handler.Sagas;

public class SaleOrderDetailSaga : 
    Saga<SaleOrderDetailSagaData>,
    IAmStartedByMessages<ProductsInSaleOrderAdded>
{

    static readonly ILog log = LogManager.GetLogger<SaleOrderSaga>();
    private readonly ILogger<SaleOrderDetailSaga> _logger;

    public SaleOrderDetailSaga(ILogger<SaleOrderDetailSaga> logger)
    => _logger = logger;

    public async Task Handle(ProductsInSaleOrderAdded message, IMessageHandlerContext context)
    {
        try
        {
            _logger.LogInformation($"Saga SaleOrderDetail, SaleOrderId = {message.SaleOrderId}");
            Data.SaleOrderId = message.SaleOrderId;
            var command = new CompleteSaleOrder()
            {
                SaleOrderId = message.SaleOrderId
            };
            await context.SendLocal(command).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            log.Error(ex.Message + " ** " + ex.StackTrace);
        }
    }

    protected override void ConfigureHowToFindSaga(SagaPropertyMapper<SaleOrderDetailSagaData> mapper)
    {
        mapper.MapSaga(saga => saga.SaleOrderId).ToMessage<ProductsInSaleOrderAdded>(message => message.SaleOrderId);
    }
}
