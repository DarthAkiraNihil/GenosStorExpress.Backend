using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item;
using GenosStorExpress.Domain.Interface.Orders;
using GenosStorExpress.Domain.Interface.User;
using GenosStorExpress.Infrastructure.Context;
using GenosStorExpress.Infrastructure.Repository.Item;
using GenosStorExpress.Infrastructure.Repository.Orders;
using GenosStorExpress.Infrastructure.Repository.User;

namespace GenosStorExpress.Infrastructure.Repository {
    public class GenosStorExpressRepositories: IGenosStorExpressRepositories {
        
        private GenosStorExpressDatabaseContext _context;
        
        private ItemRepository _items;
        private OrderEntitiesRepository _orders;
        private UserEntitiesRepository _users;
        

        public GenosStorExpressRepositories(GenosStorExpressDatabaseContext context) {
	        _context = context;
        }
        
        public IItemRepository Items {
            get {
                if (_items == null) {
                    _items = new ItemRepository(_context);
                }
                return _items;
            }
        }
        
        public IOrderEntitiesRepository Orders {
            get {
                if (_orders == null) {
                    _orders = new OrderEntitiesRepository(_context);
                }
                return _orders;
            }
        }
        public IUserEntitiesRepository Users {
            get {
                if (_users == null) {
                    _users = new UserEntitiesRepository(_context);
                }
                return _users;
            }
        }

        public int Save() {
            return _context.SaveChanges();
        }
    }
}