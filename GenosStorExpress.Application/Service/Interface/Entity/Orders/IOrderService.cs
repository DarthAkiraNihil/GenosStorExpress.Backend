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
        ShortOrderWrapper? Get(int orderId, string customerId);
        /// <summary>
        /// Получение товаров в заказе
        /// </summary>
        /// <param name="orderId">Номер заказа</param>
        /// <param name="customerId">Номер заказчика</param>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <returns></returns>
        PaginatedOrderItemWrapper? GetItems(int orderId, string customerId, int pageNumber, int pageSize);
        /// <summary>
        /// Получение списка заказов покупателя
        /// </summary>
        /// <param name="customerId">Номер покупателя</param>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <returns>Список всех заказов покупателя</returns>
        PaginatedShortOrderInfoWrapper List(string customerId, int pageNumber, int pageSize);
        /// <summary>
        /// Создание заказа из корзины
        /// </summary>
        /// 
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