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
        /// Получение краткой информации о заказе по номеру
        /// </summary>
        /// <param name="orderId">Номер заказа</param>
        /// <param name="customerId">Номер заказчика</param>
        /// <returns>Обёртку заказа или null, если у покупателя такой заказ отсутствует</returns>
        /// <exception cref="NullReferenceException">Если заказа не существует</exception>
        ShortOrderWrapper IOrderService.Get(int orderId, string customerId) {
            Order? order = _repositories.Orders.Orders.Get(orderId);

            if (order == null || order.CustomerId != customerId) {
                throw new NullReferenceException($"Заказа с номером {orderId} не существует");
            }

            return new ShortOrderWrapper {
                Id = order.Id,
                Status = order.OrderStatus!.Name,
                CreatedAt = order.CreatedAt,
                Count = order.Items.Count
            };
        }

        /// <summary>
        /// Получение пагинированного списка товаров в заказе
        /// </summary>
        /// <param name="orderId">Номер заказа</param>
        /// <param name="customerId">Номер заказчика</param>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <returns>Пагинированный список товаров в заказе</returns>
        /// <exception cref="NullReferenceException">Если указанного заказа не существует</exception>
        public PaginatedOrderItemWrapper GetItems(int orderId, string customerId, int pageNumber, int pageSize) {
            Order? order = _repositories.Orders.Orders.Get(orderId);

            if (order == null || order.CustomerId != customerId) {
                throw new NullReferenceException($"Заказа с номером {orderId} не существует");
            }

            return new PaginatedOrderItemWrapper {
                Count = order.Items.Count,
                Previous = pageNumber == 1 ? null : (pageNumber - 1).ToString(),
                Next = (pageNumber + 1) * pageSize >= order.Items.Count ? null : (pageNumber + 1).ToString(),
                Items = order.Items.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(
                    i => new OrderItemWrapper {
                        Item = _allItemsService.Get(i.ItemId)!,
                        Quantity = i.Quantity,
                        BoughtFor = i.BoughtFor,
                    }
                ).ToList()
            };
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

            if (order == null || order.CustomerId != customerId) {
                throw new NullReferenceException($"Заказа с номером {orderId} не существует");
            }

            return new OrderWrapper {
                Id = order.Id,
                Status = order.OrderStatus!.Name,
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
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <returns>Список всех заказов покупателя</returns>
        public PaginatedShortOrderInfoWrapper List(string customerId, int pageNumber, int pageSize) {
            var orders = _repositories.Orders.Orders.List().Where(o => o.CustomerId == customerId).ToList();
            
            return new PaginatedShortOrderInfoWrapper {
                Count = orders.Count,
                Previous = pageNumber == 1 ? null : (pageNumber - 1).ToString(),
                Next = (pageNumber + 1) * pageSize >= orders.Count ? null : (pageNumber + 1).ToString(),
                Items = orders.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(
                    i => new ShortOrderWrapper {
                        Id = i.Id,
                        Count = i.Items.Count,
                        CreatedAt = i.CreatedAt,
                        Status = i.OrderStatus!.Name,
                    }
                ).ToList()
            };
            
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

            using var transaction = _repositories.BeginTransaction();
            
            Customer? customer = _getCustomer(customerId);

            if (customer == null) {
                throw new NullReferenceException("Покупатель с указанным ID не найден");
            }
            
            var cart = _repositories.Orders.Carts.Get(customer.Id)!;
            var orderItems = new List<OrderItems>();
            var status = _orderStatusService.GetEntityByDescriptor(OrderStatusDescriptor.Created);

            if (status == null) {
                throw new NullReferenceException("Невозможно создать заказ, так как отсутствует статус \"Создан\". Мы уже решаем данную проблему");
            }
            
            var order = new Order {
                CustomerId = customerId,
                CreatedAt = DateTime.Now,
                OrderStatusId = status.Id,
                //OrderStatus = status
            };
            
            _repositories.Orders.Orders.Create(order);
            _repositories.Save();
            order.OrderStatus = status;

            if (cart.Items.Count == 0) {
                throw new InvalidOperationException("Корзина пуста. Создать заказ невозможно");
            }
            
            foreach (var item in cart.Items) {
                var discount = item.Item!.ActiveDiscount;
                orderItems.Add(
                    new OrderItems {
                        OrderId = order.Id,
                        ItemId = item.Item.Id,
                        BoughtFor = discount != null ? item.Item.Price * discount.Value: item.Item.Price,
                        Quantity = item.Quantity
                    }
                );
            }
            
            order.Items = orderItems;
            _cartService.ClearCart(customerId);
            // Create(order);
            _repositories.Save();
            transaction?.Commit();
            return new ShortOrderWrapper {
                Id = order.Id,
                Status = order.OrderStatus!.Name,
                CreatedAt = order.CreatedAt,
                Count = orderItems.Count
            };
        }

        /// <summary>
        /// Перевод заказа в статус "Отменён"
        /// </summary>
        /// <param name="orderId">Номер заказа</param>
        /// <param name="customerId">Номер покупателя</param>
        /// <exception cref="NullReferenceException">Если заказа не существует или если в базе данных нет статуса "Получен"</exception>
        public void ReceiveOrder(int orderId, string customerId) {
            
            Order? order = _repositories.Orders.Orders.Get(orderId);

            if (order == null || order.CustomerId != customerId) {
                throw new NullReferenceException($"Заказа с номером {orderId} не существует");
            }
            
            if (order.OrderStatus!.Id != (int) OrderStatusDescriptor.Delivering) {
                throw new ArgumentException("Невозможно получить заказ, так как он не доставляется");
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
        /// <param name="orderId">Номе заказа</param>
        /// <param name="customerId">Номер покупателя</param>
        /// <exception cref="NullReferenceException">Если заказа не существует или если в базе данных нет статуса "Отменён"</exception>
        public void CancelOrder(int orderId, string customerId) {
            
            Order? order = _repositories.Orders.Orders.Get(orderId);

            if (order == null || order.CustomerId != customerId) {
                throw new NullReferenceException($"Заказа с номером {orderId} не существует");
            }
            
            OrderStatus? cancelled = _orderStatusService.GetEntityByDescriptor(OrderStatusDescriptor.Cancelled);

            if (cancelled == null) {
                throw new NullReferenceException("Невозможно перевести заказ в статус \"Отменён\", так как он отсутствует. Мы уже решаем данную проблему");
            }

            if (order.OrderStatus!.Id == cancelled.Id) {
                throw new ArgumentException("Невозможно отменить уже отменённый заказ");
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
        public PaginatedShortOrderInfoWrapper GetActiveOrders(string sudoId, int pageNumber, int pageSize) {
            
            Administrator? sudo = _repositories.Users.Administrators.Get(sudoId);
            if (sudo == null) {
                throw new NullReferenceException("Запрещено! Данный метод может быть вызван только администратором");
            }
            
            var orders = GetActiveOrdersRaw(sudoId);
            
            return new PaginatedShortOrderInfoWrapper {
                Count = orders.Count,
                Previous = pageNumber == 1 ? null : (pageNumber - 1).ToString(),
                Next = (pageNumber + 1) * pageSize >= orders.Count ? null : (pageNumber + 1).ToString(),
                Items = orders.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(
                    i => new ShortOrderWrapper {
                        Id = i.Id,
                        Count = i.Items.Count,
                        CreatedAt = i.CreatedAt,
                        Status = i.OrderStatus!.Name,
                    }
                ).ToList()
            };
        }

        public ShortOrderWrapper? GetDetailsOfAny(int orderId, string sudoId) {
            Administrator? sudo = _repositories.Users.Administrators.Get(sudoId);
            if (sudo == null) {
                throw new NullReferenceException("Запрещено! Данный метод может быть вызван только администратором");
            }
            
            Order? order = _repositories.Orders.Orders.Get(orderId);

            if (order == null) {
                throw new NullReferenceException($"Заказа с номером {orderId} не существует");
            }

            return new ShortOrderWrapper {
                Id = order.Id,
                Status = order.OrderStatus!.Name,
                CreatedAt = order.CreatedAt,
                Count = order.Items.Count
            };
        }


        public PaginatedOrderItemWrapper GetItemsOfAny(int orderId, string sudoId, int pageNumber, int pageSize) {
            
            Administrator? sudo = _repositories.Users.Administrators.Get(sudoId);
            if (sudo == null) {
                throw new NullReferenceException("Запрещено! Данный метод может быть вызван только администратором");
            }
            
            Order? order = _repositories.Orders.Orders.Get(orderId);

            if (order == null) {
                throw new NullReferenceException($"Заказа с номером {orderId} не существует");
            }

            return new PaginatedOrderItemWrapper {
                Count = order.Items.Count,
                Previous = pageNumber == 1 ? null : (pageNumber - 1).ToString(),
                Next = (pageNumber + 1) * pageSize >= order.Items.Count ? null : (pageNumber + 1).ToString(),
                Items = order.Items.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(
                    i => new OrderItemWrapper {
                        Item = _allItemsService.Get(i.ItemId)!,
                        Quantity = i.Quantity,
                        BoughtFor = i.BoughtFor,
                    }
                ).ToList()
            };
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
                o => o.OrderStatus!.Name != "Cancelled" && o.OrderStatus.Name != "Received"
            ).ToList();
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

        public ShortOrderWrapper PromoteOrder(int orderId, string sudoId) {
            
            Administrator? sudo = _repositories.Users.Administrators.Get(sudoId);
            if (sudo == null) {
                throw new NullReferenceException("Запрещено! Данный метод может быть вызван только администратором");
            }
            
            Order? order = _repositories.Orders.Orders.Get(orderId);

            if (order == null) {
                throw new NullReferenceException($"Заказа с номером {orderId} не существует");
            }

            if (order.OrderStatus == null) {
                throw new NullReferenceException($"У заказа с номером {orderId} нет статуса. Мы уже работаем над данной проблемой");
            }

            if (order.OrderStatus.Id == (long)OrderStatusDescriptor.Cancelled) {
                throw new ArgumentException("Невозможно продвинуть отменённый заказ");
            } 
            
            if (order.OrderStatus.Id == (long)OrderStatusDescriptor.Received) {
                throw new ArgumentException("Невозможно продвинуть полученный заказ");
            } 

            switch (order.OrderStatus.Id) {
                case (long) OrderStatusDescriptor.Created: {
                    order.OrderStatus = _orderStatusService.GetEntityByDescriptor(OrderStatusDescriptor.Confirmed);
                    break;
                }
                case (long) OrderStatusDescriptor.Confirmed: {
                    order.OrderStatus = _orderStatusService.GetEntityByDescriptor(OrderStatusDescriptor.AwaitsPayment);
                    break;
                }
                case (long) OrderStatusDescriptor.Paid: {
                    order.OrderStatus = _orderStatusService.GetEntityByDescriptor(OrderStatusDescriptor.Processing);
                    break;
                }
                case (long) OrderStatusDescriptor.Processing: {
                    order.OrderStatus = _orderStatusService.GetEntityByDescriptor(OrderStatusDescriptor.Delivering);
                    break;
                }
            }
            
            _repositories.Orders.Orders.Update(order);
            _repositories.Save();

            return new ShortOrderWrapper {
                Id = order.Id,
                Status = order.OrderStatus!.Name,
                CreatedAt = order.CreatedAt,
                Count = order.Items.Count
            };
            
        }
    }
}