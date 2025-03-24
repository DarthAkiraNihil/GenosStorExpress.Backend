using GenosStorExpress.Application.Wrappers.Entity.Orders;
using GenosStorExpress.Domain.Entity.Orders;
using GenosStorExpress.Utils.Operations;

namespace GenosStorExpress.Application.Service.Interface.Entity.Orders {
    /// <summary>
    /// Реализация сервиса заказов
    /// </summary>
    public interface IOrderService: ISupportsSave {
        /// <summary>
        /// Получение заказа по номеру
        /// </summary>
        /// <param name="orderId">Номер заказа</param>
        /// <param name="customerId">Номер заказчика</param>
        /// <returns>Обёртку заказа или null, если у покупателя такой заказ отсутствует</returns>
        OrderWrapper? Get(int orderId, string customerId);
        /// <summary>
        /// Получение списка заказов покупателя
        /// </summary>
        /// <param name="customerId">Номер покупателя</param>
        /// <returns>Список всех заказов покупателя</returns>
        IList<OrderWrapper> List(string customerId);
        /// <summary>
        /// Создание заказа из корзины
        /// </summary>
        /// <param name="customerId">Номер покупателя</param>
        /// <returns>Краткую информацию о созданном заказе</returns>
        ShortOrderWrapper CreateOrderFromCart(string customerId);
        /// <summary>
        /// Подсчёт общей стоимости заказа
        /// </summary>
        /// <param name="orderId">Номер заказа</param>
        /// <returns>Общая стоимость заказа</returns>
        double CalculateTotal(int orderId);
        /// <summary>
        /// Перевод заказа в статус "Получен"
        /// </summary>
        /// <param name="orderId">Номер заказа</param>
        void ReceiveOrder(int orderId);
        /// <summary>
        /// Перевод заказа в статус "Отменён"
        /// </summary>
        /// <param name="orderId">Номер заказа</param>
        void CancelOrder(int orderId);
        /// <summary>
        /// Получение списка активных заказов
        /// </summary>
        /// <returns>Список активных (не отменённых и не полученных) заказов</returns>
        List<ShortOrderWrapper> GetActiveOrders();
        /// <summary>
        /// Получение списка активных заказов в сыром виде. Только для администратора
        /// </summary>
        /// <param name="sudoId">Номер администратора</param>
        /// <returns>Список активных (не отменённых и не полученных) заказов в виде сущностей</returns>
        List<Order> GetActiveOrdersRaw(string sudoId);
        /// <summary>
        /// Проверка существования заказа
        /// </summary>
        /// <param name="orderId">Номер заказа</param>
        /// <returns><c>true</c> если заказ существует, иначе <c>false</c></returns>
        bool OrderExists(int orderId);
    }
}