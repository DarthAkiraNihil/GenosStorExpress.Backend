using GenosStorExpress.Application.Wrappers.Entity.Users;

namespace GenosStorExpress.Application.Service.Interface.Entity.Users {
    /// <summary>
    /// Интерфейс сервиса юридических лиц
    /// </summary>
    public interface ILegalEntityService {
        /// <summary>
        /// Подтверждение юридического лица
        /// </summary>
        /// <param name="sudoId">Номе администратора</param>
        /// <param name="legalEntityId">Номер юридического лица</param>
        void Verify(string sudoId, string legalEntityId);
        /// <summary>
        /// Отзыв верификации
        /// </summary>
        /// <param name="sudoId">Номе администратора</param>
        /// <param name="legalEntityId">Номер юридического лица</param>
        void Revoke(string sudoId, string legalEntityId);
        /// <summary>
        /// Получение подтверждённых юридических лиц
        /// </summary>
        /// <param name="sudoId">Номер администратора</param>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <returns></returns>
        PaginatedLegalEntityWrapper GetVerified(string sudoId, int pageNumber, int pageSize);
        /// <summary>
        /// Получение юридических лиц, ожидающих подтверждения
        /// </summary>
        /// <param name="sudoId">Номер администратора</param>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <returns></returns>
        PaginatedLegalEntityWrapper GetWaitingForVerification(string sudoId, int pageNumber, int pageSize);
    }
}