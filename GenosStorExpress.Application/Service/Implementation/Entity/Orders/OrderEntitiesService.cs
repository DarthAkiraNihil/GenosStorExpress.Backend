using GenosStorExpress.Application.Service.Interface.Entity.Orders;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Orders {
    public class OrderEntitiesService: IOrderEntitiesService {
        private ICartService _cartService;
        private IOrderService _orderService;
        private IOrderStatusService _orderStatusService;
        private IActiveDiscountService _activeDiscountService;
        private readonly IBankSystemService _bankSystemService;
        private readonly IBankCardService _bankCardService;

        public ICartService Carts {
            get { return _cartService; }
        }

        public IOrderService Orders {
            get { return _orderService; }
        }

        public IOrderStatusService OrderStatuses {
            get { return _orderStatusService; }
        }

        public IActiveDiscountService ActiveDiscounts {
            get { return _activeDiscountService; }
        }

        public IBankSystemService BankSystems {
            get { return _bankSystemService; }
        }

        public IBankCardService BankCards {
            get { return _bankCardService; }
        }

        public OrderEntitiesService(
            ICartService cartService,
            IOrderService orderService,
            IOrderStatusService orderStatusService,
            IActiveDiscountService activeDiscountService,
            IBankSystemService bankSystemService,
            IBankCardService bankCardService
            ) {
            _cartService = cartService;
            _orderService = orderService;
            _orderStatusService = orderStatusService;
            _activeDiscountService = activeDiscountService;
            _bankSystemService = bankSystemService;
            _bankCardService = bankCardService;
        }
    }
}