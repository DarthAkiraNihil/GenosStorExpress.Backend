using GenosStorExpress.Application.Service.Interface.Entity.Orders;
using GenosStorExpress.Domain.Entity.Orders;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Orders {
    public class BankSystemService: IBankSystemService {
        private readonly IGenosStorExpressRepositories _repositories;

        public BankSystemService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }

        public void Create(string item) {
            var created = new BankSystem { Name = item };
            _repositories.Orders.BankSystems.Create(created);
        }

        public string Get(int id) {
            return _repositories.Orders.BankSystems.Get(id).Name;
        }

        public List<string> List() {
            return _repositories.Orders.BankSystems.List().Select(c => c.Name).ToList();
        }

        public void Update(int id, string item) {
            BankSystem obj = _repositories.Orders.BankSystems.Get(id);
            obj.Name = item;
            _repositories.Orders.BankSystems.Update(obj);
        }

        public void Delete(int id) {
            _repositories.Orders.BankSystems.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        public bool BelongsToEnum(string value) {
            return _repositories.Orders.BankSystems.List().Exists(c => c.Name == value);
        }
    }
}