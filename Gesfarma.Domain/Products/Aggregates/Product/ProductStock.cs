using Gesfarma.Domain.SharedKernel;

namespace Gesfarma.Domain.Products.Aggregates.Product;

public class ProductStock : ValueObject<ProductStock>
{
    public int Stock { get; }

    public ProductStock()
    {
        Stock = 0;
    }

    public ProductStock(int stock)
    {
        Stock = stock;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Stock;
    }
}
