using GenosStorExpress.Domain.Entity.Orders;
using GenosStorExpress.Domain.Interface.Orders;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Orders {
    public class OrderItemsRepository: IOrderItemsRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public OrderItemsRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<OrderItems> List() {
            return _context.OrderedItems.ToList();
        }

        public OrderItems? Get(int id) {
            return _context.OrderedItems.Find(id);
        }

        public void Create(OrderItems orderItems) {
            _context.OrderedItems.Add(orderItems);
        }

        public void Update(OrderItems orderItems) {
            _context.Entry(orderItems).State = EntityState.Modified;
        }

        public void Delete(int id) {
            OrderItems? orderItems = _context.OrderedItems.Find(id);
            if (orderItems != null) {
                _context.OrderedItems.Remove(orderItems);
            }
        }
        
    }
}