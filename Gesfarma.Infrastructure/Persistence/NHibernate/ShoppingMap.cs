using FluentNHibernate.Mapping;
using Gesfarma.Domain.Shoppings.Aggregates.Shopping;

namespace Gesfarma.Infrastructure.Persistence.NHibernate;

public class ShoppingMap : ClassMap<Shopping>
{
    public ShoppingMap()
    {
        Table("shopping");
        CompositeId(x => x.Id).KeyProperty(x => x.Id, y => y.ColumnName("id"));
        Map(x => x.State).CustomType<int>().Column("shopping_state");
        Map(x => x.Active).CustomType<bool>().Column("is_active");
        //Map(x => x.CreatedAt).CustomType<DateTime>().Column("created_at_utc");
        //Map(x => x.UpdatedAt).CustomType<DateTime>().Column("updated_at_utc");
        Component(x => x.ClientId, m =>        {            m.Map(x => x.Id, "client_id");        });
    }
}
