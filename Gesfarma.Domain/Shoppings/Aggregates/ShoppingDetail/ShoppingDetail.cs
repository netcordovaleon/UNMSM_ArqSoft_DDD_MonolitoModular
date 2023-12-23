using Gesfarma.Domain.Clients.Aggregates.Client;
using Gesfarma.Domain.Products.Aggregates.Product;
using Gesfarma.Domain.Shoppings.Aggregates.Shopping;

namespace Gesfarma.Domain.Shoppings.Aggregates.ShoppingDetail;

public class ShoppingDetail
{
    public virtual ShoppingDetailId Id { get; protected set; }
    public virtual ShoppingId ShoppingId { get; protected set; }
    public virtual ProductId ProductId { get; protected set; }
    public virtual ShoppingDetailQuantity Quantity { get; protected set; }
    public virtual bool Active { get; protected set; }
    public virtual DateTime CreatedAt { get; protected set; }
    public virtual DateTime UpdatedAt { get; protected set; }

    public virtual void GenerateId() {
        Id = ShoppingDetailId.FromExisting(Guid.NewGuid().ToString());
    }

    public virtual void SetShoppingReference(string shoppingId, string productId, int quantity)
    {
        ProductId = ProductId.FromExisting(productId);
        ShoppingId = ShoppingId.FromExisting(shoppingId);
        Quantity = ShoppingDetailQuantity.SetQuantity(quantity);

    }

    public virtual ShoppingDetail AddProductInDetail(ShoppingId shoppingId, string productId, int quantity)
    {
        return new ShoppingDetail()
        {
            Id = ShoppingDetailId.FromExisting(Guid.NewGuid().ToString()),
            ShoppingId = shoppingId,
            ProductId = ProductId.FromExisting(productId),
            Quantity = new ShoppingDetailQuantity(quantity)
        };
    }
}
