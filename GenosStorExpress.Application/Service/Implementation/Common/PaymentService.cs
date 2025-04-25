using GenosStorExpress.Application.Service.Interface.Common;
using GenosStorExpress.Application.Wrappers.Enum;
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

        public bool ProcessPayment(int orderId, int bankCardId, string customerId) {
            
            Order? order = _repositories.Orders.Orders.Get(orderId);

            if (order == null || order.CustomerId != customerId) {
                throw new NullReferenceException($"Заказа с номером {orderId} не существует");
            }

            if (order.OrderStatus!.Name != "AwaitsPayment") {
                throw new NullReferenceException($"Попытка оплатить заказ, не ожидающий оплаты. Запрос отконён");
            }
            
            BankCard? card = _repositories.Orders.BankCards.Get(bankCardId);

            if (card == null) {
                throw new NullReferenceException("Указанной банковской карты не существует");
            }
            
            var paid = _repositories.Orders.OrderStatuses.List().First(os => os.Id == (int) OrderStatusDescriptor.Paid);
            
            // Вот тут магия оплаты...
            
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