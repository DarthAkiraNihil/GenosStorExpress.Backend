using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Orders;
using GenosStorExpress.Application.Service.Interface.Entity.Users;

namespace GenosStorExpress.Application.Service.Interface.Entity {
    public interface IEntityServices {
        IItemServices Items { get; }
        IOrderEntitiesService Orders { get; }
        IUserEntitiesService Users { get; }
    }
}