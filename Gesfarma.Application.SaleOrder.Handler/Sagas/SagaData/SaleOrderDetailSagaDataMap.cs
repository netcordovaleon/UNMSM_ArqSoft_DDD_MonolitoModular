using FluentNHibernate.Mapping;

namespace Gesfarma.Application.SaleOrder.Handler.Sagas.SagaData;

public class SaleOrderDetailSagaDataMap : ClassMap<SaleOrderDetailSagaData>
{
    public SaleOrderDetailSagaDataMap()
    {
        Table("shopping_detail_saga_data");
        Id(x => x.Id);
        Map(x => x.Originator).Column("Originator");
        Map(x => x.OriginalMessageId).Column("OriginalMessageId");
        Map(x => x.SaleOrderId).Column("SaleOrderId");
        Map(x => x.ProductId).Column("ProductId");
        Map(x => x.Quantity).Column("Quantity");
    }
}
