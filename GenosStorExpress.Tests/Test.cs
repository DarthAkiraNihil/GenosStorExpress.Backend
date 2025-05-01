using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Orders;
using GenosStorExpress.Domain.Entity.Item;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Entity.Orders;
using GenosStorExpress.Domain.Entity.User;
using GenosStorExpress.Domain.Interface;
using Moq;

namespace GenosStorExpress.Tests;

public abstract class Test {

    protected readonly Mock<IGenosStorExpressRepositories> _mockRepositories;
    protected readonly Mock<ICartService> _mockCartService;
    protected readonly Mock<IOrderStatusService> _mockOrderStatusService;
    protected readonly Mock<IAllItemsService> _mockAllItemsService;

    protected Test() {
        _mockRepositories = new Mock<IGenosStorExpressRepositories>();
        _mockCartService = new Mock<ICartService>();
        _mockOrderStatusService = new Mock<IOrderStatusService>();
        _mockAllItemsService = new Mock<IAllItemsService>();
    }
    
    protected Item _createItem(int id) {
        var item = new RAM {
            Id = id,
            Name = "TEST_RAM",
            Description = "TEST_RAM",
            Price = 100,
            TDP = 10,
            TotalSize = 16,
            ModuleSize = 8,
            ModulesCount = 2,
            Frequency = 1000,
            CL = 1,
            tRCD = 1,
            tRP = 1,
            tRAS = 2,
        };
        _mockRepositories.Setup(r => r.Items.All.Get(It.Is<int>(it => it == id))).Returns(item);
        return item;
    }

    protected Cart _createCart(string customerId, List<CartItem> items) {
        var cart = new Cart {
            CustomerId = customerId,
            Items = items
        };
        
        _mockRepositories.Setup(r => r.Orders.Carts.Get(It.Is<string>(id => id == customerId))).Returns(cart);
        _mockRepositories.Setup(r => r.Orders.Carts.List()).Returns(new List<Cart> {cart});

        return cart;
    }
    
    protected IndividualEntity _createIndividual() {
        var individualEntity = new IndividualEntity {
            Id = Guid.NewGuid().ToString(),
            UserName = "amogus@amogus.net",
            Email = "amogus@amogus.net",
            Name = "Amogus",
            Surname = "Amogusov",
            PhoneNumber = "+1234567890"
        };
        individualEntity.CartId = individualEntity.Id;
        individualEntity.Cart.CustomerId = individualEntity.Id;
        
        _mockRepositories
            .Setup(
                r => r
                     .Users
                     .IndividualEntities
                     .Get(It.Is<string>(id => id == individualEntity.Id)))
            .Returns(individualEntity);
        _mockRepositories.Setup(r => r.Users.Users.List()).Returns(new List<User> {individualEntity});
        
        return individualEntity;
    }

    protected void _commitUserList(List<User> entities) {
        _mockRepositories.Setup(r => r.Users.Users.List()).Returns(entities);
        //_mockRepositories.Setup(r => r.Users.IndividualEntities.Get(It.IsAny<string>()));
        //_mockRepositories.Setup(r => r.Users.LegalEntities.Get(It.IsAny<string>()));
    }
    
    protected void _commitOrderList(List<Order> entities) {
        _mockRepositories.Setup(r => r.Orders.Orders.List()).Returns(entities);
    }

    protected Administrator _createAdministrator() {
        var administrator = new Administrator {
            Id = Guid.NewGuid().ToString(),
            UserName = "amogus@amogus.net",
            Email = "amogus@amogus.net",
        };
        
        _mockRepositories
            .Setup(
                r => r
                     .Users
                     .Administrators
                     .Get(It.Is<string>(id => id == administrator.Id)))
            .Returns(administrator);
        
        return administrator;
    }
}