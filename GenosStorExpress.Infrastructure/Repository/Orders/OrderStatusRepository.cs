using GenosStorExpress.Domain.Entity.Orders;
using GenosStorExpress.Domain.Interface.Orders;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Orders {
    public class OrderStatusRepository: IOrderStatusRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public OrderStatusRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<OrderStatus> List() {
            return _context.OrderStatuses.ToList();
        }

        public OrderStatus? Get(int id) {
            return _context.OrderStatuses.Find(id);
        }

        public void Create(OrderStatus orderStatus) {
            _context.OrderStatuses.Add(orderStatus);
        }

        public void Update(OrderStatus orderStatus) {
            _context.Entry(orderStatus).State = EntityState.Modified;
        }

        public void Delete(int id) {
            OrderStatus orderStatus = _context.OrderStatuses.Find(id);
            if (orderStatus != null) {
                _context.OrderStatuses.Remove(orderStatus);
            }
        }
    }
}