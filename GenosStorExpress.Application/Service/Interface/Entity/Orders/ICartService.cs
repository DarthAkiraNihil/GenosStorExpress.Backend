using GenosStorExpress.Application.Wrappers.Entity.Orders;

namespace GenosStorExpress.Application.Service.Interface.Entity.Orders {
    /// <summary>
    /// Интерфейс сервиса корзин
    /// </summary>
    public interface ICartService {
        /// <summary>
        /// Добавление товара в корзину
        /// </summary>
        /// <param name="itemId">Номер товара</param>
        /// <param name="customerId">Номер покупателя</param>
        void AddToCart(int itemId, string customerId);
        /// <summary>
        /// Удаление товара из корзины
        /// </summary>
        /// <param name="itemId">Номер товара</param>
        /// <param name="customerId">Номер покупателя</param>
        void RemoveFromCart(int itemId, string customerId);
        /// <summary>
        /// Увеличение количества товара в корзине на 1
        /// </summary>
        /// <param name="itemId">Номер товара</param>
        /// <param name="customerId">Номер покупателя</param>
        void IncrementCartItemQuantity(int itemId, string customerId);
        /// <summary>
        /// Уменьшение количества товара в корзине на 1 (или удаление, если он только один в корзине)
        /// </summary>
        /// <param name="itemId">Номер товара</param>
        /// <param name="customerId">Номер покупателя</param>
        void DecrementCartItemQuantity(int itemId, string customerId);
        /// <summary>
        /// Проверка наличия товара в корзине
        /// </summary>
        /// <param name="itemId">Номер товара</param>
        /// <param name="customerId">Номер покупателя</param>
        /// <returns>true если товар есть в корзине, иначе false</returns>
        bool IsInCart(int itemId, string customerId);
        /// <summary>
        /// Очистка корзины
        /// </summary>
        /// <param name="customerId">Номер покупателя</param>
        void ClearCart(string customerId);
        /// <summary>
        /// Получение содержимого корзины
        /// </summary>
        /// <param name="customerId">Номер покупателя</param>
        /// <returns>Содержимое корзины покупателя</returns>
        CartWrapper GetCart(string customerId);
        
    }
}