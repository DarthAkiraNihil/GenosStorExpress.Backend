using GenosStorExpress.Domain.Entity.Orders;
using GenosStorExpress.Domain.Interface.Orders;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Orders {
    public class CartRepository: ICartRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public CartRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<Cart> List() {
            return _context.Carts
                           .Include(i => i.Items)
                           .ToList();
        }

        public Cart? Get(int id) {
            return _context.Carts
                           .Include(i => i.Items)
                           .FirstOrDefault(i => i.CustomerId == id);
        }

        public void Create(Cart cart) {
            _context.Carts.Add(cart);
        }

        public void Update(Cart cart) {
            _context.Entry(cart).State = EntityState.Modified;
        }

        public void Delete(int id) {
            Cart? cart = _context.Carts.Find(id);
            if (cart != null) {
                _context.Carts.Remove(cart);
            }
        }

        public void DeleteRaw(Cart item) {
            _context.Carts.Remove(item);
        }
    }
}