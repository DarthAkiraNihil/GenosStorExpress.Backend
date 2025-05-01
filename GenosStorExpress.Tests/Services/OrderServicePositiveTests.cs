using GenosStorExpress.Application.Service.Implementation.Entity.Orders;
using GenosStorExpress.Application.Wrappers.Enum;
using GenosStorExpress.Domain.Entity.Orders;
using GenosStorExpress.Domain.Entity.User;
using Moq;

namespace GenosStorExpress.Tests.Services;

public class OrderServicePositiveTests: Test {
    private readonly OrderService _orderService;

    public OrderServicePositiveTests() {
        _orderService = new OrderService(
            _mockRepositories.Object,
            _mockCartService.Object,
            _mockOrderStatusService.Object,
            _mockAllItemsService.Object
        );
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

    [Fact]
    public void GetOrderSuccess() {
        
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
        // Act
        var result =  _orderService.Get(1, individualEntity.Id);

        // Assert
        Assert.Equal(order.CreatedAt, result.CreatedAt);
        Assert.Equal(order.Id, result.Id);
        Assert.Equal(order.OrderStatus!.Name, result.Status);
        Assert.Equal(order.Items.Count, result.Items.Count);
        Assert.Equal(order.Items[0].Quantity, result.Items[0].Quantity);
        Assert.Equal(order.Items[0].BoughtFor, result.Items[0].BoughtFor);
        
    }

    [Fact]
    public void GetOrderItemsSuccess() {
        var individualEntity = _createIndividual();
        _commitUserList(new List<User> {individualEntity});
        
        var item = _createItem(1);

        _createOrder(individualEntity.Id, 1, new List<OrderItems> {
            new OrderItems {
                ItemId = item.Id,
                OrderId = 1,
                Quantity = 1,
                BoughtFor = 100
            }
        });
        // Act
        var result =  _orderService.GetItems(1, individualEntity.Id, 1, 10);
        
        // Assert
        Assert.Equal(1, result.Count);
        Assert.Null(result.Previous);
        Assert.Null(result.Next);
        
    }

    [Fact]
    public void GetOrderListSuccess() {
        _createOrderStatuses();
        
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
        // Act
        var result =  _orderService.List(individualEntity.Id, 1, 10);
        
        // Assert
        Assert.Equal(1, result.Count);
        Assert.Null(result.Previous);
        Assert.Null(result.Next);
        Assert.Equal("Created", result.Items[0].Status);
        Assert.Equal(order.CreatedAt, result.Items[0].CreatedAt);
        Assert.Equal(order.Id, result.Items[0].Id);
    }
    
    [Fact]
    public void CreateOrderSuccess() {
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
        
        // Act
        var result =  _orderService.CreateOrderFromCart(individualEntity.Id);
        
        // Assert
        Assert.Equal(1, result.Count);
        Assert.Equal("Created", result.Status);
        
    }

    [Fact]
    public void CalculateTotalSuccess() {
        var individualEntity = _createIndividual();
        _commitUserList(new List<User> {individualEntity});
        
        var item1 = _createItem(1);
        var item2 = _createItem(2);

        var order = _createOrder(individualEntity.Id, 1, new List<OrderItems> {
            new OrderItems {
                ItemId = item1.Id,
                OrderId = 1,
                Quantity = 10,
                BoughtFor = 100
            },
            new OrderItems {
                ItemId = item2.Id,
                OrderId = 1,
                Quantity = 1,
                BoughtFor = 200
            }
        });
        // Act
        var result =  _orderService.CalculateTotal((int) order.Id);
        
        // Assert
        Assert.Equal(1200.0, result);
    }

    [Fact]
    public void CancelOrderSuccess() {
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
        // Act
        _orderService.CancelOrder(1, individualEntity.Id);

        // Act
        
        // Assert
        Assert.Equal("Cancelled", order.OrderStatus!.Name);
    }

    [Fact]
    public void GetActiveOrdersSuccess() {
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
        
        // Act
        
        _orderService.CancelOrder(2, individualEntity.Id);
        Assert.Equal("Cancelled", order2.OrderStatus!.Name);
        
        var result = _orderService.GetActiveOrders(administrator.Id, 0, 10);
        
        // Assert
        Assert.Equal(1, result.Count);
        Assert.Equal("-1", result.Previous);
        Assert.Null(result.Next);
        Assert.Equal(order1.Id, result.Items[0].Id);
        Assert.Equal("Created", result.Items[0].Status);
        
    }

    [Fact]
    public void GetDetailsOfAnySuccess() {
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
        // Act
        var result =  _orderService.GetDetailsOfAny(1, administrator.Id);

        // Assert
        Assert.Equal(order.CreatedAt, result!.CreatedAt);
        Assert.Equal(order.Id, result.Id);
        Assert.Equal(order.OrderStatus!.Name, result.Status);
        Assert.Equal(order.Items.Count, result.Count);
    }

    [Fact]
    public void GetItemsOfAnySuccess() {
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
        // Act
        var result =  _orderService.GetItemsOfAny(1, administrator.Id, 1, 10);
        
        // Assert
        Assert.Equal(1, result.Count);
        Assert.Null(result.Previous);
        Assert.Null(result.Next);
    }

    [Fact]
    public void PromoteOrderSuccess() {
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
        // Act
        var result = _orderService.PromoteOrder(1, administrator.Id);
        
        // Assert
        Assert.Equal(order.CreatedAt, result.CreatedAt);
        Assert.Equal(order.Id, result.Id);
        Assert.Equal("Confirmed", result.Status);
        Assert.Equal(order.Items.Count, result.Count);
        
        // Act
        result = _orderService.PromoteOrder(1, administrator.Id);
        
        // Assert
        Assert.Equal(order.CreatedAt, result.CreatedAt);
        Assert.Equal(order.Id, result.Id);
        Assert.Equal("AwaitsPayment", result.Status);
        Assert.Equal(order.Items.Count, result.Count);
        
    }

}