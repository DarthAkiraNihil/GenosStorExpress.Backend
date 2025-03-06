using GenosStorExpress.Application.Service.Interface.Entity.Orders;
using GenosStorExpress.Domain.Entity.Orders;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Orders {
    public class OrderStatusService: IOrderStatusService {

        private readonly IGenosStorExpressRepositories _repositories;

        public void Create(string item) {
            var created = new OrderStatus { Name = item };
            _repositories.Orders.OrderStatuses.Create(created);
        }

        public string Get(int id) {
            return _repositories.Orders.OrderStatuses.Get(id).Name;
        }

        public List<string> List() {
            return _repositories.Orders.OrderStatuses.List().Select(c => c.Name).ToList();
        }

        public void Update(int id, string item) {
            OrderStatus obj = _repositories.Orders.OrderStatuses.Get(id);
            obj.Name = item;
            _repositories.Orders.OrderStatuses.Update(obj);
        }

        public void Delete(int id) {
            _repositories.Orders.OrderStatuses.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        public bool BelongsToEnum(string value) {
            return _repositories.Orders.OrderStatuses.List().Exists(c => c.Name == value);
        }

    }
}