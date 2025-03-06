using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.ComputerComponents {
    public class PowerSupplyService: IPowerSupplyService {
        private IGenosStorExpressRepositories _repositories;

        public PowerSupplyService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }

        public void Create(PowerSupply item) {
            _repositories.Items.ComputerComponents.PowerSupplies.Create(item);
        }

        public PowerSupply Get(int id) {
            return _repositories.Items.ComputerComponents.PowerSupplies.Get(id);
        }

        public List<PowerSupply> List() {
            return _repositories.Items.ComputerComponents.PowerSupplies.List();
        }

        public void Update(PowerSupply item) {
            _repositories.Items.ComputerComponents.PowerSupplies.Update(item);
        }

        public void Delete(int id) {
            _repositories.Items.ComputerComponents.PowerSupplies.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }
        
        public List<PowerSupply> Filter(List<Func<PowerSupply, bool>> filters) {
            var result = List();
            foreach (var filter in filters) {
                result = result.Where(filter).ToList();
            }
            return result;
        }
    }
}