using GenosStorExpress.Application.Service.Implementation.Entity.Orders;
using GenosStorExpress.Domain.Entity.Orders;
using GenosStorExpress.Domain.Entity.User;
using Moq;

namespace GenosStorExpress.Tests.Services;

public class CartServiceNegativeTests: Test {
    
    private readonly CartService _cartService;

    public CartServiceNegativeTests() {
        _cartService = new CartService(
            _mockRepositories.Object,
            _mockAllItemsService.Object
        );
    }

    [Fact]
    public void AddToCartNoCustomer() {
        var individualEntity = _createIndividual();
        _commitUserList(new List<User> {individualEntity});

        _mockRepositories.Setup(r => r.Users.IndividualEntities.Get(It.Is<string>(id => id == individualEntity.Id)));
        _mockRepositories.Setup(r => r.Users.LegalEntities.Get(It.IsAny<string>()));
        
        var item = _createItem(1);

        var cart = _createCart(individualEntity.Id, new List<CartItem> {
            new CartItem {
                CartId = individualEntity.Id,
                ItemId = 1,
                Item = item,
                Quantity = 1,
            }
        });
        
        var newItem = _createItem(2);
        
        _mockRepositories.Setup(r => r.Orders.Carts.Create(It.IsAny<Cart>()));
        _mockRepositories.Setup(r => r.Orders.CartItems.Create(It.IsAny<CartItem>())).Callback(
            (CartItem created) => {
                _createCart(individualEntity.Id, new List<CartItem> {
                    cart.Items[0],
                    created
                });
            }
        );

        NullReferenceException exc = Assert.Throws<NullReferenceException>(() => _cartService.AddToCart(newItem.Id, "void"));
        Assert.Equal("Покупатель с указанным ID не найден", exc.Message);
    }

    [Fact]
    public void AddToCartNoItem() {
        var individualEntity = _createIndividual();
        _commitUserList(new List<User> {individualEntity});

        var item = _createItem(1);

        var cart = _createCart(individualEntity.Id, new List<CartItem> {
            new CartItem {
                CartId = individualEntity.Id,
                ItemId = 1,
                Item = item,
                Quantity = 1,
            }
        });
        
        _mockRepositories.Setup(r => r.Orders.Carts.Create(It.IsAny<Cart>()));
        _mockRepositories.Setup(r => r.Orders.CartItems.Create(It.IsAny<CartItem>())).Callback(
            (CartItem created) => {
                _createCart(individualEntity.Id, new List<CartItem> {
                    cart.Items[0],
                    created
                });
            }
        );

        NullReferenceException exc = Assert.Throws<NullReferenceException>(() => _cartService.AddToCart(2, individualEntity.Id));
        Assert.Equal($"Товара с номером 2 не существует", exc.Message);
    }
    
    [Fact]
    public void RemoveToCartNoCustomer() {
        var individualEntity = _createIndividual();
        _commitUserList(new List<User> {individualEntity});
        
        _mockRepositories.Setup(r => r.Users.IndividualEntities.Get(It.Is<string>(id => id == individualEntity.Id)));
        _mockRepositories.Setup(r => r.Users.LegalEntities.Get(It.IsAny<string>()));

        var item = _createItem(1);

        var cart = _createCart(individualEntity.Id, new List<CartItem> {
            new CartItem {
                CartId = individualEntity.Id,
                ItemId = 1,
                Item = item,
                Quantity = 1,
            }
        });
        
        var newItem = _createItem(2);
        
        _mockRepositories.Setup(r => r.Orders.Carts.Create(It.IsAny<Cart>()));
        _mockRepositories.Setup(r => r.Orders.CartItems.Create(It.IsAny<CartItem>())).Callback(
            (CartItem created) => {
                _createCart(individualEntity.Id, new List<CartItem> {
                    cart.Items[0],
                    created
                });
            }
        );

        NullReferenceException exc = Assert.Throws<NullReferenceException>(() => _cartService.RemoveFromCart(newItem.Id, "void"));
        Assert.Equal("Покупатель с указанным ID не найден", exc.Message);
    }

    [Fact]
    public void RemoveFromCartNoItem() {
        var individualEntity = _createIndividual();
        _commitUserList(new List<User> {individualEntity});

        var item = _createItem(1);

        var cart = _createCart(individualEntity.Id, new List<CartItem> {
            new CartItem {
                CartId = individualEntity.Id,
                ItemId = 1,
                Item = item,
                Quantity = 1,
            }
        });
        
        _mockRepositories.Setup(r => r.Orders.Carts.Create(It.IsAny<Cart>()));
        _mockRepositories.Setup(r => r.Orders.CartItems.Create(It.IsAny<CartItem>())).Callback(
            (CartItem created) => {
                _createCart(individualEntity.Id, new List<CartItem> {
                    cart.Items[0],
                    created
                });
            }
        );

        NullReferenceException exc = Assert.Throws<NullReferenceException>(() => _cartService.RemoveFromCart(2, individualEntity.Id));
        Assert.Equal($"Товара с номером 2 не существует", exc.Message);
    }

    [Fact]
    public void IncrementItemQuantityNoCustomer() {
        var individualEntity = _createIndividual();
        _commitUserList(new List<User> {individualEntity});
        
        _mockRepositories.Setup(r => r.Users.IndividualEntities.Get(It.Is<string>(id => id == individualEntity.Id)));
        _mockRepositories.Setup(r => r.Users.LegalEntities.Get(It.IsAny<string>()));

        var item = _createItem(1);

        var cart = _createCart(individualEntity.Id, new List<CartItem> {
            new CartItem {
                CartId = individualEntity.Id,
                ItemId = 1,
                Item = item,
                Quantity = 1,
            }
        });
        
        var newItem = _createItem(2);
        
        _mockRepositories.Setup(r => r.Orders.Carts.Create(It.IsAny<Cart>()));
        _mockRepositories.Setup(r => r.Orders.CartItems.Create(It.IsAny<CartItem>())).Callback(
            (CartItem created) => {
                _createCart(individualEntity.Id, new List<CartItem> {
                    cart.Items[0],
                    created
                });
            }
        );

        NullReferenceException exc = Assert.Throws<NullReferenceException>(() => _cartService.IncrementCartItemQuantity(newItem.Id, "void"));
        Assert.Equal("Покупатель с указанным ID не найден", exc.Message);
    }

    [Fact]
    public void IncrementItemQuantityNoItem() {
        var individualEntity = _createIndividual();
        _commitUserList(new List<User> {individualEntity});

        var item = _createItem(1);

        var cart = _createCart(individualEntity.Id, new List<CartItem> {
            new CartItem {
                CartId = individualEntity.Id,
                ItemId = 1,
                Item = item,
                Quantity = 1,
            }
        });
        
        _mockRepositories.Setup(r => r.Orders.Carts.Create(It.IsAny<Cart>()));
        _mockRepositories.Setup(r => r.Orders.CartItems.Create(It.IsAny<CartItem>())).Callback(
            (CartItem created) => {
                _createCart(individualEntity.Id, new List<CartItem> {
                    cart.Items[0],
                    created
                });
            }
        );

        NullReferenceException exc = Assert.Throws<NullReferenceException>(() => _cartService.IncrementCartItemQuantity(2, individualEntity.Id));
        Assert.Equal($"Товара с номером 2 не существует", exc.Message);
    }
    
    [Fact]
    public void DecrementItemQuantityNoCustomer() {
        var individualEntity = _createIndividual();
        _commitUserList(new List<User> {individualEntity});
        
        _mockRepositories.Setup(r => r.Users.IndividualEntities.Get(It.Is<string>(id => id == individualEntity.Id)));
        _mockRepositories.Setup(r => r.Users.LegalEntities.Get(It.IsAny<string>()));

        var item = _createItem(1);

        var cart = _createCart(individualEntity.Id, new List<CartItem> {
            new CartItem {
                CartId = individualEntity.Id,
                ItemId = 1,
                Item = item,
                Quantity = 1,
            }
        });
        
        var newItem = _createItem(2);
        
        _mockRepositories.Setup(r => r.Orders.Carts.Create(It.IsAny<Cart>()));
        _mockRepositories.Setup(r => r.Orders.CartItems.Create(It.IsAny<CartItem>())).Callback(
            (CartItem created) => {
                _createCart(individualEntity.Id, new List<CartItem> {
                    cart.Items[0],
                    created
                });
            }
        );

        NullReferenceException exc = Assert.Throws<NullReferenceException>(() => _cartService.DecrementCartItemQuantity(newItem.Id, "void"));
        Assert.Equal("Покупатель с указанным ID не найден", exc.Message);
    }

    [Fact]
    public void DecrementItemQuantityNoItem() {
        var individualEntity = _createIndividual();
        _commitUserList(new List<User> {individualEntity});

        var item = _createItem(1);

        var cart = _createCart(individualEntity.Id, new List<CartItem> {
            new CartItem {
                CartId = individualEntity.Id,
                ItemId = 1,
                Item = item,
                Quantity = 1,
            }
        });
        
        _mockRepositories.Setup(r => r.Orders.Carts.Create(It.IsAny<Cart>()));
        _mockRepositories.Setup(r => r.Orders.CartItems.Create(It.IsAny<CartItem>())).Callback(
            (CartItem created) => {
                _createCart(individualEntity.Id, new List<CartItem> {
                    cart.Items[0],
                    created
                });
            }
        );

        NullReferenceException exc = Assert.Throws<NullReferenceException>(() => _cartService.DecrementCartItemQuantity(2, individualEntity.Id));
        Assert.Equal($"Товара с номером 2 не существует", exc.Message);
    }

    [Fact]
    public void IsInCartNoCustomer() {
        var individualEntity = _createIndividual();
        _commitUserList(new List<User> {individualEntity});
        
        _mockRepositories.Setup(r => r.Users.IndividualEntities.Get(It.Is<string>(id => id == individualEntity.Id)));
        _mockRepositories.Setup(r => r.Users.LegalEntities.Get(It.IsAny<string>()));

        var item = _createItem(1);

        var cart = _createCart(individualEntity.Id, new List<CartItem> {
            new CartItem {
                CartId = individualEntity.Id,
                ItemId = 1,
                Item = item,
                Quantity = 1,
            }
        });
        
        var newItem = _createItem(2);
        
        _mockRepositories.Setup(r => r.Orders.Carts.Create(It.IsAny<Cart>()));
        _mockRepositories.Setup(r => r.Orders.CartItems.Create(It.IsAny<CartItem>())).Callback(
            (CartItem created) => {
                _createCart(individualEntity.Id, new List<CartItem> {
                    cart.Items[0],
                    created
                });
            }
        );

        NullReferenceException exc = Assert.Throws<NullReferenceException>(() => _cartService.IsInCart(newItem.Id, "void"));
        Assert.Equal("Покупатель с указанным ID не найден", exc.Message);
    }

    [Fact]
    public void IsInCartNoItem() {
        var individualEntity = _createIndividual();
        _commitUserList(new List<User> {individualEntity});

        var item = _createItem(1);

        var cart = _createCart(individualEntity.Id, new List<CartItem> {
            new CartItem {
                CartId = individualEntity.Id,
                ItemId = 1,
                Item = item,
                Quantity = 1,
            }
        });
        
        _mockRepositories.Setup(r => r.Orders.Carts.Create(It.IsAny<Cart>()));
        _mockRepositories.Setup(r => r.Orders.CartItems.Create(It.IsAny<CartItem>())).Callback(
            (CartItem created) => {
                _createCart(individualEntity.Id, new List<CartItem> {
                    cart.Items[0],
                    created
                });
            }
        );

        NullReferenceException exc = Assert.Throws<NullReferenceException>(() => _cartService.IsInCart(2, individualEntity.Id));
        Assert.Equal($"Товара с номером 2 не существует", exc.Message);
    }

    [Fact]
    public void ClearCartNoCustomer() {
        var individualEntity = _createIndividual();
        _commitUserList(new List<User> {individualEntity});
        
        _mockRepositories.Setup(r => r.Users.IndividualEntities.Get(It.Is<string>(id => id == individualEntity.Id)));
        _mockRepositories.Setup(r => r.Users.LegalEntities.Get(It.IsAny<string>()));

        var item = _createItem(1);

        var cart = _createCart(individualEntity.Id, new List<CartItem> {
            new CartItem {
                CartId = individualEntity.Id,
                ItemId = 1,
                Item = item,
                Quantity = 1,
            }
        });
        
        var newItem = _createItem(2);
        
        _mockRepositories.Setup(r => r.Orders.Carts.Create(It.IsAny<Cart>()));
        _mockRepositories.Setup(r => r.Orders.CartItems.Create(It.IsAny<CartItem>())).Callback(
            (CartItem created) => {
                _createCart(individualEntity.Id, new List<CartItem> {
                    cart.Items[0],
                    created
                });
            }
        );

        NullReferenceException exc = Assert.Throws<NullReferenceException>(() => _cartService.ClearCart("void"));
        Assert.Equal("Покупатель с указанным ID не найден", exc.Message);
    }

    [Fact]
    public void GetCartNoCustomer() {
        var individualEntity = _createIndividual();
        _commitUserList(new List<User> {individualEntity});
        
        _mockRepositories.Setup(r => r.Users.IndividualEntities.Get(It.Is<string>(id => id == individualEntity.Id)));
        _mockRepositories.Setup(r => r.Users.LegalEntities.Get(It.IsAny<string>()));

        var item = _createItem(1);

        var cart = _createCart(individualEntity.Id, new List<CartItem> {
            new CartItem {
                CartId = individualEntity.Id,
                ItemId = 1,
                Item = item,
                Quantity = 1,
            }
        });
        
        var newItem = _createItem(2);
        
        _mockRepositories.Setup(r => r.Orders.Carts.Create(It.IsAny<Cart>()));
        _mockRepositories.Setup(r => r.Orders.CartItems.Create(It.IsAny<CartItem>())).Callback(
            (CartItem created) => {
                _createCart(individualEntity.Id, new List<CartItem> {
                    cart.Items[0],
                    created
                });
            }
        );

        NullReferenceException exc = Assert.Throws<NullReferenceException>(() => _cartService.GetCart("void", 0, 10));
        Assert.Equal("Покупатель с указанным ID не найден", exc.Message);
    }

}