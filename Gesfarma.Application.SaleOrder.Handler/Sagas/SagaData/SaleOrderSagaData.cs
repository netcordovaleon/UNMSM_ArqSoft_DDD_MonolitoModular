namespace Gesfarma.Application.SaleOrder.Handler.Sagas.SagaData;

public class SaleOrderSagaData : ContainSagaData
{
    public virtual string SaleOrderId { get; set; } = string.Empty;
    public virtual string ClientId { get; set; } = string.Empty;
    public virtual int State { get; set; } = 0;
}
