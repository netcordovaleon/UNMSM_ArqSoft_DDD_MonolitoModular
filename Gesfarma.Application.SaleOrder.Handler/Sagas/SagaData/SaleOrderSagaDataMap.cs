using FluentNHibernate.Mapping;

namespace Gesfarma.Application.SaleOrder.Handler.Sagas.SagaData;

public class SaleOrderSagaDataMap : ClassMap<SaleOrderSagaData>
{
    public SaleOrderSagaDataMap()
    {
        Table("shopping_saga_data");
        Id(x => x.Id);
        Map(x => x.Originator).Column("Originator");
        Map(x => x.OriginalMessageId).Column("OriginalMessageId");
        Map(x => x.SaleOrderId).Column("SaleOrderId");
        Map(x => x.ClientId).Column("ClientId");
        Map(x => x.State).Column("ShoppingState");
    }
}
