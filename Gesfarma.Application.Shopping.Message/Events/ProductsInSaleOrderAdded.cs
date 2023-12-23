namespace Gesfarma.Application.Shopping.Message.Events;
public class ProductsInSaleOrderAdded : IEvent
{
    public string SaleOrderId { get; set; }

}
