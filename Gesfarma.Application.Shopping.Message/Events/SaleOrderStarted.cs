namespace Gesfarma.Application.Shopping.Message.Events;

public class SaleOrderStarted : IEvent
{
    public string SaleOrderId { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public List<SaleOrderDetailStarted> Products { get; set; }

    public SaleOrderStarted()
    {
        Products = new List<SaleOrderDetailStarted>();
    }

}


public class SaleOrderDetailStarted {
    public string ProductId { get; set; } = string.Empty;
    public int Quantity { get; set; } = 0;
}