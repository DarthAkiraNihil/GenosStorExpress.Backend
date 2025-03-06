using GenosStorExpress.Application.Wrappers.Enum;
using GenosStorExpress.Application.Service.Interface.Common;
using GenosStorExpress.Domain.Entity.Orders;
using GenosStorExpress.Domain.Entity.User;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Common {
    public class PaymentService: IPaymentService {
        private IGenosStorExpressRepositories _repositories;

        public PaymentService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }

        public string GetOrdererInfo(Customer customer) {
            if (customer == null) {
                return string.Empty;
            }

            if (customer is IndividualEntity) {
                var ie = (IndividualEntity)customer;
                return $"{ie.Name} {ie.Surname}";
            }

            if (customer is LegalEntity) {
                var le = (LegalEntity)customer;
                return $"{le.Email} (ИНН: {le.INN})";
            }
            
            return string.Empty;
        }

        public bool ProcessPayment(Order order) {
            var paid = _repositories.Orders.OrderStatuses.List().First(os => os.Id == (int) OrderStatusDescriptor.Paid);
            
            double chance = new Random().NextDouble();
            if (chance <= 0.05) {
                return false;
            }

            order.OrderStatus = paid;
            _repositories.Orders.Orders.Update(order);
            _repositories.Save();
            return true;
        }
    }
}