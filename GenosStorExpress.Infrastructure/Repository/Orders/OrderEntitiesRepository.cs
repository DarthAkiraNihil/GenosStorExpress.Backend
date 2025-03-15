using GenosStorExpress.Domain.Interface.Orders;
using GenosStorExpress.Infrastructure.Context;

namespace GenosStorExpress.Infrastructure.Repository.Orders {
    public class OrderEntitiesRepository: IOrderEntitiesRepository {
        // Orders
        private ActiveDiscountRepository? _activeDiscounts;
        private BankCardRepository? _bankCards;
        private BankSystemRepository? _bankSystems;
        private CartRepository? _carts;
        private CartItemsRepository? _cartItems;
        private OrderItemsRepository? _orderItems;
        private OrderRepository? _orders;
        private OrderStatusRepository? _orderStatuses;
        
        private GenosStorExpressDatabaseContext _context;

        public OrderEntitiesRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }
        
        // Orders
        public IActiveDiscountRepository ActiveDiscounts {
            get {
                if (_activeDiscounts == null) {
                    _activeDiscounts = new ActiveDiscountRepository(_context);
                }
                return _activeDiscounts;
            }
        }
        public IBankCardRepository BankCards {
            get {
                if (_bankCards == null) {
                    _bankCards = new BankCardRepository(_context);
                }
                return _bankCards;
            }
        }
        public IBankSystemRepository BankSystems {
            get {
                if (_bankSystems == null) {
                    _bankSystems = new BankSystemRepository(_context);
                }
                return _bankSystems;
            }
        }
        public ICartRepository Carts {
            get {
                if (_carts == null) {
                    _carts = new CartRepository(_context);
                }
                return _carts;
            }
        }
        public IOrderItemsRepository OrderItems {
            get {
                if (_orderItems == null) {
                    _orderItems = new OrderItemsRepository(_context);
                }
                return _orderItems;
            }
        }
        public IOrderRepository Orders {
            get {
                if (_orders == null) {
                    _orders = new OrderRepository(_context);
                }
                return _orders;
            }
        }
        public IOrderStatusRepository OrderStatuses {
            get {
                if (_orderStatuses == null) {
                    _orderStatuses = new OrderStatusRepository(_context);
                }
                return _orderStatuses;
            }
        }

        public ICartItemsRepository CartItems {
            get {
                if (_cartItems == null) {
                    _cartItems = new CartItemsRepository(_context);
                }
                return _cartItems;
            }
        }
    }
}