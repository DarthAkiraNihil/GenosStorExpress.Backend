using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Orders;
using GenosStorExpress.Application.Wrappers.Entity.Orders;
using GenosStorExpress.Application.Wrappers.Enum;
using GenosStorExpress.Domain.Entity.Orders;
using GenosStorExpress.Domain.Entity.User;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Orders {
    /// <summary>
    /// Интерфейс для сервиса заказов
    /// </summary>
    public class OrderService: IOrderService {

        private readonly IGenosStorExpressRepositories _repositories;
        private readonly ICartService _cartService;
        private readonly IOrderStatusService _orderStatusService;
        private readonly IAllItemsService _allItemsService;

        /// <summary>
        /// Стандартный конструктор
        /// </summary>
        /// <param name="repositories">Репозитории с данными</param>
        /// <param name="cartService">Сервис корзин</param>
        /// <param name="orderStatusService">Сервис статусов заказов</param>
        /// <param name="allItemsService">Общий сервис товаров</param>
        public OrderService(IGenosStorExpressRepositories repositories, ICartService cartService, IOrderStatusService orderStatusService, IAllItemsService allItemsService) {
            _repositories = repositories;
            _cartService = cartService;
            _orderStatusService = orderStatusService;
            _allItemsService = allItemsService;
        }
        
        /// <summary>
        /// Получение заказа по номеру
        /// </summary>
        /// <param name="orderId">Номер заказа</param>
        /// <param name="customerId">Номер заказчика</param>
        /// <returns>Обёртку заказа или null, если у покупателя такой заказ отсутствует</returns>
        /// <exception cref="NullReferenceException">Если заказа не существует</exception>
        public OrderWrapper Get(int orderId, string customerId) {
            Order? order = _repositories.Orders.Orders.Get(orderId);

            if (order == null || order.Customer.Id != customerId) {
                throw new NullReferenceException($"Заказа с номером {orderId} не существует");
            }

            return new OrderWrapper {
                Id = order.Id,
                Status = order.OrderStatus.Name,
                CreatedAt = order.CreatedAt,
                Items = order.Items.Select(
                    i => new OrderItemWrapper {
                        Item = _allItemsService.Get(i.ItemId)!,
                        Quantity = i.Quantity,
                        BoughtFor = i.BoughtFor,
                    }).ToList()
            };
        }

        /// <summary>
        /// Получение списка заказов покупателя
        /// </summary>
        /// <param name="customerId">Номер покупателя</param>
        /// <returns>Список всех заказов покупателя</returns>
        public IList<OrderWrapper> List(string customerId) {
            var orders = _repositories.Orders.Orders.List().Where(o => o.Customer.Id == customerId).ToList();

            return orders.Select(order => new OrderWrapper {
                Id = order.Id,
                Status = order.OrderStatus.Name,
                CreatedAt = order.CreatedAt,
                Items = order.Items.Select(
                    i => new OrderItemWrapper {
                        Item = _allItemsService.Get(i.ItemId)!,
                        Quantity = i.Quantity,
                        BoughtFor = i.BoughtFor,
                    }).ToList()
            }).ToList();
        }

        /// <summary>
        /// Перевод заказа в статус "Получен"
        /// </summary>
        /// <param name="orderId">Номер заказа</param>
        /// <exception cref="NullReferenceException">Если заказа не существует</exception>
        public double CalculateTotal(int orderId) {
            Order? order = _repositories.Orders.Orders.Get(orderId);

            if (order == null) {
                throw new NullReferenceException($"Заказа с номером {orderId} не существует");
            }
            
            return order.Items.Sum(i => i.BoughtFor * i.Quantity);
        }

        private Customer? _getCustomer(string id) {
            Customer? customer = _repositories.Users.IndividualEntities.Get(id);
            if (customer == null) {
                return _repositories.Users.LegalEntities.Get(id);
            }
            return customer;
        }
        
        /// <summary>
        /// Создание заказа из корзины
        /// </summary>
        /// <param name="customerId">Номер покупателя</param>
        /// <returns>Краткую информацию о созданном заказе</returns>
        /// <exception cref="NullReferenceException">Если покупателя не существует или если в базе данных нет статуса "Создан"</exception>
        public ShortOrderWrapper CreateOrderFromCart(string customerId) {
            
            Customer? customer = _getCustomer(customerId);

            if (customer == null) {
                throw new NullReferenceException("Покупатель с указанным ID не найден");
            }
            
            var cart = customer.Cart;
            var orderItems = new List<OrderItems>();
            var status = _orderStatusService.GetEntityByDescriptor(OrderStatusDescriptor.Created);

            if (status == null) {
                throw new NullReferenceException("Невозможно создать заказ, так как отсутствует статус \"Создан\". Мы уже решаем данную проблему");
            }
            
            var order = new Order {
                Customer = customer,
                CreatedAt = DateTime.Now,
                OrderStatus = status
            };

            if (cart.Items.Count == 0) {
                throw new InvalidOperationException("Корзина пуста. Создать заказ невозможно");
            }
            
            foreach (var item in cart.Items) {
                var discount = item.Item.ActiveDiscount;
                orderItems.Add(
                    new OrderItems {
                        Order = order,
                        Item = item.Item,
                        BoughtFor = discount != null ? item.Item.Price * discount.Value: item.Item.Price,
                        Quantity = item.Quantity
                    }
                );
            }
            
            order.Items = orderItems;
            _cartService.ClearCart(customerId);
            Create(order);
            _repositories.Save();
            return new ShortOrderWrapper {
                OrderId = order.Id,
                Status = order.OrderStatus.Name
            };
        }

        /// <summary>
        /// Перевод заказа в статус "Отменён"
        /// </summary>
        /// <param name="orderId">Номер заказа</param>
        /// <exception cref="NullReferenceException">Если заказа не существует или если в базе данных нет статуса "Получен"</exception>
        public void ReceiveOrder(int orderId) {
            
            Order? order = _repositories.Orders.Orders.Get(orderId);

            if (order == null) {
                throw new NullReferenceException($"Заказа с номером {orderId} не существует");
            }

            OrderStatus? received = _orderStatusService.GetEntityByDescriptor(OrderStatusDescriptor.Received);

            if (received == null) {
                throw new NullReferenceException("Невозможно перевести заказ в статус \"Получен\", так как он отсутствует. Мы уже решаем данную проблему");
            }
            
            order.OrderStatus = received;
            Update(order);
            _repositories.Save();
            
        }

        /// <summary>
        /// Получение списка активных заказов
        /// </summary>
        /// <returns>Список активных (не отменённых и не полученных) заказов</returns>
        /// <exception cref="NullReferenceException">Если заказа не существует или если в базе данных нет статуса "Отменён"</exception>
        public void CancelOrder(int orderId) {
            
            Order? order = _repositories.Orders.Orders.Get(orderId);

            if (order == null) {
                throw new NullReferenceException($"Заказа с номером {orderId} не существует");
            }

            OrderStatus? cancelled = _orderStatusService.GetEntityByDescriptor(OrderStatusDescriptor.Cancelled);

            if (cancelled == null) {
                throw new NullReferenceException("Невозможно перевести заказ в статус \"Отменён\", так как он отсутствует. Мы уже решаем данную проблему");
            }
            
            order.OrderStatus = cancelled;
            Update(order);
            _repositories.Save();
            
        }
        
        /// <summary>
        /// Проверка существования заказа
        /// </summary>
        /// <param name="orderId">Номер заказа</param>
        /// <returns><c>true</c> если заказ существует, иначе <c>false</c></returns>
        public bool OrderExists(int orderId) {
            return Get(orderId) != null;
        }

        /// <summary>
        /// Получение списка активных заказов
        /// </summary>
        /// <returns>Список активных (не отменённых и не полученных) заказов</returns>
        public List<ShortOrderWrapper> GetActiveOrders() {
            return List().Where(
                o => o.OrderStatus.Name != "Cancelled" && o.OrderStatus.Name != "Received"
            ).Select(i => new ShortOrderWrapper {
                OrderId = i.Id,
                Status = i.OrderStatus.Name
            }).ToList();
        }

        /// <summary>
        /// Получение списка активных заказов в сыром виде. Только для администратора
        /// </summary>
        /// <param name="sudoId">Номер администратора</param>
        /// <returns>Список активных (не отменённых и не полученных) заказов в виде сущностей</returns>
        public List<Order> GetActiveOrdersRaw(string sudoId) {
            Administrator? sudo = _repositories.Users.Administrators.Get(sudoId);
            if (sudo == null) {
                throw new NullReferenceException("Запрещено! Данный метод может быть вызван только администратором");
            }
            
            return List().Where(
                o => o.OrderStatus.Name != "Cancelled" && o.OrderStatus.Name != "Received"
            ).ToList();
        }

        private void Create(Order item) {
            _repositories.Orders.Orders.Create(item);
        }

        private Order? Get(int id) {
            return _repositories.Orders.Orders.Get(id);
        }

        private List<Order> List() {
            return _repositories.Orders.Orders.List();
        }

        private void Update(Order item) {
            _repositories.Orders.Orders.Update(item);
        }
        
        /// <summary>
        /// Сохранение данных репозиториев
        /// </summary>
        /// <returns>Количество сохранённых сущностей</returns>
        public int Save() {
            return _repositories.Save();
        }

        
    }
}