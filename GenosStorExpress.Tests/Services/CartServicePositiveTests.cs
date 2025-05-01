using GenosStorExpress.Application.Service.Implementation.Entity.Orders;
using GenosStorExpress.Domain.Entity.Orders;
using GenosStorExpress.Domain.Entity.User;
using Moq;

namespace GenosStorExpress.Tests.Services;

public class CartServicePositiveTests: Test {
    
    private readonly CartService _cartService;

    public CartServicePositiveTests() {
        _cartService = new CartService(
            _mockRepositories.Object,
            _mockAllItemsService.Object
        );
    }

    [Fact]
    public void AddToCartSuccess() {
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
        
        // Act
        _cartService.AddToCart(newItem.Id, individualEntity.Id);
        var result =  _cartService.GetCart(individualEntity.Id, 0, 10);
        
        // Assert
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void RemoveFromCartSuccess() {
        var individualEntity = _createIndividual();
        _commitUserList(new List<User> {individualEntity});

        var item = _createItem(1);
        var anotherItem = _createItem(2);

        var cart = _createCart(individualEntity.Id, new List<CartItem> {
            new CartItem {
                CartId = individualEntity.Id,
                ItemId = 1,
                Item = item,
                Quantity = 1,
            },
            new CartItem {
                CartId = individualEntity.Id,
                ItemId = 2,
                Item = anotherItem,
                Quantity = 1,
            },
        });

        _mockRepositories.Setup(r => r.Orders.Carts.Create(It.IsAny<Cart>()));
        _mockRepositories.Setup(r => r.Orders.CartItems.DeleteRaw(It.IsAny<CartItem>())).Callback(
            (CartItem deleted) => {
                _createCart(individualEntity.Id, cart.Items.Where(x => x.ItemId != deleted.ItemId).ToList());
            }
        );
        
        // Act
        _cartService.RemoveFromCart(anotherItem.Id, individualEntity.Id);
        var result =  _cartService.GetCart(individualEntity.Id, 0, 10);
        
        // Assert
        Assert.Equal(1, result.Count);
    }

    [Fact]
    public void IncrementItemQuantitySuccess() {
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
        
        _mockRepositories.Setup(r => r.Orders.CartItems.Update(It.IsAny<CartItem>()));
        
        // Act
        _cartService.IncrementCartItemQuantity(item.Id, individualEntity.Id);
        
        var result =  _cartService.GetCart(individualEntity.Id, 0, 10);
        
        // Assert
        Assert.Equal(1, result.Count);
        Assert.Equal(2, result.Items[0].Quantity);
    }

    [Fact]
    public void DecrementItemQuantitySuccess() {
        var individualEntity = _createIndividual();
        _commitUserList(new List<User> {individualEntity});

        var item = _createItem(1);

        _createCart(individualEntity.Id, new List<CartItem> {
            new CartItem {
                CartId = individualEntity.Id,
                ItemId = 1,
                Item = item,
                Quantity = 2,
            }
        });
        
        _mockRepositories.Setup(r => r.Orders.CartItems.Update(It.IsAny<CartItem>()));
        
        // Act
        _cartService.DecrementCartItemQuantity(item.Id, individualEntity.Id);
        
        var result =  _cartService.GetCart(individualEntity.Id, 0, 10);
        
        // Assert
        Assert.Equal(1, result.Count);
        Assert.Equal(1, result.Items[0].Quantity);
    }

    [Fact]
    public void IsInCartSuccess() {
        var individualEntity = _createIndividual();
        _commitUserList(new List<User> {individualEntity});

        var item = _createItem(1);

        _createCart(individualEntity.Id, new List<CartItem> {
            new CartItem {
                CartId = individualEntity.Id,
                ItemId = 1,
                Item = item,
                Quantity = 2,
            }
        });

        Assert.True(_cartService.IsInCart(item.Id, individualEntity.Id));
    }

    [Fact]
    public void ClearCartSuccess() {
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
        _mockRepositories.Setup(r => r.Orders.CartItems.DeleteRaw(It.IsAny<CartItem>())).Callback(
            (CartItem deleted) => {
                _createCart(individualEntity.Id, cart.Items.Where(x => x.ItemId != deleted.ItemId).ToList());
            }
        );
        
        // Act
        _cartService.ClearCart(individualEntity.Id);
        var result =  _cartService.GetCart(individualEntity.Id, 0, 10);

        // Assert
        Assert.Equal(0, result.Count);
    }

    [Fact]
    public void GetCartSuccess() {
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
        
        _mockRepositories.Setup(r => r.Orders.Carts.Create(It.IsAny<Cart>()));
        
        // Act
        var result =  _cartService.GetCart(individualEntity.Id, 0, 10);
        
        // Assert
        Assert.Equal(1, result.Count);
        Assert.Equal(1, result.Items[0].Quantity);
    }

}