using GenosStorExpress.Application.Service.Interface.Entity.Items.SimpleComputerComponents;
using GenosStorExpress.Domain.Entity.Item.SimpleComputerComponent;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.SimpleComputerComponents {
    public class CPUCoresService: ICPUCoreService {
        private IGenosStorExpressRepositories _repositories;

        public CPUCoresService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }

        public void Create(CPUCore item) {
            _repositories.Items.SimpleComputerComponents.CPUCores.Create(item);
        }

        public CPUCore Get(int id) {
            return _repositories.Items.SimpleComputerComponents.CPUCores.Get(id);
        }

        public List<CPUCore> List() {
            return _repositories.Items.SimpleComputerComponents.CPUCores.List();
        }

        public void Update(CPUCore item) {
            _repositories.Items.SimpleComputerComponents.CPUCores.Update(item);
        }

        public void Delete(int id) {
            _repositories.Items.SimpleComputerComponents.CPUCores.Delete(id);
        }
        
        public int Save() {
            return _repositories.Save();
        }
    }
}