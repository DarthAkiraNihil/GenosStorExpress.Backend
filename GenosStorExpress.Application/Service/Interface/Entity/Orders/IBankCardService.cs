using GenosStorExpress.Application.Service.Interface.Base;
using GenosStorExpress.Application.Wrappers.Entity.Orders;

namespace GenosStorExpress.Application.Service.Interface.Entity.Orders {
    /// <summary>
    /// Интерфейс сервиса банковских карт
    /// </summary>
    public interface IBankCardService: IStandardService<BankCardWrapper> {
        /// <summary>
        /// Получение пагинированного списка банковских карт покупателя
        /// </summary>
        /// <param name="customerId">Номер покупателя</param>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <returns>Пагинированный список банковских карт покупателя</returns>
        PaginatedBackCardWrapper ListOfCustomer(string customerId, int pageNumber, int pageSize);
    }
}