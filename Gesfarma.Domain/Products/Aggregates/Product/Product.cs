using Gesfarma.Domain.Clients.Aggregates.Client;
using Gesfarma.Domain.SharedKernel;

namespace Gesfarma.Domain.Products.Aggregates.Product;

public class Product
{
    public virtual ProductId ProductId { get; protected set; }
    public virtual Name Name { get; protected set; }
    public virtual Money Price{ get; protected set; }
    public virtual ProductStock Stock { get; protected set; }
    public virtual bool Active { get; protected set; }
    public virtual DateTime CreatedAt { get; protected set; }
    public virtual DateTime UpdatedAt { get; protected set; }

    protected Product()
    {
    }

    protected Product(
        ProductId productId,
        Name name,
        Money price,
        ProductStock stock,
        bool active,
        DateTime createdAt,
        DateTime updatedAt)
    {
        ProductId = productId;
        Name = name;
        Price = price;
        Stock = stock;
        Active = active;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Product From(
        ProductId productId,
        Name name,
        Money price,
        ProductStock stock,
        bool active,
        DateTime createdAt,
        DateTime updatedAt)
    {
        return new Product(
            productId,
            name,
            price,
            stock,
            active,
            createdAt,
            updatedAt);
    }

    public static Product NonExisting()
    {
        ProductId productId = ProductId.FromExisting(null);
        DateTime Now = DateTime.Now;
        return new Product(
            productId,
            null,
            null,
            null,
            true,
            Now,
            Now);
    }

    public virtual bool DoesNotExist()
    {
        return ProductId == null || !ProductId.Ok();
    }

    public virtual bool Exist()
    {
        return ProductId != null && ProductId.Ok();
    }
}
