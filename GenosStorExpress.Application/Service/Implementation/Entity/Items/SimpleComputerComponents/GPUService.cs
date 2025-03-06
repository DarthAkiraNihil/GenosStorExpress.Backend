using GenosStorExpress.Application.Service.Interface.Entity.Items.SimpleComputerComponents;
using GenosStorExpress.Domain.Entity.Item.SimpleComputerComponent;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.SimpleComputerComponents {
    public class GPUService: IGPUService {
        private IGenosStorExpressRepositories _repositories;

        public GPUService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }

        public void Create(GPU item) {
            _repositories.Items.SimpleComputerComponents.GPUs.Create(item);
        }

        public GPU Get(int id) {
            return _repositories.Items.SimpleComputerComponents.GPUs.Get(id);
        }

        public List<GPU> List() {
            return _repositories.Items.SimpleComputerComponents.GPUs.List();
        }

        public void Update(GPU item) {
            _repositories.Items.SimpleComputerComponents.GPUs.Update(item);
        }

        public void Delete(int id) {
            _repositories.Items.SimpleComputerComponents.GPUs.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }
    }
}