using GenosStorExpress.Application.Service.Interface.Entity.Items.SimpleComputerComponents;
using GenosStorExpress.Domain.Entity.Item.SimpleComputerComponent;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.SimpleComputerComponents {
    public class NetworkAdapterService: INetworkAdapterService {
        private IGenosStorExpressRepositories _repositories;

        public NetworkAdapterService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }

        public void Create(NetworkAdapter item) {
            _repositories.Items.SimpleComputerComponents.NetworkAdapters.Create(item);
        }

        public NetworkAdapter Get(int id) {
            return _repositories.Items.SimpleComputerComponents.NetworkAdapters.Get(id);
        }

        public List<NetworkAdapter> List() {
            return _repositories.Items.SimpleComputerComponents.NetworkAdapters.List();
        }

        public void Update(NetworkAdapter item) {
            _repositories.Items.SimpleComputerComponents.NetworkAdapters.Update(item);
        }

        public void Delete(int id) {
            _repositories.Items.SimpleComputerComponents.NetworkAdapters.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }
    }
}