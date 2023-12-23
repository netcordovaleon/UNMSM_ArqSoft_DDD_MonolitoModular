
namespace Gesfarma.Application.Shopping.Message.Commands;

public class SalesOrderCar : ICommand
{
    public string SalesOrderId { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public List<SalesOrderCarDetail> Products { get; set; }

    public SalesOrderCar()
    {
        Products = new List<SalesOrderCarDetail>();
    }
}

public class SalesOrderCarDetail {

    public string ProductId { get; set; } = string.Empty;
    public int Quantity { get; set; } = 0;

}