using GenosStorExpress.Application.Service.Implementation.Entity.Orders;
using GenosStorExpress.Application.Wrappers.Enum;
using GenosStorExpress.Domain.Entity.Orders;
using GenosStorExpress.Domain.Entity.User;
using Moq;

namespace GenosStorExpress.Tests.Services;

public class OrderServiceNegativeTests: Test {
    
    private readonly OrderService _orderService;

    public OrderServiceNegativeTests() {
        _orderService = new OrderService(
            _mockRepositories.Object,
            _mockCartService.Object,
            _mockOrderStatusService.Object,
            _mockAllItemsService.Object
        );
    }
    
    private Order _createOrder(string customerId, int orderId, List<OrderItems> orderItems) {
        var order = new Order {
            Id = orderId,
            CreatedAt = DateTime.Now,
            CustomerId = customerId,
            OrderStatus = new OrderStatus {
                Id = 1,
                Name = "Created"
            },
            Items = orderItems
        };
        
        _mockRepositories.Setup(r => r.Orders.Orders.Get(It.Is<int>(id => id == orderId))).Returns(order);
        _mockRepositories.Setup(r => r.Orders.Orders.List()).Returns(new List<Order> {order});
        
        return order;
    }
    
    private void _createOrderStatuses() {
        IDictionary<OrderStatusDescriptor, string> orderStatuses = new Dictionary<OrderStatusDescriptor, string> {
            { OrderStatusDescriptor.Created, "Created" },
            { OrderStatusDescriptor.Confirmed, "Confirmed" },
            { OrderStatusDescriptor.AwaitsPayment, "AwaitsPayment" },
            { OrderStatusDescriptor.Paid, "Paid" },
            { OrderStatusDescriptor.Processing, "Processing" },
            { OrderStatusDescriptor.Delivering, "Delivering" },
            { OrderStatusDescriptor.Received, "Received" },
            { OrderStatusDescriptor.Cancelled, "Cancelled" },
        };
        
        foreach (var status in orderStatuses) {
            _mockOrderStatusService.Setup(s => s.GetEntityFromString(It.Is<string>(st => st == status.Value)))
                                   .Returns(new OrderStatus {
                                       Id = (int)status.Key,
                                       Name = status.Value
                                   });
            _mockOrderStatusService.Setup(s => s.GetEntityByDescriptor(It.Is<OrderStatusDescriptor>(st => st == status.Key)))
                                   .Returns(new OrderStatus {
                                       Id = (int)status.Key,
                                       Name = status.Value
                                   });
        }
        
        _mockRepositories.Setup(r => r.Orders.OrderStatuses.List()).Returns(orderStatuses.Select(s => new OrderStatus {
            Id = (int)s.Key,
            Name = s.Value
        }).ToList());
    }

    [Fact]
    public void GetOrderNoOrder() {
        var individualEntity = _createIndividual();
        _commitUserList(new List<User> {individualEntity});
        _mockRepositories.Setup(r => r.Orders.Orders.Get(It.IsAny<int>()));
        
        // Assert
        NullReferenceException exc = Assert.Throws<NullReferenceException>(() => _orderService.Get(1, individualEntity.Id));
        Assert.Equal("Заказа с номером 1 не существует", exc.Message);
    }

    [Fact]
    public void GetOrderNoCustomer() {
        var individualEntity = _createIndividual();
        _commitUserList(new List<User> {individualEntity});

        var item = _createItem(1);

        _createOrder(individualEntity.Id, 1, new List<OrderItems> {
            new OrderItems {
                ItemId = item.Id,
                Item = item,
                OrderId = 1,
                Quantity = 1,
                BoughtFor = 100
            }
        });

        NullReferenceException exc = Assert.Throws<NullReferenceException>(() => _orderService.Get(1, "void"));
        Assert.Equal("Заказа с номером 1 не существует", exc.Message);
    }
    
    [Fact]
    public void GetOrderItemsNoOrder() {
        var individualEntity = _createIndividual();
        _commitUserList(new List<User> {individualEntity});
        _mockRepositories.Setup(r => r.Orders.Orders.Get(It.IsAny<int>()));
        
        // Assert
        NullReferenceException exc = Assert.Throws<NullReferenceException>(() => _orderService.GetItems(1, individualEntity.Id, 0, 10));
        Assert.Equal("Заказа с номером 1 не существует", exc.Message);
    }

    [Fact]
    public void GetOrderItemsNoCustomer() {
        var individualEntity = _createIndividual();
        _commitUserList(new List<User> {individualEntity});

        var item = _createItem(1);

        var order = _createOrder(individualEntity.Id, 1, new List<OrderItems> {
            new OrderItems {
                ItemId = item.Id,
                Item = item,
                OrderId = 1,
                Quantity = 1,
                BoughtFor = 100
            }
        });

        NullReferenceException exc = Assert.Throws<NullReferenceException>(() => _orderService.GetItems(1, "void", 0, 10));
        Assert.Equal("Заказа с номером 1 не существует", exc.Message);
    }

    [Fact]
    public void GetListEmpty() {
        var individualEntity = _createIndividual();
        _commitUserList(new List<User> {individualEntity});
        
        _mockRepositories.Setup(r => r.Orders.Orders.List()).Returns(new List<Order>());
        
        var result = _orderService.List(individualEntity.Id, 0, 10);
        
        Assert.Equal(0, result.Count);
        Assert.Equal("-1", result.Previous);
        Assert.Null(result.Next);
    }
    
    [Fact]
    public void CalculateTotalNoOrder() {
        var individualEntity = _createIndividual();
        _commitUserList(new List<User> {individualEntity});
        _mockRepositories.Setup(r => r.Orders.Orders.Get(It.IsAny<int>()));
        
        // Assert
        NullReferenceException exc = Assert.Throws<NullReferenceException>(() => _orderService.CalculateTotal(1));
        Assert.Equal("Заказа с номером 1 не существует", exc.Message);
    }
    
    [Fact]
    public void CreateOrderNoCustomer() {
        var individualEntity = _createIndividual();
        _commitUserList(new List<User> {individualEntity});

        var item = _createItem(1);

        _createCart(individualEntity.Id, new List<CartItem> {
            new CartItem {
                CartId = individualEntity.Id,
                ItemId = 1,
                Item = item,
                Quantity = 1,
            }
        });
        
        _createOrderStatuses();
        _mockRepositories.Setup(r => r.Orders.Orders.Create(It.IsAny<Order>()));
        
        //NullReferenceException exc = Assert.Throws<NullReferenceException>(() => _orderService.CreateOrderFromCart("void"));
        //Assert.Equal("Покупатель с указанным ID не найден", exc.Message);
    }

    [Fact]
    public void CreateOrderNoStatus() {
        var individualEntity = _createIndividual();
        _commitUserList(new List<User> {individualEntity});

        var item = _createItem(1);

        _createCart(individualEntity.Id, new List<CartItem> {
            new CartItem {
                CartId = individualEntity.Id,
                ItemId = 1,
                Item = item,
                Quantity = 1,
            }
        });
        
        _mockRepositories.Setup(r => r.Orders.Orders.Create(It.IsAny<Order>()));
        
        NullReferenceException exc = Assert.Throws<NullReferenceException>(() => _orderService.CreateOrderFromCart(individualEntity.Id));
        Assert.Equal("Невозможно создать заказ, так как отсутствует статус \"Создан\". Мы уже решаем данную проблему", exc.Message);
    }

    [Fact]
    public void CreateOrderCartIsEmpty() {
        _createOrderStatuses();
        var individualEntity = _createIndividual();
        _commitUserList(new List<User> {individualEntity});

        var item = _createItem(1);

        _createCart(individualEntity.Id, new List<CartItem>());
        
        _mockRepositories.Setup(r => r.Orders.Orders.Create(It.IsAny<Order>()));
        
        InvalidOperationException exc = Assert.Throws<InvalidOperationException>(() => _orderService.CreateOrderFromCart(individualEntity.Id));
        Assert.Equal("Корзина пуста. Создать заказ невозможно", exc.Message);
    }

    [Fact]
    public void ReceiveOrderNoOrder() {
        var individualEntity = _createIndividual();
        _commitUserList(new List<User> {individualEntity});
        _mockRepositories.Setup(r => r.Orders.Orders.Get(It.IsAny<int>()));
        
        // Assert
        NullReferenceException exc = Assert.Throws<NullReferenceException>(() => _orderService.ReceiveOrder(1, individualEntity.Id));
        Assert.Equal("Заказа с номером 1 не существует", exc.Message);
    }

    [Fact]
    public void ReceiveOrderNoCustomer() {
        var individualEntity = _createIndividual();
        _commitUserList(new List<User> {individualEntity});

        var item = _createItem(1);

        _createOrder(individualEntity.Id, 1, new List<OrderItems> {
            new OrderItems {
                ItemId = item.Id,
                Item = item,
                OrderId = 1,
                Quantity = 1,
                BoughtFor = 100
            }
        });

        NullReferenceException exc = Assert.Throws<NullReferenceException>(() => _orderService.ReceiveOrder(1, "void"));
        Assert.Equal("Заказа с номером 1 не существует", exc.Message);
    }

    [Fact]
    public void ReceiveOrderNotDelivering() {
        var individualEntity = _createIndividual();
        _commitUserList(new List<User> {individualEntity});
        
        var item = _createItem(1);
        
        _createOrderStatuses();

        var order = _createOrder(individualEntity.Id, 1, new List<OrderItems> {
            new OrderItems {
                ItemId = item.Id,
                OrderId = 1,
                Quantity = 1,
                BoughtFor = 100
            }
        });
        
        ArgumentException exc = Assert.Throws<ArgumentException>(() => _orderService.ReceiveOrder((int) order.Id, individualEntity.Id));
        Assert.Equal("Невозможно получить заказ, так как он не доставляется", exc.Message);
    }
    
    [Fact]
    public void CancelOrderNoOrder() {
        var individualEntity = _createIndividual();
        _commitUserList(new List<User> {individualEntity});
        _mockRepositories.Setup(r => r.Orders.Orders.Get(It.IsAny<int>()));
        
        // Assert
        NullReferenceException exc = Assert.Throws<NullReferenceException>(() => _orderService.CancelOrder(1, individualEntity.Id));
        Assert.Equal("Заказа с номером 1 не существует", exc.Message);
    }

    [Fact]
    public void CancelOrderNoCustomer() {
        var individualEntity = _createIndividual();
        _commitUserList(new List<User> {individualEntity});

        var item = _createItem(1);

        _createOrder(individualEntity.Id, 1, new List<OrderItems> {
            new OrderItems {
                ItemId = item.Id,
                Item = item,
                OrderId = 1,
                Quantity = 1,
                BoughtFor = 100
            }
        });

        NullReferenceException exc = Assert.Throws<NullReferenceException>(() => _orderService.CancelOrder(1, "void"));
        Assert.Equal("Заказа с номером 1 не существует", exc.Message);
    }

    [Fact]
    public void CancelOrderNoStatus() {
        var individualEntity = _createIndividual();
        _commitUserList(new List<User> {individualEntity});
        
        var item = _createItem(1);

        var order = _createOrder(individualEntity.Id, 1, new List<OrderItems> {
            new OrderItems {
                ItemId = item.Id,
                OrderId = 1,
                Quantity = 1,
                BoughtFor = 100
            }
        });
        
        NullReferenceException exc = Assert.Throws<NullReferenceException>(() => _orderService.CancelOrder((int) order.Id, individualEntity.Id));
        Assert.Equal("Невозможно перевести заказ в статус \"Отменён\", так как он отсутствует. Мы уже решаем данную проблему", exc.Message);
    }

    [Fact]
    public void GetActiveOrdersNotAdmin() {
        var individualEntity = _createIndividual();
        var administrator = _createAdministrator();
        _commitUserList(new List<User> {individualEntity, administrator});
        
        _createOrderStatuses();

        var item = _createItem(1);

        var order1 = _createOrder(individualEntity.Id, 1, new List<OrderItems> {
            new OrderItems {
                ItemId = item.Id,
                OrderId = 1,
                Quantity = 1,
                BoughtFor = 100
            }
        });
        var order2 = _createOrder(individualEntity.Id, 2, new List<OrderItems> {
            new OrderItems {
                ItemId = item.Id,
                OrderId = 1,
                Quantity = 1,
                BoughtFor = 100
            }
        });
        _commitOrderList(
            new List<Order>{order1, order2}
        );
        
        NullReferenceException exc = Assert.Throws<NullReferenceException>(() => _orderService.GetActiveOrders(individualEntity.Id, 0, 10));
        Assert.Equal("Запрещено! Данный метод может быть вызван только администратором", exc.Message);
    }

    [Fact]
    public void GetDetailsOfAnyNotAdmin() {
        var individualEntity = _createIndividual();
        var administrator = _createAdministrator();
        _commitUserList(new List<User> {individualEntity, administrator});

        var item = _createItem(1);

        var order = _createOrder(individualEntity.Id, 1, new List<OrderItems> {
            new OrderItems {
                ItemId = item.Id,
                Item = item,
                OrderId = 1,
                Quantity = 1,
                BoughtFor = 100
            }
        });
        
        NullReferenceException exc = Assert.Throws<NullReferenceException>(() => _orderService.GetDetailsOfAny((int) order.Id, individualEntity.Id));
        Assert.Equal("Запрещено! Данный метод может быть вызван только администратором", exc.Message);
    }

    [Fact]
    public void GetItemsOfAnyNotAdmin() {
        var individualEntity = _createIndividual();
        var administrator = _createAdministrator();
        
        _commitUserList(new List<User> {individualEntity, administrator});
        
        var item = _createItem(1);

        _createOrder(individualEntity.Id, 1, new List<OrderItems> {
            new OrderItems {
                ItemId = item.Id,
                OrderId = 1,
                Quantity = 1,
                BoughtFor = 100
            }
        });
        
        NullReferenceException exc = Assert.Throws<NullReferenceException>(() => _orderService.GetItemsOfAny(1, individualEntity.Id, 0, 10));
        Assert.Equal("Запрещено! Данный метод может быть вызван только администратором", exc.Message);
    }

    [Fact]
    public void PromoteOrderNotAdmin() {
        var individualEntity = _createIndividual();
        var administrator = _createAdministrator();
        _commitUserList(new List<User> {individualEntity, administrator});
        
        _createOrderStatuses();

        var item = _createItem(1);

        var order = _createOrder(individualEntity.Id, 1, new List<OrderItems> {
            new OrderItems {
                ItemId = item.Id,
                OrderId = 1,
                Quantity = 1,
                BoughtFor = 100
            }
        });
        
        NullReferenceException exc = Assert.Throws<NullReferenceException>(() => _orderService.PromoteOrder((int) order.Id, individualEntity.Id));
        Assert.Equal("Запрещено! Данный метод может быть вызван только администратором", exc.Message);
    }

}