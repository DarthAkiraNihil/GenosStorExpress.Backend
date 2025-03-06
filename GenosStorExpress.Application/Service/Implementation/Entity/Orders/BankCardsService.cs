using GenosStorExpress.Application.Service.Interface.Entity.Orders;
using GenosStorExpress.Domain.Entity.Orders;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Orders {
    public class BankCardsService: IBankCardService {
        private IGenosStorExpressRepositories _repositories;

        public BankCardsService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }

        public void Create(BankCard item) {
            _repositories.Orders.BankCards.Create(item);
        }

        public BankCard Get(int id) {
            return _repositories.Orders.BankCards.Get(id);
        }

        public List<BankCard> List() {
            return _repositories.Orders.BankCards.List();
        }

        public void Update(BankCard item) {
            _repositories.Orders.BankCards.Update(item);
        }

        public void Delete(int id) {
            _repositories.Orders.BankCards.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }
    }
}