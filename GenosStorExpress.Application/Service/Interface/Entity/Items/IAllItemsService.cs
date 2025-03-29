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
        /// <param name="review">Отзыв</param>
        void LeaveReview(int itemId, ReviewWrapper review);
        /// <summary>
        /// Получение отзывов на товар
        /// </summary>
        /// <param name="itemId">Номер товара</param>
        /// <returns>Список отзывов на товар</returns>
        IList<ReviewWrapper> GetReviews(int itemId);
        void SetImage(string sudoId, int itemId, MemoryStream stream);
    }
}