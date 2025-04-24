using GenosStorExpress.Application.Service.Interface.Base;
using GenosStorExpress.Application.Wrappers.Entity.Orders;

namespace GenosStorExpress.Application.Service.Interface.Entity.Orders {
    /// <summary>
    /// Интерфейс сервиса банковских карт
    /// </summary>
    public interface IBankCardService {
        /// <summary>
        /// Добавление новой карты для покупателя
        /// </summary>
        /// <param name="customerId">Номер покупателя</param>
        /// <param name="item">Данные карты</param>
        void Create(string customerId, BankCardWrapper item);
        /// <summary>
        /// Получение данных о карте
        /// </summary>
        /// <param name="customerId">Номер покупателя</param>
        /// <param name="id">Номер карты</param>
        /// <returns></returns>
        BankCardWrapper? Get(string customerId, int id);
        void Update(string customerId, int id, BankCardWrapper item);
        void Delete(string customerId, int id);
        int Save();
        /// <summary>
        /// Получение пагинированного списка банковских карт покупателя
        /// </summary>
        /// <param name="customerId">Номер покупателя</param>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <returns>Пагинированный список банковских карт покупателя</returns>
        PaginatedBankCardWrapper List(string customerId, int pageNumber, int pageSize);
    }
}