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
    public class GraphicsCardService: AbstractComputerComponentService, IGraphicsCardService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly IVideoPortService _videoPortService;
        private readonly IGPUService _gpuService;
        private readonly IGraphicsCardRepository _graphicsCards;

        public GraphicsCardService(IItemTypeService itemTypeService, IVendorService vendorService, IGenosStorExpressRepositories repositories, IVideoPortService videoPortService, IGPUService gpuService) : base(itemTypeService, vendorService) {
            _repositories = repositories;
            _videoPortService = videoPortService;
            _gpuService = gpuService;
            _graphicsCards = _repositories.Items.ComputerComponents.GraphicsCards;
        }
        
        public void Create(GraphicsCardWrapper item) {
            var created = new GraphicsCard();
            _setEntityPropertiesFromWrapper(created, item);
            
            created.VideoRAM = item.VideoRAM;
            created.VideoPorts = item.VideoPorts.Select(i => _videoPortService.GetEntityFromString(i)).ToList();
            created.MaxDisplaysSupported = item.MaxDisplaysSupported;
            created.UsedSlots = item.UsedSlots;
            created.GPU = _gpuService.GetRaw(item.GPU.Id);
            
            _graphicsCards.Create(created);
        }

        public GraphicsCardWrapper Get(int id) {
            GraphicsCard obj = _graphicsCards.Get(id);
            var wrapped = new GraphicsCardWrapper();
            
            _setWrapperPropertiesFromEntity(obj, wrapped);
            
            wrapped.VideoRAM = obj.VideoRAM;
            wrapped.VideoPorts = obj.VideoPorts.Select(i => i.Name).ToList();
            wrapped.MaxDisplaysSupported = obj.MaxDisplaysSupported;
            wrapped.UsedSlots = obj.UsedSlots;
            wrapped.GPU = _gpuService.Get(obj.GPU.Id);
            
            return wrapped;
        }

        public List<GraphicsCardWrapper> List() {
            return _graphicsCards.List().Select(obj => {
                var wrapped = new GraphicsCardWrapper();
            
                _setWrapperPropertiesFromEntity(obj, wrapped);
            
                wrapped.VideoRAM = obj.VideoRAM;
                wrapped.VideoPorts = obj.VideoPorts.Select(i => i.Name).ToList();
                wrapped.MaxDisplaysSupported = obj.MaxDisplaysSupported;
                wrapped.UsedSlots = obj.UsedSlots;
                wrapped.GPU = _gpuService.Get(obj.GPU.Id);
            
                return wrapped;
            }).ToList();
        }

        public void Update(int id, GraphicsCardWrapper item) {
            var obj = _graphicsCards.Get(id);
            _setEntityPropertiesFromWrapper(obj, item);
            
            obj.VideoRAM = item.VideoRAM;
            obj.VideoPorts = item.VideoPorts.Select(i => _videoPortService.GetEntityFromString(i)).ToList();
            obj.MaxDisplaysSupported = item.MaxDisplaysSupported;
            obj.UsedSlots = item.UsedSlots;
            obj.GPU = _gpuService.GetRaw(item.GPU.Id);
            
            _graphicsCards.Update(obj);
        }

        public void Delete(int id) {
            _graphicsCards.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }
        
        public List<GraphicsCardWrapper> Filter(List<Func<GraphicsCardWrapper, bool>> filters) {
            var result = List();
            foreach (var filter in filters) {
                result = result.Where(filter).ToList();
            }
            return result;
        }
    }
}