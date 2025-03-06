using GenosStorExpress.Application.Service.Interface.Entity.Orders;
using GenosStorExpress.Domain.Entity.Item;
using GenosStorExpress.Domain.Entity.Orders;
using GenosStorExpress.Domain.Entity.User;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Orders {
    public class CartService: ICartService {
        private IGenosStorExpressRepositories _repositories;
        
        public void AddToCart(Item item, Customer customer) {
            
            var cart = customer.Cart;
            var cartItem = new CartItem {
                Cart = cart,
                Item = item,
                Quantity = 1
            };
            cart.Items.Add(cartItem);
            _repositories.Save();
        }

        public void RemoveFromCart(Item item, Customer customer) {
            var cart = customer.Cart;
            if (cart == null) {
                return;
            }

            var cartItem = cart.Items.First(i => i.Item == item);
            _repositories.Orders.CartItems.DeleteRaw(cartItem);
            cart.Items.Remove(cartItem);
            
            _repositories.Save();
        }

        public void IncrementCartItemQuantity(Item item, Customer customer) {
            var cart = customer.Cart;
            cart.Items.First(i => i.Item == item).Quantity++;
            _repositories.Save();
        }

        public void DecrementCartItemQuantity(Item item, Customer customer) {
            var cart = customer.Cart;
            var itemToRemove = cart.Items.First(i => i.Item == item);
            itemToRemove.Quantity--;
            if (itemToRemove.Quantity == 0) {
                RemoveFromCart(item, customer);
                return;
            }
            _repositories.Save();
        }

        public bool IsInCart(Item item, Customer customer) {
            var cart = customer.Cart;
            return cart.Items.Select(i => i.Item).Contains(item);
        }

        public void ClearCart(Customer customer) {
            var cart = customer.Cart;
            while (cart.Items.Count > 0) {
                RemoveFromCart(cart.Items[0].Item, customer);
            }
        }

        public void Create(Cart item) {
            _repositories.Orders.Carts.Create(item);
        }

        public Cart Get(int id) {
            return _repositories.Orders.Carts.Get(id);
        }

        public List<Cart> List() {
            return _repositories.Orders.Carts.List();
        }

        public void Update(Cart item) {
            _repositories.Orders.Carts.Update(item);
        }

        public void Delete(int id) {
            _repositories.Orders.Carts.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        public CartService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }
    }
}