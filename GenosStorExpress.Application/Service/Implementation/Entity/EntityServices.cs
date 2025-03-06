using GenosStorExpress.Application.Service.Interface.Entity;
using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Orders;
using GenosStorExpress.Application.Service.Interface.Entity.Users;

namespace GenosStorExpress.Application.Service.Implementation.Entity {
    public class EntityServices: IEntityServices {
        
        private IItemServices _itemServices;

        public IItemServices Items {
            get {
                return _itemServices;
            }
        }
        
        private IOrderEntitiesService _orderService;

        public IOrderEntitiesService Orders {
            get
            {
                return _orderService;
            }
        }
        
        private readonly IUserEntitiesService _userEntitiesService;
        
        public IUserEntitiesService Users => _userEntitiesService;

        public EntityServices(
            IItemServices itemServices,
            IOrderEntitiesService orderService,
            IUserEntitiesService userEntitiesService
            ) {
            _itemServices = itemServices;
            _orderService = orderService;
            _userEntitiesService = userEntitiesService;
        }
    }
}