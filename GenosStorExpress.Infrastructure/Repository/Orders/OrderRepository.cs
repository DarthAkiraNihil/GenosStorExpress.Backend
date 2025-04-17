using GenosStorExpress.Domain.Entity.Orders;
using GenosStorExpress.Domain.Interface.Orders;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Orders {
    public class OrderRepository: IOrderRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public OrderRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<Order> List() {
            return _context.Orders
                           .Include(i => i.Items)
                           .ToList();
        }

        public Order? Get(int id) {
            return _context.Orders
                           .Include(i => i.Items)
                           .FirstOrDefault(i => i.Id == id);
        }

        public void Create(Order order) {
            _context.ChangeTracker.Clear();
            _context.Orders.Add(order);
        }

        public void Update(Order order) {
            _context.Entry(order).State = EntityState.Modified;
        }

        public void Delete(int id) {
            Order? order = _context.Orders.Find(id);
            if (order != null) {
                _context.Orders.Remove(order);
            }
        }
        
    }
}