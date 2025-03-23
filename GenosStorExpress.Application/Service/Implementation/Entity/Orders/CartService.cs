using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Orders;
using GenosStorExpress.Application.Wrappers.Entity.Orders;
using GenosStorExpress.Domain.Entity.Orders;
using GenosStorExpress.Domain.Entity.User;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Orders {
    public class CartService: ICartService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly IAllItemsService _allItemsService;

        public CartService(IGenosStorExpressRepositories repositories, IAllItemsService allItemsService) {
            _repositories = repositories;
            _allItemsService = allItemsService;
        }

        private Customer? _getCustomer(string id) {
            Customer? customer = _repositories.Users.IndividualEntities.Get(id);
            if (customer == null) {
                return _repositories.Users.LegalEntities.Get(id);
            }
            return customer;
        }

        public void AddToCart(int itemId, string customerId) {
            Customer? customer = _getCustomer(customerId);

            if (customer == null) {
                throw new NullReferenceException("Покупатель с указанным ID не найден");
            }
            
            var item = _repositories.Items.All.Get(itemId);

            if (item == null) {
                throw new NullReferenceException($"Товара с номером {itemId} не существует");
            }
            
            var cart = customer.Cart;
            var cartItem = new CartItem {
                Cart = cart,
                Item = item,
                Quantity = 1
            };
            cart.Items.Add(cartItem);
            _repositories.Save();

        }

        public void RemoveFromCart(int itemId, string customerId) {
            Customer? customer = _getCustomer(customerId);

            if (customer == null) {
                throw new NullReferenceException("Покупатель с указанным ID не найден");
            }
            
            var item = _repositories.Items.All.Get(itemId);

            if (item == null) {
                throw new NullReferenceException($"Товара с номером {itemId} не существует");
            }
            
            var cart = customer.Cart;
            var cartItem = cart.Items.First(i => i.Item == item);
            _repositories.Orders.CartItems.DeleteRaw(cartItem);
            cart.Items.Remove(cartItem);
            
            _repositories.Save();
        }

        public void IncrementCartItemQuantity(int itemId, string customerId) {
            Customer? customer = _getCustomer(customerId);

            if (customer == null) {
                throw new NullReferenceException("Покупатель с указанным ID не найден");
            }
            
            var item = _repositories.Items.All.Get(itemId);

            if (item == null) {
                throw new NullReferenceException($"Товара с номером {itemId} не существует");
            }
            
            var cart = customer.Cart;
            cart.Items.First(i => i.Item == item).Quantity++;
            _repositories.Save();
        }

        public void DecrementCartItemQuantity(int itemId, string customerId) {
            Customer? customer = _getCustomer(customerId);

            if (customer == null) {
                throw new NullReferenceException("Покупатель с указанным ID не найден");
            }
            
            var item = _repositories.Items.All.Get(itemId);

            if (item == null) {
                throw new NullReferenceException($"Товара с номером {itemId} не существует");
            }
            
            var cart = customer.Cart;
            var itemToRemove = cart.Items.First(i => i.Item == item);
            itemToRemove.Quantity--;
            if (itemToRemove.Quantity == 0) {
                RemoveFromCart(itemId, customerId);
                return;
            }
            _repositories.Save();
        }

        public bool IsInCart(int itemId, string customerId) {
            Customer? customer = _getCustomer(customerId);

            if (customer == null) {
                throw new NullReferenceException("Покупатель с указанным ID не найден");
            }
            
            var item = _repositories.Items.All.Get(itemId);

            if (item == null) {
                throw new NullReferenceException($"Товара с номером {itemId} не существует");
            }
            
            var cart = customer.Cart;
            return cart.Items.Select(i => i.Item).Contains(item);
        }

        public void ClearCart(string customerId) {
            Customer? customer = _getCustomer(customerId);

            if (customer == null) {
                throw new NullReferenceException("Покупатель с указанным ID не найден");
            }
            
            var cart = customer.Cart;
            while (cart.Items.Count > 0) {
                RemoveFromCart(cart.Items[0].Item.Id, customerId);
            }
        }

        public CartWrapper GetCart(string customerId) {
            Customer? customer = _getCustomer(customerId);

            if (customer == null) {
                throw new NullReferenceException("Покупатель с указанным ID не найден");
            }
            
            var cart = customer.Cart;
            return new CartWrapper {
                Items = cart.Items.Select(
                    i => new CartItemWrapper {
                        Item = _allItemsService.Get(i.Item.Id)!,
                        Quantity = i.Quantity
                    }).ToList()
                };
        }
    }
}