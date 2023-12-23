using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gesfarma.Application.SaleOrder.Handler.Sagas.SagaData;

public class SaleOrderDetailSagaData : ContainSagaData
{
    public virtual string SaleOrderId { get; set; } = string.Empty;
    public virtual string ProductId { get; set; } = string.Empty;
    public virtual int Quantity { get; set; } = 0;
}
