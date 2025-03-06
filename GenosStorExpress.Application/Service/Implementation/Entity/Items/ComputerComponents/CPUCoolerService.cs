using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.ComputerComponents {
    public class CPUCoolerService: ICPUCoolerService {
        private IGenosStorExpressRepositories _repositories;

        public CPUCoolerService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }

        public void Create(CPUCooler item) {
            _repositories.Items.ComputerComponents.CPUCoolers.Create(item);
        }

        public CPUCooler Get(int id) {
            return _repositories.Items.ComputerComponents.CPUCoolers.Get(id);
        }

        public List<CPUCooler> List() {
            return _repositories.Items.ComputerComponents.CPUCoolers.List();
        }

        public void Update(CPUCooler item) {
            _repositories.Items.ComputerComponents.CPUCoolers.Update(item);
        }

        public void Delete(int id) {
            _repositories.Items.ComputerComponents.CPUCoolers.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }
        
        public List<CPUCooler> Filter(List<Func<CPUCooler, bool>> filters) {
            var result = List();
            foreach (var filter in filters) {
                result = result.Where(filter).ToList();
            }
            return result;
        }
    }
}