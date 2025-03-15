using GenosStorExpress.Application.Service.Implementation.Base;
using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Application.Service.Interface.Entity.Items.SimpleComputerComponents;
using GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.ComputerComponents {
    public class SataSSDService: AbstractSSDService, ISataSSDService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly ISataSSDRepository _sataSSDs;

        public SataSSDService(IItemTypeService itemTypeService, IVendorService vendorService, ISSDControllerService ssdControllerService, IGenosStorExpressRepositories repositories) : base(itemTypeService, vendorService, ssdControllerService) {
            _repositories = repositories;
            _sataSSDs = _repositories.Items.ComputerComponents.SataSSDs;
        }

        public void Create(SataSSDWrapper item) {
            var created = new SataSSD();
            _setEntityPropertiesFromWrapper(created, item);
            _sataSSDs.Create(created);
        }

        public SataSSDWrapper? Get(int id) {
            SataSSD? obj = _sataSSDs.Get(id);
            if (obj == null) {
                return null;
            }
            var wrapped = new SataSSDWrapper();
            _setWrapperPropertiesFromEntity(obj, wrapped);
            return wrapped;
        }

        public List<SataSSDWrapper> List() {
            return _sataSSDs.List().Select(obj => {
                var wrapped = new SataSSDWrapper();
                _setWrapperPropertiesFromEntity(obj, wrapped);
                return wrapped;
            }).ToList();
        }

        public void Update(int id, SataSSDWrapper item) {
            var obj = _sataSSDs.Get(id);
            if (obj == null) {
                throw new NullReferenceException($"Твердотельного накопителя Sata с номером {id} не существует");
            }
            _setEntityPropertiesFromWrapper(obj, item);
            _sataSSDs.Update(obj);
        }

        public void Delete(int id) {
            _sataSSDs.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }
        
        public List<SataSSDWrapper> Filter(List<Func<SataSSDWrapper, bool>> filters) {
            var result = List();
            foreach (var filter in filters) {
                result = result.Where(filter).ToList();
            }
            return result;
        }
        
    }
}