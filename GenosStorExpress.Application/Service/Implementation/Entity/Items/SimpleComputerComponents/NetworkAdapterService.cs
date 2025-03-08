using GenosStorExpress.Application.Service.Implementation.Base;
using GenosStorExpress.Application.Service.Interface.Entity.Items.SimpleComputerComponents;
using GenosStorExpress.Application.Wrappers.Entity.Item.SimpleComputerComponent;
using GenosStorExpress.Domain.Entity.Item.SimpleComputerComponent;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.SimpleComputerComponent;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.SimpleComputerComponents {
    public class NetworkAdapterService: AbstractSimpleComputerComponentService, INetworkAdapterService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly INetworkAdapterRepository _networkAdapters;

        public NetworkAdapterService(ISimpleComputerComponentTypeService simpleComputerComponentTypeService, IGenosStorExpressRepositories repositories) : base(simpleComputerComponentTypeService) {
            _repositories = repositories;
            _networkAdapters = _repositories.Items.SimpleComputerComponents.NetworkAdapters;
        }
        
        public void Create(NetworkAdapterWrapper item) {
            var created = new NetworkAdapter();
            _setEntityPropertiesFromWrapper(created, item);
            _networkAdapters.Create(created);
        }

        public NetworkAdapterWrapper Get(int id) {
            NetworkAdapter obj = _networkAdapters.Get(id);
            var wrapped = new NetworkAdapterWrapper();
            _setWrapperPropertiesFromEntity(obj, wrapped);
            return wrapped;
        }

        public List<NetworkAdapterWrapper> List() {
            return _networkAdapters.List().Select(obj => {
                var wrapped = new NetworkAdapterWrapper();
                _setWrapperPropertiesFromEntity(obj, wrapped);
                return wrapped;
            }).ToList();
        }

        public void Update(int id, NetworkAdapterWrapper item) {
            var obj = _networkAdapters.Get(id);
            _setEntityPropertiesFromWrapper(obj, item);
            _networkAdapters.Create(obj);
        }

        public void Delete(int id) {
            _networkAdapters.Delete(id);
        }

        public NetworkAdapter GetRaw(int id) {
            return _networkAdapters.Get(id);
        }

        public int Save() {
            return _repositories.Save();
        }
    }
}