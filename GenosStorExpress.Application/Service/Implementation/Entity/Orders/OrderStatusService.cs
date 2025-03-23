using GenosStorExpress.Application.Service.Interface.Entity.Orders;
using GenosStorExpress.Application.Wrappers.Enum;
using GenosStorExpress.Domain.Entity.Orders;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Orders;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Orders {
    public class OrderStatusService: IOrderStatusService {

        private readonly IGenosStorExpressRepositories _repositories;
        private readonly IOrderStatusRepository _orderStatuses;

        public OrderStatusService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
            _orderStatuses = _repositories.Orders.OrderStatuses;
        }

        public void Create(string item) {
            var created = new OrderStatus { Name = item };
            _orderStatuses.Create(created);
        }

        public string? Get(int id) {
            return _orderStatuses.Get(id)?.Name;
        }

        public List<string> List() {
            return _orderStatuses.List().Select(c => c.Name).ToList();
        }

        public void Update(int id, string item) {
            OrderStatus obj = _orderStatuses.Get(id)!;
            obj.Name = item;
            _orderStatuses.Update(obj);
        }

        public void Delete(int id) {
            _orderStatuses.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        public bool BelongsToEnum(string value) {
            return _orderStatuses.List().Exists(c => c.Name == value);
        }

        public OrderStatus? GetEntityFromString(string value) {
            return _orderStatuses.List().FirstOrDefault(c => c.Name == value);
        }

        public OrderStatus? GetEntityByDescriptor(OrderStatusDescriptor descriptor) {
            switch(descriptor) {
                case OrderStatusDescriptor.Created: {
                    return GetEntityFromString("Created");
                }
                case OrderStatusDescriptor.Confirmed: {
                    return GetEntityFromString("Confirmed");
                }
                case OrderStatusDescriptor.AwaitsPayment: {
                    return GetEntityFromString("AwaitsPayment");
                }
                case OrderStatusDescriptor.Paid: {
                    return GetEntityFromString("Paid");
                }
                case OrderStatusDescriptor.Processing: {
                    return GetEntityFromString("Processing");
                }
                case OrderStatusDescriptor.Delivering: {
                    return GetEntityFromString("Delivering");
                }
                case OrderStatusDescriptor.Received: {
                    return GetEntityFromString("Received");
                }
                case OrderStatusDescriptor.Cancelled: {
                    return GetEntityFromString("Cancelled");
                }
                default: {
                    return null;
                }
            }
        }
    }
}