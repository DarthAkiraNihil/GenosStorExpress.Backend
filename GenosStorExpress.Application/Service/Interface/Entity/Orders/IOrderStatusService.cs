using GenosStorExpress.Application.Service.Interface.Base;
using GenosStorExpress.Application.Wrappers.Enum;
using GenosStorExpress.Domain.Entity.Orders;

namespace GenosStorExpress.Application.Service.Interface.Entity.Orders {
    public interface IOrderStatusService: IEnumService<OrderStatus> {
        OrderStatus? GetEntityByDescriptor(OrderStatusDescriptor descriptor);
    }
}