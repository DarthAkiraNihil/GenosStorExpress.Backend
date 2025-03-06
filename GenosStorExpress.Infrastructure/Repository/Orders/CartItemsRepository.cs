using GenosStorExpress.Domain.Entity.Orders;
using GenosStorExpress.Domain.Interface.Orders;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Orders {
    public class CartItemsRepository: ICartItemsRepository {
        private readonly GenosStorExpressDatabaseContext _context;
        
        public CartItemsRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<CartItem> List() {
            return _context.CartItems.ToList();
        }

        public CartItem Get(int id) {
            return _context.CartItems.Find(id);
        }

        public void Create(CartItem item) {
            _context.CartItems.Add(item);
        }

        public void Update(CartItem item) {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id) {
            CartItem item = _context.CartItems.Find(new {ItemID = id, CartId = 0});
            if (item != null)
                _context.CartItems.Remove(item);
        }

        public void DeleteRaw(CartItem item) {
            _context.CartItems.Remove(item);
        }
    }
}