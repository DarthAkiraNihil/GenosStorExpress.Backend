using GenosStorExpress.Application.Service.Interface.Entity.Items.SimpleComputerComponents;
using GenosStorExpress.Domain.Entity.Item.SimpleComputerComponent;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.SimpleComputerComponents {
    public class SSDControllerService: ISSDControllerService {
        private IGenosStorExpressRepositories _repositories;

        public SSDControllerService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }

        public void Create(SSDController item) {
            _repositories.Items.SimpleComputerComponents.SSDControllers.Create(item);
        }

        public SSDController Get(int id) {
            return _repositories.Items.SimpleComputerComponents.SSDControllers.Get(id);
        }

        public List<SSDController> List() {
            return _repositories.Items.SimpleComputerComponents.SSDControllers.List();
        }

        public void Update(SSDController item) {
            _repositories.Items.SimpleComputerComponents.SSDControllers.Update(item);
        }

        public void Delete(int id) {
            _repositories.Items.SimpleComputerComponents.SSDControllers.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }
    }
}