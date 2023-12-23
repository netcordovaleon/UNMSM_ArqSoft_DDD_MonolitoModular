
using FluentNHibernate.Mapping;
using Gesfarma.Domain.Products.Aggregates.Product;

namespace Gesfarma.Infrastructure.Persistence.NHibernate;

public class ProductMap : ClassMap<Product>
{
    public ProductMap()
    {
        Table("clients");
        CompositeId(x => x.ProductId).KeyProperty(x => x.Id, y => y.ColumnName("id"));
        Map(x => x.Name).CustomType<string>().Column("product_name");
        Map(x => x.Price).CustomType<decimal>().Column("price");
        Map(x => x.Stock).CustomType<int>().Column("stock");
        Map(x => x.Active).CustomType<bool>().Column("is_active");
        Map(x => x.CreatedAt).Column("created_at_utc");
        Map(x => x.UpdatedAt).Column("updated_at_utc");
    }
}
