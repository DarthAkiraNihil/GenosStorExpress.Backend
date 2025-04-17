using GenosStorExpress.Domain.Interface.Item;
using GenosStorExpress.Domain.Interface.Orders;
using GenosStorExpress.Domain.Interface.User;
using Microsoft.EntityFrameworkCore.Storage;

namespace GenosStorExpress.Domain.Interface {
    public interface IGenosStorExpressRepositories {
        
        IItemRepository Items { get; }
        IOrderEntitiesRepository Orders { get; }
        IUserEntitiesRepository Users { get; }
        
        IDbContextTransaction BeginTransaction();

        int Save();

    }
}