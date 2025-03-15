using GenosStorExpress.Application.Service.Implementation.Base;
using GenosStorExpress.Application.Service.Interface.Entity.Items.SimpleComputerComponents;
using GenosStorExpress.Application.Wrappers.Entity.Item.SimpleComputerComponent;
using GenosStorExpress.Domain.Entity.Item.SimpleComputerComponent;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.SimpleComputerComponent;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.SimpleComputerComponents {
    public class SSDControllerService: AbstractSimpleComputerComponentService, ISSDControllerService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly ISSDControllerRepository _ssdControllers;

        public SSDControllerService(ISimpleComputerComponentTypeService simpleComputerComponentTypeService, IGenosStorExpressRepositories repositories) : base(simpleComputerComponentTypeService) {
            _repositories = repositories;
            _ssdControllers = _repositories.Items.SimpleComputerComponents.SSDControllers;
        }
        
        public void Create(SSDControllerWrapper item) {
            var created = new SSDController();
            _setEntityPropertiesFromWrapper(created, item);
            _ssdControllers.Create(created);
        }

        public SSDControllerWrapper? Get(int id) {
            SSDController? obj = _ssdControllers.Get(id);
            if (obj == null) {
                return null;
            }
            var wrapped = new SSDControllerWrapper();
            _setWrapperPropertiesFromEntity(obj, wrapped);
            return wrapped;
        }

        public List<SSDControllerWrapper> List() {
            return _ssdControllers.List().Select(obj => {
                var wrapped = new SSDControllerWrapper();
                _setWrapperPropertiesFromEntity(obj, wrapped);
                return wrapped;
            }).ToList();
        }

        public void Update(int id, SSDControllerWrapper item) {
            var obj = _ssdControllers.Get(id);
            if (obj == null) {
                throw new NullReferenceException($"Контроллера твердотельного накопителя с номером {id} не существует");
            }
            _setEntityPropertiesFromWrapper(obj, item);
            _ssdControllers.Create(obj);
        }

        public void Delete(int id) {
            _ssdControllers.Delete(id);
        }

        public SSDController? GetRaw(int id) {
            return _ssdControllers.Get(id);
        }

        public int Save() {
            return _repositories.Save();
        }
    }
}