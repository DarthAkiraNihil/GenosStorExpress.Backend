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
            
            var definition = _definitionService.GetRaw(item.Definition.Id);
            if (definition == null) {
                throw new NullReferenceException($"Разрешения экрана с номером {item.Definition.Id} ({item.Definition.Width}x{item.Definition.Height}) не существует");
            }
            created.Definition = definition;
            
            var matrixType = _matrixTypeService.GetEntityFromString(item.MatrixType);
            if (matrixType == null) {
                throw new NullReferenceException($"Типа матрицы экрана {item.MatrixType} не существует");
            }
            created.MatrixType = matrixType;
            
            var underlight = _underlightService.GetEntityFromString(item.Underlight);
            if (underlight == null) {
                throw new NullReferenceException($"Типа подсветки экрана {item.Underlight} не существует");
            }
            created.Underlight = underlight;
            
            var vesaSize = _vesaSizeService.GetEntityFromString(item.VesaSize);
            if (vesaSize == null) {
                throw new NullReferenceException($"Размера Vesa {item.VesaSize} не существует");
            }
            created.VesaSize = vesaSize;
            
            _displays.Create(created);
        }

        public DisplayWrapper? Get(int id) {
            Display? obj = _displays.Get(id);
            if (obj == null) {
                return null;
            }
            var wrapped = new DisplayWrapper();
            
            _setWrapperPropertiesFromEntity(obj, wrapped);
            wrapped.MaxUpdateFrequency = obj.MaxUpdateFrequency;
            wrapped.ScreenDiagonal = obj.ScreenDiagonal;
            wrapped.Definition = _definitionService.Get(obj.Definition.Id)!;
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
                wrapped.Definition = _definitionService.Get(obj.Definition.Id)!;
                wrapped.MatrixType = obj.MatrixType.Name;
                wrapped.Underlight = obj.Underlight.Name;
                wrapped.VesaSize = obj.VesaSize.Name;
            
                return wrapped;
            }).ToList();
        }

        public void Update(int id, DisplayWrapper item) {
            var obj = _displays.Get(id);
            if (obj == null) {
                throw new NullReferenceException($"Монитора с номером {id} не существует");
            }
            _setEntityPropertiesFromWrapper(obj, item);
            
            obj.MaxUpdateFrequency = item.MaxUpdateFrequency;
            obj.ScreenDiagonal = item.ScreenDiagonal;
            
            var definition = _definitionService.GetRaw(item.Definition.Id);
            if (definition == null) {
                throw new NullReferenceException($"Разрешения экрана с номером {item.Definition.Id} ({item.Definition.Width}x{item.Definition.Height}) не существует");
            }
            obj.Definition = definition;
            
            var matrixType = _matrixTypeService.GetEntityFromString(item.MatrixType);
            if (matrixType == null) {
                throw new NullReferenceException($"Типа матрицы экрана {item.MatrixType} не существует");
            }
            obj.MatrixType = matrixType;
            
            var underlight = _underlightService.GetEntityFromString(item.Underlight);
            if (underlight == null) {
                throw new NullReferenceException($"Типа подсветки экрана {item.Underlight} не существует");
            }
            obj.Underlight = underlight;
            
            var vesaSize = _vesaSizeService.GetEntityFromString(item.VesaSize);
            if (vesaSize == null) {
                throw new NullReferenceException($"Размера Vesa {item.VesaSize} не существует");
            }
            obj.VesaSize = vesaSize;
            
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