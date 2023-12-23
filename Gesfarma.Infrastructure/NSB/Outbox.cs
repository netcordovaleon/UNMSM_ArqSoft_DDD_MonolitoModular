using NServiceBus.Outbox.NHibernate;

namespace Gesfarma.Infrastructure.NSB;

public class Outbox : IOutboxRecord
{
    public virtual long Id { get; set; }
    public virtual string MessageId { get; set; } = string.Empty;
    public virtual bool Dispatched { get; set; }
    public virtual DateTime? DispatchedAt { get; set; } = DateTime.Now;
    public virtual string TransportOperations { get; set; } = string.Empty;
}
