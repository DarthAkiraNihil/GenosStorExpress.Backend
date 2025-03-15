using GenosStorExpress.Application.Service.Interface.Entity.Orders;
using GenosStorExpress.Application.Wrappers.Entity.Orders;
using GenosStorExpress.Domain.Entity.Orders;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Orders;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Orders {
    public class BankCardsService: IBankCardService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly IBankCardRepository _bankCards;
        private readonly IBankSystemService _bankSystemService;

        public BankCardsService(IGenosStorExpressRepositories repositories, IBankSystemService bankSystemService) {
            _repositories = repositories;
            _bankSystemService = bankSystemService;
            _bankCards = _repositories.Orders.BankCards;
        }

        public void Create(BankCardWrapper item) {

            var bankSystem = _bankSystemService.GetEntityFromString(item.BankSystem);
            if (bankSystem == null) {
                throw new NullReferenceException($"Банковской системы {item.BankSystem} не существует");
            }
            
            _bankCards.Create(new BankCard {
                Number = item.Number,
                ValidThruMonth = item.ValidThruMonth,
                ValidThruYear = item.ValidThruYear,
                CVC = item.CVC,
                Owner = item.Owner,
                BankSystem = bankSystem
            });
        }

        public BankCardWrapper? Get(int id) {
            var obj =  _bankCards.Get(id);
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

        public List<BankCardWrapper> List() {
            return _bankCards.List().Select(obj => new BankCardWrapper {
                Id = obj.Id,
                Number = obj.Number,
                ValidThruMonth = obj.ValidThruMonth,
                ValidThruYear = obj.ValidThruYear,
                CVC = obj.CVC,
                Owner = obj.Owner,
                BankSystem = obj.BankSystem.Name
            }).ToList();
        }

        public void Update(int id, BankCardWrapper item) {
            var obj = _bankCards.Get(id);

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

        public void Delete(int id) {
            _repositories.Orders.BankCards.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }
    }
}