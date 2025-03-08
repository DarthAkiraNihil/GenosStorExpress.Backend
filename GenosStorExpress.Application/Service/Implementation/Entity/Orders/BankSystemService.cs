using GenosStorExpress.Application.Service.Interface.Entity.Orders;
using GenosStorExpress.Domain.Entity.Orders;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Orders;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Orders {
    public class BankSystemService: IBankSystemService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly IBankSystemRepository _bankSystems;

        public BankSystemService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
            _bankSystems = _repositories.Orders.BankSystems;
        }

        public void Create(string item) {
            var created = new BankSystem { Name = item };
            _bankSystems.Create(created);
        }

        public string Get(int id) {
            return _bankSystems.Get(id).Name;
        }

        public List<string> List() {
            return _bankSystems.List().Select(c => c.Name).ToList();
        }

        public void Update(int id, string item) {
            BankSystem obj = _bankSystems.Get(id);
            obj.Name = item;
            _bankSystems.Update(obj);
        }

        public void Delete(int id) {
            _bankSystems.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        public bool BelongsToEnum(string value) {
            return _bankSystems.List().Exists(c => c.Name == value);
        }

        public BankSystem GetEntityFromString(string value) {
            return _bankSystems.List().FirstOrDefault(c => c.Name == value, null);
        }
    }
}