using Gesfarma.Domain.SharedKernel;

namespace Gesfarma.Domain.Shoppings.Aggregates.ShoppingDetail;

public class ShoppingDetailId : Identity
{
    protected ShoppingDetailId()
    {
    }
    private ShoppingDetailId(string referencedId) : base(referencedId)
    {
    }
    public static ShoppingDetailId FromExisting(string referencedId)
    {
        return new ShoppingDetailId(referencedId);
    }
}
