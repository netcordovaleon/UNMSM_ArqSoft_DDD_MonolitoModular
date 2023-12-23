using Gesfarma.Domain.Clients.Aggregates.Client;
using Gesfarma.Domain.Products.Aggregates.Product;
using Gesfarma.Domain.Shoppings.Aggregates.ShoppingDetail;
namespace Gesfarma.Domain.Shoppings.Aggregates.Shopping;
public class Shopping
{
    public virtual ShoppingId Id { get; protected set; }
    public virtual ClientId ClientId { get; protected set; }
    public virtual ShoppingState State { get; protected set; }
    public virtual bool Active { get; protected set; }
    public virtual DateTime CreatedAt { get; protected set; }
    public virtual DateTime UpdatedAt { get; protected set; }

    public virtual void GenerateId()
    {
        Id = ShoppingId.FromExisting(Guid.NewGuid().ToString());
    }

    public virtual void SetId(ShoppingId id)
    {
        Id = id;
    }

    public virtual void SetClient(ClientId clientId)
    {
        ClientId = clientId;
    }

    public virtual void SetSalesDate()
    {
        CreatedAt = DateTime.UtcNow;
    }

    public virtual void SetStartSaleProcess()
    {
        State = ShoppingState.IN_PROGRESS;
    }

    public virtual void SetStartSaleCompleted()
    {
        State = ShoppingState.COMPLETED;
    }

}
