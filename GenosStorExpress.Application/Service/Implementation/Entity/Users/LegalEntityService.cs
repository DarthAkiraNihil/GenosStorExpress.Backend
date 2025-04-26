using GenosStorExpress.Application.Service.Interface.Entity.Users;
using GenosStorExpress.Application.Wrappers.Entity.Users;
using GenosStorExpress.Domain.Entity.User;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Users {
    /// <summary>
    /// Реализация сервиса юридических лиц
    /// </summary>
    public class LegalEntityService : ILegalEntityService {

        private readonly IGenosStorExpressRepositories _repositories;

        /// <summary>
        /// Стандартный конструктор
        /// </summary>
        /// <param name="repositories"></param>
        public LegalEntityService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }

        /// <summary>
        /// Подтверждение юридического лица
        /// </summary>
        /// <param name="sudoId">Номе администратора</param>
        /// <param name="legalEntityId">Номер юридического лица</param>
        public void Verify(string sudoId, string legalEntityId) {

            Administrator? sudo = _repositories.Users.Administrators.Get(sudoId);
            if (sudo == null) {
                throw new NullReferenceException("Запрещено! Данный метод может быть вызван только администратором");
            }

            LegalEntity? legalEntity = _repositories.Users.LegalEntities.Get(legalEntityId);

            if (legalEntity == null) {
                throw new NullReferenceException($"Юридического лица с номером {legalEntityId} не существует");
            }
            
            if (legalEntity.IsVerified) {
                throw new ArgumentException($"Юридическое лицо с номером {legalEntityId} уже подтверждено");
            }

            legalEntity.IsVerified = true;
            _repositories.Users.LegalEntities.Update(legalEntity);
            _repositories.Save();
        }

        /// <summary>
        /// Отзыв верификации
        /// </summary>
        /// <param name="sudoId">Номе администратора</param>
        /// <param name="legalEntityId">Номер юридического лица</param>
        public void Revoke(string sudoId, string legalEntityId) {

            Administrator? sudo = _repositories.Users.Administrators.Get(sudoId);
            if (sudo == null) {
                throw new NullReferenceException("Запрещено! Данный метод может быть вызван только администратором");
            }

            LegalEntity? legalEntity = _repositories.Users.LegalEntities.Get(legalEntityId);

            if (legalEntity == null) {
                throw new NullReferenceException($"Юридического лица с номером {legalEntityId} не существует");
            }

            foreach (var bankCard in legalEntity.BankCards) {
                _repositories.Orders.BankCards.Delete(bankCard.Id);
            }

            _repositories.Orders.Carts.DeleteRaw(legalEntity.Cart);
            _repositories.Users.LegalEntities.Delete(legalEntity.Id);
            _repositories.Save();
        }

        /// <summary>
        /// Получение подтверждённых юридических лиц
        /// </summary>
        /// <param name="sudoId">Номер администратора</param>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <returns></returns>
        public PaginatedLegalEntityWrapper GetVerified(string sudoId, int pageNumber, int pageSize) {
            Administrator? sudo = _repositories.Users.Administrators.Get(sudoId);
            if (sudo == null) {
                throw new UnauthorizedAccessException("ACCESS DENIED!");
            }

            var list = _repositories.Users
                                    .LegalEntities
                                    .List()
                                    .Where(e => e.IsVerified)
                                    .ToList();

            return new PaginatedLegalEntityWrapper {
                Count = list.Count,
                Previous = pageNumber == 1 ? null : (pageNumber - 1).ToString(),
                Next = (pageNumber + 1) * pageSize >= list.Count ? null : (pageNumber + 1).ToString(),
                Items = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(
                    i => new LegalEntityWrapper {
                        Id = i.Id,
                        Email = i.Email!,
                        INN = i.INN,
                        KPP = i.KPP,
                        PhysicalAddress = i.PhysicalAddress,
                        LegalAddress = i.LegalAddress
                    }
                ).ToList()
            };
        }

        /// <summary>
        /// Получение юридических лиц, ожидающих подтверждения
        /// </summary>
        /// <param name="sudoId">Номер администратора</param>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <returns></returns>
        public PaginatedLegalEntityWrapper GetWaitingForVerification(string sudoId, int pageNumber, int pageSize) {
            Administrator? sudo = _repositories.Users.Administrators.Get(sudoId);
            if (sudo == null) {
                throw new UnauthorizedAccessException("ACCESS DENIED!");
            }

            var list = _repositories.Users
                                    .LegalEntities
                                    .List()
                                    .Where(e => !e.IsVerified)
                                    .ToList();

            return new PaginatedLegalEntityWrapper {
                Count = list.Count,
                Previous = pageNumber == 1 ? null : (pageNumber - 1).ToString(),
                Next = (pageNumber + 1) * pageSize >= list.Count ? null : (pageNumber + 1).ToString(),
                Items = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(
                    i => new LegalEntityWrapper {
                        Id = i.Id,
                        Email = i.Email!,
                        INN = i.INN,
                        KPP = i.KPP,
                        PhysicalAddress = i.PhysicalAddress,
                        LegalAddress = i.LegalAddress
                    }
                ).ToList()
            };
        }
    }
}