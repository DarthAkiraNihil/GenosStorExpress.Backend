using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.ComputerComponents {
    public class MotherboardService: IMotherboardService {

        private IGenosStorExpressRepositories _repositories;

        public void Create(Motherboard item) {
            _repositories.Items.ComputerComponents.Motherboards.Create(item);
        }

        public Motherboard Get(int id) {
            return _repositories.Items.ComputerComponents.Motherboards.Get(id);
        }

        public List<Motherboard> List() {
            return _repositories.Items.ComputerComponents.Motherboards.List();
        }

        public void Update(Motherboard item) {
            _repositories.Items.ComputerComponents.Motherboards.Update(item);
        }

        public void Delete(int id) {
            _repositories.Items.ComputerComponents.Motherboards.Delete(id);
        }

        public List<Motherboard> Filter(List<Func<Motherboard, bool>> filters) {
            var result = _repositories.Items.ComputerComponents.Motherboards.List();
            foreach (var filter in filters) {
                result = result.Where(filter).ToList();
            }
            return result;
        }
        
        public int Save() {
            return _repositories.Save();
        }

        public MotherboardService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }
    }
}