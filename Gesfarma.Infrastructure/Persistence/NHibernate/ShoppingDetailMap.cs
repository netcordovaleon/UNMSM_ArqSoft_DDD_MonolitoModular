using FluentNHibernate.Mapping;
using Gesfarma.Domain.Shoppings.Aggregates.ShoppingDetail;

namespace Gesfarma.Infrastructure.Persistence.NHibernate;

public class ShoppingDetailMap : ClassMap<ShoppingDetail>
{
    public ShoppingDetailMap()
    {
        Table("shopping_detail");
        CompositeId(x => x.Id).KeyProperty(x => x.Id, y => y.ColumnName("id"));
        //Map(x => x.Quantity).CustomType<int>().Column("quantity");
        Map(x => x.Active).CustomType<bool>().Column("is_active");
        //Map(x => x.CreatedAt).Column("created_at_utc");
        //Map(x => x.UpdatedAt).Column("updated_at_utc");
        Component(x => x.Quantity, m =>        {            m.Map(x => x.Stock, "quantity");        });        Component(x => x.ShoppingId, m =>        {            m.Map(x => x.Id, "shopping_id");        });
        Component(x => x.ProductId, m =>        {            m.Map(x => x.Id, "product_id");        });
    }
}
