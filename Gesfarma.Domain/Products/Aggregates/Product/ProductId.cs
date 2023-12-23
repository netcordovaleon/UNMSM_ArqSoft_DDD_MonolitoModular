using Gesfarma.Domain.SharedKernel;
namespace Gesfarma.Domain.Products.Aggregates.Product;
public class ProductId : Identity
{
    protected ProductId()
    {
    }
    private ProductId(string referencedId) : base(referencedId)
    {
    }

    public static ProductId FromExisting(string referencedId)
    {
        return new ProductId(referencedId);
    }
}
