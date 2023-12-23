using Gesfarma.Domain.SharedKernel;
namespace Gesfarma.Domain.Clients.Aggregates.Client;
public class ClientId : Identity
{
    protected ClientId()
    {
    }
    private ClientId(string referencedId) : base(referencedId)
    {
    }

    public static ClientId FromExisting(string referencedId)
    {
        return new ClientId(referencedId);
    }
}
