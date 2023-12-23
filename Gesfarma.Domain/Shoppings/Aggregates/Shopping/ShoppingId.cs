using Gesfarma.Domain.Products.Aggregates.Product;
using Gesfarma.Domain.SharedKernel;


namespace Gesfarma.Domain.Shoppings.Aggregates.Shopping;

public class ShoppingId : Identity
{
    protected ShoppingId()
    {
    }
    private ShoppingId(string referencedId) : base(referencedId)
    {
    }

    public static ShoppingId FromExisting(string referencedId)
    {
        return new ShoppingId(referencedId);
    }
}
