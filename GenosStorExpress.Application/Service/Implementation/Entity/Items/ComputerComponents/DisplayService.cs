using GenosStorExpress.Application.Service.Implementation.Base;
using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.ComputerComponents {
    public class DisplayService: AbstractComputerComponentService, IDisplayService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly IDisplayRepository _displays;
        
        private readonly IDefinitionService _definitionService;
        private readonly IMatrixTypeService _matrixTypeService;
        private readonly IUnderlightService _underlightService;
        private readonly IVesaSizeService _vesaSizeService;

        public DisplayService(IItemTypeService itemTypeService, IVendorService vendorService, IGenosStorExpressRepositories repositories, IDefinitionService definitionService, IMatrixTypeService matrixTypeService, IUnderlightService underlightService, IVesaSizeService vesaSizeService) : base(itemTypeService, vendorService) {
            _repositories = repositories;
            _definitionService = definitionService;
            _matrixTypeService = matrixTypeService;
            _underlightService = underlightService;
            _vesaSizeService = vesaSizeService;
            _displays = _repositories.Items.ComputerComponents.Displays;
        }
        
        public void Create(DisplayWrapper item) {
            var created = new Display();
            _setEntityPropertiesFromWrapper(created, item);
            
            created.MaxUpdateFrequency = item.MaxUpdateFrequency;
            created.ScreenDiagonal = item.ScreenDiagonal;
            created.Definition = _definitionService.GetRaw(item.Definition.Id);
            created.MatrixType = _matrixTypeService.GetEntityFromString(item.MatrixType);
            created.Underlight = _underlightService.GetEntityFromString(item.Underlight);
            created.VesaSize = _vesaSizeService.GetEntityFromString(item.VesaSize);
            
            _displays.Create(created);
        }

        public DisplayWrapper Get(int id) {
            Display obj = _displays.Get(id);
            var wrapped = new DisplayWrapper();
            
            _setWrapperPropertiesFromEntity(obj, wrapped);
            wrapped.MaxUpdateFrequency = obj.MaxUpdateFrequency;
            wrapped.ScreenDiagonal = obj.ScreenDiagonal;
            wrapped.Definition = _definitionService.Get(obj.Definition.Id);
            wrapped.MatrixType = obj.MatrixType.Name;
            wrapped.Underlight = obj.Underlight.Name;
            wrapped.VesaSize = obj.VesaSize.Name;
            
            return wrapped;
        }

        public List<DisplayWrapper> List() {
            return _displays.List().Select(obj => {
                var wrapped = new DisplayWrapper();
            
                _setWrapperPropertiesFromEntity(obj, wrapped);
                wrapped.MaxUpdateFrequency = obj.MaxUpdateFrequency;
                wrapped.ScreenDiagonal = obj.ScreenDiagonal;
                wrapped.Definition = _definitionService.Get(obj.Definition.Id);
                wrapped.MatrixType = obj.MatrixType.Name;
                wrapped.Underlight = obj.Underlight.Name;
                wrapped.VesaSize = obj.VesaSize.Name;
            
                return wrapped;
            }).ToList();
        }

        public void Update(int id, DisplayWrapper item) {
            var obj = _displays.Get(id);
            _setEntityPropertiesFromWrapper(obj, item);
            
            obj.MaxUpdateFrequency = item.MaxUpdateFrequency;
            obj.ScreenDiagonal = item.ScreenDiagonal;
            obj.Definition = _definitionService.GetRaw(item.Definition.Id);
            obj.MatrixType = _matrixTypeService.GetEntityFromString(item.MatrixType);
            obj.Underlight = _underlightService.GetEntityFromString(item.Underlight);
            obj.VesaSize = _vesaSizeService.GetEntityFromString(item.VesaSize);
            
            _displays.Update(obj);
        }

        public void Delete(int id) {
            _repositories.Items.ComputerComponents.Displays.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }
        
        public List<DisplayWrapper> Filter(List<Func<DisplayWrapper, bool>> filters) {
            var result = List();
            foreach (var filter in filters) {
                result = result.Where(filter).ToList();
            }
            return result;
        }
    }
}