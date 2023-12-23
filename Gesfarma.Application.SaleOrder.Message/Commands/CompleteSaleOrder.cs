namespace Gesfarma.Application.SaleOrder.Message.Commands;

public class CompleteSaleOrder : ICommand
{
    public string SaleOrderId { get; set; } = string.Empty;
}
