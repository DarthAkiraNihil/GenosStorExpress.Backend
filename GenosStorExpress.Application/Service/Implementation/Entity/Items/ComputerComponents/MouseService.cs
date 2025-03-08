using GenosStorExpress.Application.Service.Implementation.Base;
using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.ComputerComponents {
    public class MouseService: AbstractComputerComponentService, IMouseService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly IMouseRepository _mouses;
        private readonly IDPIModeService _dpiModeService;

        public MouseService(IItemTypeService itemTypeService, IVendorService vendorService, IGenosStorExpressRepositories repositories, IDPIModeService dpiModeService) : base(itemTypeService, vendorService) {
            _repositories = repositories;
            _dpiModeService = dpiModeService;
            _mouses = _repositories.Items.ComputerComponents.Mouses;
        }

        public void Create(MouseWrapper item) {
            var created = new Mouse();
            _setEntityPropertiesFromWrapper(created, item);
            
            created.ButtonsCount = item.ButtonsCount;
            created.HasProgrammableButtons = item.HasProgrammableButtons;
            created.DPIModes = item.DPIModes.Select(i => _dpiModeService.GetByValue(i)).ToList();
            created.IsWireless = item.IsWireless;
            
            _mouses.Create(created);
        }

        public MouseWrapper Get(int id) {
            Mouse obj = _mouses.Get(id);
            var wrapped = new MouseWrapper();
            
            _setWrapperPropertiesFromEntity(obj, wrapped);
            wrapped.ButtonsCount = obj.ButtonsCount;
            wrapped.HasProgrammableButtons = obj.HasProgrammableButtons;
            wrapped.DPIModes = obj.DPIModes.Select(i => i.DPI).ToList();
            wrapped.IsWireless = obj.IsWireless;
            
            return wrapped;
        }

        public List<MouseWrapper> List() {
            return _mouses.List().Select(obj => {
                var wrapped = new MouseWrapper();
            
                _setWrapperPropertiesFromEntity(obj, wrapped);
                wrapped.ButtonsCount = obj.ButtonsCount;
                wrapped.HasProgrammableButtons = obj.HasProgrammableButtons;
                wrapped.DPIModes = obj.DPIModes.Select(i => i.DPI).ToList();
                wrapped.IsWireless = obj.IsWireless;
            
                return wrapped;
            }).ToList();
        }

        public void Update(int id, MouseWrapper item) {
            var obj = _mouses.Get(id);
            _setEntityPropertiesFromWrapper(obj, item);
            
            obj.ButtonsCount = item.ButtonsCount;
            obj.HasProgrammableButtons = item.HasProgrammableButtons;
            obj.DPIModes = item.DPIModes.Select(i => _dpiModeService.GetByValue(i)).ToList();
            obj.IsWireless = item.IsWireless;
            
            _mouses.Update(obj);
        }

        public void Delete(int id) {
            _repositories.Items.ComputerComponents.Mouses.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }
        
        public List<MouseWrapper> Filter(List<Func<MouseWrapper, bool>> filters) {
            var result = List();
            foreach (var filter in filters) {
                result = result.Where(filter).ToList();
            }
            return result;
        }
    }
}