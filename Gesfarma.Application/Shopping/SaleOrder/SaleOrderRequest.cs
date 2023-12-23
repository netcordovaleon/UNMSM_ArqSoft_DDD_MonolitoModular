namespace Gesfarma.Application.Shopping.SaleOrder;

public class SaleOrderRequest
{
    public string SalesOrderId { get; set; }
    public string ClientId { get; set; }
    public List<SalesOrderDetailRequest> Products { get; set; }
    public SaleOrderRequest()
    {
        Products = new List<SalesOrderDetailRequest>();
    }
}

public class SalesOrderDetailRequest
{
    public string ProductId { get; set; }
    public int Quantity { get; set; }
}