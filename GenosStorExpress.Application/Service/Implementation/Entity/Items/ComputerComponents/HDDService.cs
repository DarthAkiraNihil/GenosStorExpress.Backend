using GenosStorExpress.Application.Service.Implementation.Base;
using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.ComputerComponents {
    public class HDDService: AbstractDiskDriveService, IHDDService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly IHDDRepository _hdds;

        public HDDService(IItemTypeService itemTypeService, IVendorService vendorService, IGenosStorExpressRepositories repositories) : base(itemTypeService, vendorService) {
            _repositories = repositories;
            _hdds = _repositories.Items.ComputerComponents.HDDs;
        }

        public void Create(HDDWrapper item) {
            var created = new HDD();
            
            _setEntityPropertiesFromWrapper(created, item);
            created.RPM = item.RPM;
            
            _hdds.Create(created);
        }

        public HDDWrapper Get(int id) {
            HDD obj = _hdds.Get(id);
            var wrapped = new HDDWrapper();
            
            _setWrapperPropertiesFromEntity(obj, wrapped);
            wrapped.RPM = obj.RPM;
            
            return wrapped;
        }

        public List<HDDWrapper> List() {
            return _hdds.List().Select(obj => {
                var wrapped = new HDDWrapper();
            
                _setWrapperPropertiesFromEntity(obj, wrapped);
                wrapped.RPM = obj.RPM;
            
                return wrapped;
            }).ToList();
        }

        public void Update(int id, HDDWrapper item) {
            HDD obj = _hdds.Get(id);
            _setEntityPropertiesFromWrapper(obj, item);
            obj.RPM = item.RPM;
            
            _hdds.Update(obj);
        }

        public void Delete(int id) {
            _hdds.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }
        
        public List<HDDWrapper> Filter(List<Func<HDDWrapper, bool>> filters) {
            var result = List();
            foreach (var filter in filters) {
                result = result.Where(filter).ToList();
            }
            return result;
        }
    }
}