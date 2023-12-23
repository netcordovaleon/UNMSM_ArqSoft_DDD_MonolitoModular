namespace Gesfarma.Application.SaleOrder.Message.Events;

public class SaleOrderComplete : IEvent
{
    public string SaleOrderId { get; set; } = string.Empty;
}
