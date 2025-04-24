using GenosStorExpress.Application.Service.Interface.Entity.Orders;
using GenosStorExpress.Application.Wrappers.Entity.Orders;
using GenosStorExpress.Domain.Entity.Orders;
using GenosStorExpress.Domain.Entity.User;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Orders;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Orders {
    /// <summary>
    /// Реализация сервиса банковских карт
    /// </summary>
    public class BankCardsService: IBankCardService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly IBankCardRepository _bankCards;
        private readonly IBankSystemService _bankSystemService;

        /// <summary>
        /// Стандартный конструктор
        /// </summary>
        /// <param name="repositories">Репозитории проекта</param>
        /// <param name="bankSystemService">Сервис банковских систем</param>
        public BankCardsService(IGenosStorExpressRepositories repositories, IBankSystemService bankSystemService) {
            _repositories = repositories;
            _bankSystemService = bankSystemService;
            _bankCards = _repositories.Orders.BankCards;
        }
        
        private Customer? _getCustomer(string id) {
            Customer? customer = _repositories.Users.IndividualEntities.Get(id);
            if (customer == null) {
                return _repositories.Users.LegalEntities.Get(id);
            }
            return customer;
        }

        /// <summary>
        /// Создание сущности карты из обёртки
        /// </summary>
        /// <param name="item">Обёртка карты</param>
        /// <exception cref="NullReferenceException">Если банковская система не найдена</exception>
        public void Create(string customerId, BankCardWrapper item) {
            
            Customer? customer = _getCustomer(customerId);

            if (customer == null) {
                throw new NullReferenceException("Покупатель с указанным ID не найден");
            }

            var bankSystem = _bankSystemService.GetEntityFromString(item.BankSystem);
            if (bankSystem == null) {
                throw new NullReferenceException($"Банковской системы {item.BankSystem} не существует");
            }
            
            customer.BankCards.Add(new BankCard {
                Number = item.Number,
                ValidThruMonth = item.ValidThruMonth,
                ValidThruYear = item.ValidThruYear,
                CVC = item.CVC,
                Owner = item.Owner,
                BankSystem = bankSystem
            });
            // Save();
        }

        public BankCardWrapper? Get(string customerId, int id) {
            
            Customer? customer = _getCustomer(customerId);

            if (customer == null) {
                throw new NullReferenceException("Покупатель с указанным ID не найден");
            }
            
            var obj =  customer.BankCards.FirstOrDefault(i => i.Id == id);
            if (obj == null) {
                return null;
            }
            return new BankCardWrapper {
                Id = obj.Id,
                Number = obj.Number,
                ValidThruMonth = obj.ValidThruMonth,
                ValidThruYear = obj.ValidThruYear,
                CVC = obj.CVC,
                Owner = obj.Owner,
                BankSystem = obj.BankSystem.Name
            };
        }
        

        /// <summary>
        /// Получение пагинированного списка банковских карт покупателя
        /// </summary>
        /// <param name="customerId">Номер покупателя</param>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <returns>Пагинированный список банковских карт покупателя</returns>
        public PaginatedBankCardWrapper List(string customerId, int pageNumber, int pageSize) {
            var cards = _bankCards
                .List()
                .Where(c => c.CustomerId == customerId).ToList();

            return new PaginatedBankCardWrapper {
                Count = cards.Count,
                Previous = pageNumber == 1 ? null : (pageNumber - 1).ToString(),
                Next = (pageNumber + 1) * pageSize >= cards.Count ? null : (pageNumber + 1).ToString(),
                Items = cards.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(
                    obj => new BankCardWrapper {
                        Id = obj.Id,
                        Number = obj.Number,
                        ValidThruMonth = obj.ValidThruMonth,
                        ValidThruYear = obj.ValidThruYear,
                        CVC = obj.CVC,
                        Owner = obj.Owner,
                        BankSystem = obj.BankSystem.Name
                    }
                ).ToList()
            };
        }

        public void Update(string customerId, int id, BankCardWrapper item) {
            
            Customer? customer = _getCustomer(customerId);

            if (customer == null) {
                throw new NullReferenceException("Покупатель с указанным ID не найден");
            }
            
            var obj = customer.BankCards.FirstOrDefault(i => i.Id == id);

            if (obj == null) {
                throw new NullReferenceException($"Банковской карты с номером {id} существует");
            }
            
            obj.Number = item.Number;
            obj.ValidThruMonth = item.ValidThruMonth;
            obj.ValidThruYear = item.ValidThruYear;
            obj.CVC = item.CVC;
            obj.Owner = item.Owner;
            
            var bankSystem = _bankSystemService.GetEntityFromString(item.BankSystem);
            if (bankSystem == null) {
                throw new NullReferenceException($"Банковской системы {item.BankSystem} не существует");
            }

            obj.BankSystem = bankSystem;
            _bankCards.Update(obj);
        }

        public void Delete(string customerId, int id) {
            Customer? customer = _getCustomer(customerId);

            if (customer == null) {
                throw new NullReferenceException("Покупатель с указанным ID не найден");
            }
            
            var obj = customer.BankCards.FirstOrDefault(i => i.Id == id);

            if (obj == null) {
                throw new NullReferenceException($"Банковской карты с номером {id} существует");
            }
            _repositories.Orders.BankCards.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }
    }
}