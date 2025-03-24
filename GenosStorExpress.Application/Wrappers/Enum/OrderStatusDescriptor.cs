namespace GenosStorExpress.Application.Wrappers.Enum {
    /// <summary>
    /// Перечисление-обёртка для статуса заказа
    /// </summary>
    public enum OrderStatusDescriptor {
        /// <summary>
        /// Создан
        /// </summary>
        Created = 1,
        /// <summary>
        /// Подтверждён
        /// </summary>
        Confirmed,
        /// <summary>
        /// Ожидает оплаты
        /// </summary>
        AwaitsPayment,
        /// <summary>
        /// Оплачен
        /// </summary>
        Paid,
        /// <summary>
        /// Обрабатывается
        /// </summary>
        Processing,
        /// <summary>
        /// Доставляется
        /// </summary>
        Delivering,
        /// <summary>
        /// Получен
        /// </summary>
        Received,
        /// <summary>
        /// Отменён
        /// </summary>
        Cancelled,
    }
}