

using FluentNHibernate.Mapping;
using Gesfarma.Domain.Clients.Aggregates.Client;

namespace Gesfarma.Infrastructure.Persistence.NHibernate;

public class ClientMap : ClassMap<Client>
{
    public ClientMap()
    {
        Table("clients");
        CompositeId(x => x.ClientId).KeyProperty(x => x.Id, y => y.ColumnName("id"));
        Component(x => x.Name, m =>
        {
            m.Map(x => x.FirstName, "first_name");
            m.Map(x => x.LastName, "last_name");
        });
        Map(x => x.Active).CustomType<bool>().Column("is_active");
        Map(x => x.CreatedAt).Column("created_at_utc");
        Map(x => x.UpdatedAt).Column("updated_at_utc");
    }
}
