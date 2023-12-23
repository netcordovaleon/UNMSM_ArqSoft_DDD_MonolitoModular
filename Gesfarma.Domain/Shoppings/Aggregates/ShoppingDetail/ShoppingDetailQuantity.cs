using Gesfarma.Domain.Products.Aggregates.Product;
using Gesfarma.Domain.SharedKernel;
using Gesfarma.Domain.Shoppings.Aggregates.Shopping;

namespace Gesfarma.Domain.Shoppings.Aggregates.ShoppingDetail;

public class ShoppingDetailQuantity : ValueObject<ShoppingDetailQuantity>
{
    public int Stock { get; }

    public ShoppingDetailQuantity()
    {
        Stock = 0;
    }

    public ShoppingDetailQuantity(int stock)
    {
        Stock = stock;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Stock;
    }

    public static ShoppingDetailQuantity SetQuantity(int referenceQuantity)
    {
        return new ShoppingDetailQuantity(referenceQuantity);
    }
}
