using Gesfarma.Domain.SharedKernel;
namespace Gesfarma.Domain.Clients.Aggregates.Client;
public class Client
{
    public virtual ClientId ClientId { get; protected set; }
    public virtual Name Name { get; protected set; }
    public virtual Money Price { get; protected set; }
    public virtual bool Active { get; protected set; }
    public virtual DateTime CreatedAt { get; protected set; }
    public virtual DateTime UpdatedAt { get; protected set; }

    protected Client()
    {
    }

    protected Client(
        ClientId clientId,
        Name name,
        bool active,
        DateTime createdAt,
        DateTime updatedAt)
    {
        ClientId = clientId;
        Name = name;
        Active = active;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Client From(
        ClientId customerId,
        Name name,
        bool active,
        DateTime createdAt,
        DateTime updatedAt)
    {
        return new Client(
            customerId,
            name,
            active,
            createdAt,
            updatedAt);
    }

    public static Client NonExisting()
    {
        ClientId customerId = ClientId.FromExisting(null);
        DateTime Now = DateTime.Now;
        return new Client(
            customerId,
            null,
            true,
            Now,
            Now);
    }

    public virtual bool DoesNotExist()
    {
        return ClientId == null || !ClientId.Ok();
    }

    public virtual bool Exist()
    {
        return ClientId != null && ClientId.Ok();
    }
}
