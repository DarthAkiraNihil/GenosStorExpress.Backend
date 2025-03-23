using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Orders;
using GenosStorExpress.Application.Wrappers.Entity.Orders;
using GenosStorExpress.Application.Wrappers.Enum;
using GenosStorExpress.Domain.Entity.Orders;
using GenosStorExpress.Domain.Entity.User;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Orders {
    public class OrderService: IOrderService {

        private readonly IGenosStorExpressRepositories _repositories;
        private readonly ICartService _cartService;
        private readonly IOrderStatusService _orderStatusService;
        private readonly IAllItemsService _allItemsService;

        public OrderService(IGenosStorExpressRepositories repositories, ICartService cartService, IOrderStatusService orderStatusService, IAllItemsService allItemsService) {
            _repositories = repositories;
            _cartService = cartService;
            _orderStatusService = orderStatusService;
            _allItemsService = allItemsService;
        }
        
        public OrderWrapper? Get(int orderId, string customerId) {
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

        public double CalculateTotal(int orderId) {
            throw new NotImplementedException();
        }

        public List<ShortOrderWrapper> ListOfSpecificCustomer(string customerId) {
            throw new NotImplementedException();
        }

        private Customer? _getCustomer(string id) {
            Customer? customer = _repositories.Users.IndividualEntities.Get(id);
            if (customer == null) {
                return _repositories.Users.LegalEntities.Get(id);
            }
            return customer;
        }
        
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
        
        public bool OrderExists(int orderId) {
            return Get(orderId) != null;
        }

        List<ShortOrderWrapper> IOrderService.GetActiveOrders() {
            throw new NotImplementedException();
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
        
        public double CalculateTotal(Order order) {
            return order.Items.Sum(i => i.BoughtFor * i.Quantity);
        }

        public List<Order> ListOfSpecificCustomer(Customer customer) {
	        return List().Where(o => o.Customer.Id == customer.Id).ToList();
        }

        public List<Order> GetActiveOrders() {
            return List().Where(
                o => o.OrderStatus.Name != "Отменён" && o.OrderStatus.Name != "Получен"
            ).ToList();
        }

        public int Save() {
            return _repositories.Save();
        }

        
    }
}