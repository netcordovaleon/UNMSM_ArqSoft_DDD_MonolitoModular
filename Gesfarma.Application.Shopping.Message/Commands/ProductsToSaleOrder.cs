namespace Gesfarma.Application.Shopping.Message.Commands;

public class ProductsToSaleOrder : ICommand
{
    public string SalesOrderId { get; set; }

    public List<ProductToSaleOrder> Products { get; set; }

}

public class ProductToSaleOrder {

    public string ProductId { get; set; }

    public int Quantity { get; set; }

}
