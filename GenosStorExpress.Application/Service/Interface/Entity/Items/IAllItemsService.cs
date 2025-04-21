using GenosStorExpress.Application.Wrappers.Entity;
using GenosStorExpress.Application.Wrappers.Entity.Item;
using GenosStorExpress.Utils.Operations;

namespace GenosStorExpress.Application.Service.Interface.Entity.Items {
    /// <summary>
    /// Интерфейс для общего сервиса товаров
    /// </summary>
    public interface IAllItemsService :
        ISupportsGet<ItemWrapper>,
        ISupportsList<ItemWrapper>,
        ISupportsDelete,
        ISupportsSave {
        /// <summary>
        /// Оставление отзыва на товар
        /// </summary>
        /// <param name="itemId">Номер товара</param>
        /// <param name="customerId">Номер покупателя</param>
        /// <param name="review">Отзыв</param>
        void LeaveReview(int itemId, string customerId, ReviewWrapper review);
        /// <summary>
        /// Получение отзывов на товар
        /// </summary>
        /// <param name="itemId">Номер товара</param>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <returns>Список отзывов на товар</returns>
        PaginatedReviewWrapper GetReviews(int itemId, int pageNumber, int pageSize);
        /// <summary>
        /// Получение отзыва на товар от конкретного покупателя
        /// </summary>
        /// <param name="itemId">Номер товара</param>
        /// <param name="customerId">Номер покупателя</param>
        /// <returns></returns>
        ReviewWrapper? GetReview(int itemId, string customerId);
        /// <summary>
        /// Установка изображения товара. Только под администратором
        /// </summary>
        /// <param name="sudoId">Номер администратора</param>
        /// <param name="itemId">Номер товара</param>
        /// <param name="stream">Поток данных изображения</param>
        void SetImage(string sudoId, int itemId, MemoryStream stream);
    }
}