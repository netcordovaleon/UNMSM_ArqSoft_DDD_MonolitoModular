
namespace Gesfarma.Application.Shopping.Message.Events
{
    public class ClientWasNotFound : IEvent
    {
        public string ClientId { get; set; }

        public ClientWasNotFound(string clientId)
        {
            ClientId = clientId;
        }
    }
}
