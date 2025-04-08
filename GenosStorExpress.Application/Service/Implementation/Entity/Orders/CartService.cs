using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Orders;
using GenosStorExpress.Application.Wrappers.Entity.Orders;
using GenosStorExpress.Domain.Entity.Orders;
using GenosStorExpress.Domain.Entity.User;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Orders {
    /// <summary>
    /// Реализация сервиса корзин
    /// </summary>
    public class CartService: ICartService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly IAllItemsService _allItemsService;

        /// <summary>
        /// Стандартный конструктор
        /// </summary>
        /// <param name="repositories">Репозитории проекта</param>
        /// <param name="allItemsService">Общий сервис товаров</param>
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

        /// <summary>
        /// Добавление товара в корзину
        /// </summary>
        /// <param name="itemId">Номер товара</param>
        /// <param name="customerId">Номер покупателя</param>
        /// <exception cref="NullReferenceException">Если указанного покупателя или товара не существует</exception>
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
            _repositories.Orders.CartItems.Create(cartItem);
            // _repositories.Orders.Carts.Update(cart);
            _repositories.Save();

        }

        /// <summary>
        /// Удаление товара из корзины
        /// </summary>
        /// <param name="itemId">Номер товара</param>
        /// <param name="customerId">Номер покупателя</param>
        /// <exception cref="NullReferenceException">Если указанного покупателя или товара не существует</exception>
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
            // cart = _repositories.Orders.Carts.Get(cart.CustomerId)!;
            var cartItem = _repositories.Orders.CartItems.List().First(i => i.Item == item && i.CartId == cart.CustomerId);
            _repositories.Orders.CartItems.DeleteRaw(cartItem);
            cart.Items.Remove(cartItem);
            
            _repositories.Save();
        }

        /// <summary>
        /// Увеличение количества товара в корзине на 1
        /// </summary>
        /// <param name="itemId">Номер товара</param>
        /// <param name="customerId">Номер покупателя</param>
        /// <exception cref="NullReferenceException">Если указанного покупателя или товара не существует</exception>
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

        /// <summary>
        /// Уменьшение количества товара в корзине на 1 (или удаление, если он только один в корзине)
        /// </summary>
        /// <param name="itemId">Номер товара</param>
        /// <param name="customerId">Номер покупателя</param>
        /// <exception cref="NullReferenceException">Если указанного покупателя или товара не существует</exception>
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

        /// <summary>
        /// Проверка наличия товара в корзине
        /// </summary>
        /// <param name="itemId">Номер товара</param>
        /// <param name="customerId">Номер покупателя</param>
        /// <returns>true если товар есть в корзине, иначе false</returns>
        /// <exception cref="NullReferenceException">Если указанного покупателя или товара не существует</exception>
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

        /// <summary>
        /// Очистка корзины
        /// </summary>
        /// <param name="customerId">Номер покупателя</param>
        /// <exception cref="NullReferenceException">Если указанный покупатель не найден</exception>
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

        /// <summary>
        /// Получение содержимого корзины
        /// </summary>
        /// <param name="customerId">Номер покупателя</param>
        /// <returns>Содержимое корзины покупателя</returns>
        /// <exception cref="NullReferenceException">Если указанный покупатель не найден</exception>
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