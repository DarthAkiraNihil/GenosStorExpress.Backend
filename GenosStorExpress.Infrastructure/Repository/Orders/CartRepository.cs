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
                           .Include(i => i.Items.Select(ci => new CartItem {
                               Cart = ci.Cart,
                               CartId = ci.CartId,
                               ItemId = ci.ItemId,
                               Item = ci.Item,
                               Quantity = ci.Quantity,
                           }))
                           .ToList();
        }

        public Cart? Get(string id) {
            return _context.Carts
                           .Include(i => i.Items)
                           .Select( c => new Cart {
                               CustomerId = c.CustomerId,
                               Customer = c.Customer,
                               Items = c.Items.Select(ci => new CartItem {
                                   Cart = ci.Cart,
                                   CartId = ci.CartId,
                                   ItemId = ci.ItemId,
                                   Item = new Domain.Entity.Item.Item {
                                       Name = ci.Item.Name,
                                       Model = ci.Item.Model,
                                       Id = ci.Item.Id,
                                       Price = ci.Item.Price,
                                       Description = ci.Item.Description,
                                       Carts = ci.Item.Carts,
                                       Reviews = ci.Item.Reviews,
                                       ActiveDiscount = ci.Item.ActiveDiscount,
                                       ItemType = ci.Item.ItemType,
                                   },
                                   Quantity = ci.Quantity,
                               }).ToList(),
                           })
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