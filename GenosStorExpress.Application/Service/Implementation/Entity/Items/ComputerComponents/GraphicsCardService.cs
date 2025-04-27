using GenosStorExpress.Application.Service.Implementation.Base;
using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Application.Service.Interface.Entity.Items.SimpleComputerComponents;
using GenosStorExpress.Application.Service.Interface.Entity.Orders;
using GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;
using GenosStorExpress.Application.Wrappers.Filters;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.ComputerComponents {
    public class GraphicsCardService: AbstractComputerComponentService, IGraphicsCardService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly IVideoPortService _videoPortService;
        private readonly IGPUService _gpuService;
        private readonly IGraphicsCardRepository _graphicsCards;

        public GraphicsCardService(IItemTypeService itemTypeService, IActiveDiscountService activeDiscountService, IVendorService vendorService, IGenosStorExpressRepositories repositories, IVideoPortService videoPortService, IGPUService gpuService) : base(itemTypeService, activeDiscountService, vendorService) {
            _repositories = repositories;
            _videoPortService = videoPortService;
            _gpuService = gpuService;
            _graphicsCards = _repositories.Items.ComputerComponents.GraphicsCards;
        }
        
        public void Create(GraphicsCardWrapper item) {
            var created = new GraphicsCard();
            _setEntityPropertiesFromWrapper(created, item);
            
            created.VideoRAM = item.VideoRAM;
            created.VideoPorts = item.VideoPorts.Select(i => {
                var port = _videoPortService.GetEntityFromString(i);
                if (port == null) {
                    throw new NullReferenceException($"Видеопорта {i} не существует");
                }

                return port;
            }).ToList();
            created.MaxDisplaysSupported = item.MaxDisplaysSupported;
            created.UsedSlots = item.UsedSlots;
            
            var gpu = _gpuService.GetRaw(item.GPU.Id);
            if (gpu == null) {
                throw new NullReferenceException($"Графического процессора с номером {item.GPU.Id} ({item.GPU.Name}) не существует");
            }

            created.GPU = gpu;
            
            _graphicsCards.Create(created);
            item.Id = created.Id;
        }

        public GraphicsCardWrapper? Get(int id) {
            GraphicsCard? obj = _graphicsCards.Get(id);
            if (obj == null) {
                return null;
            }
            var wrapped = new GraphicsCardWrapper();
            
            _setWrapperPropertiesFromEntity(obj, wrapped);
            
            wrapped.VideoRAM = obj.VideoRAM;
            wrapped.VideoPorts = obj.VideoPorts.Select(i => i.Name).ToList();
            wrapped.MaxDisplaysSupported = obj.MaxDisplaysSupported;
            wrapped.UsedSlots = obj.UsedSlots;
            wrapped.GPU = _gpuService.Get(obj.GPU.Id)!;
            
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
                wrapped.GPU = _gpuService.Get(obj.GPU.Id)!;
            
                return wrapped;
            }).ToList();
        }

        public void Update(int id, GraphicsCardWrapper item) {
            var obj = _graphicsCards.Get(id);
            if (obj == null) {
                throw new NullReferenceException($"Видеокарты с номером {id} не существует");
            }
            _setEntityPropertiesFromWrapper(obj, item);
            
            obj.VideoRAM = item.VideoRAM;
            obj.VideoPorts = item.VideoPorts.Select(i => {
                var port = _videoPortService.GetEntityFromString(i);
                if (port == null) {
                    throw new NullReferenceException($"Видеопорта {i} не существует");
                }

                return port;
            }).ToList();
            obj.MaxDisplaysSupported = item.MaxDisplaysSupported;
            obj.UsedSlots = item.UsedSlots;
            
            var gpu = _gpuService.GetRaw(item.GPU.Id);
            if (gpu == null) {
                throw new NullReferenceException($"Графического процессора с номером {item.GPU.Id} ({item.GPU.Name}) не существует");
            }

            obj.GPU = gpu;
            
            _graphicsCards.Update(obj);
        }

        public void Delete(int id) {
            _graphicsCards.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        public IList<GraphicsCardWrapper> Filter(FilterContainerWrapper filters) {
            var filters_ = new List<Func<GraphicsCardWrapper, bool>>();

            IDictionary<string, RangeFilterWrapper> ranges = filters.Ranges;
            IDictionary<string, ChoiceFilterWrapper> choices = filters.Choices;
            IDictionary<string, bool> havings = filters.Havings;

            if (ranges.ContainsKey("video_ram")) {
                if (ranges["video_ram"].IsValid()) {
                    filters_.Add(
                        i => ranges["video_ram"].From <= i.VideoRAM && i.VideoRAM <= ranges["video_ram"].To
                    );
                }
            }

            if (choices.ContainsKey("video_ports")) {
                filters_.Add(
                    i => choices["video_ports"].CreateFilterClosure(n => {
                        foreach (var entry in i.VideoPorts) {
                            if (entry == n) {
                                return true;
                            }
                        }

                        return false;
                    })
                );
            }

            if (choices.ContainsKey("gpu")) {
                filters_.Add(
                    i => choices["gpu"].CreateFilterClosure(n => n.Contains(i.GPU.Name))
                );
            }

            if (ranges.ContainsKey("max_displays_supported")) {
                if (ranges["max_displays_supported"].IsValid()) {
                    filters_.Add(
                        i => ranges["max_displays_supported"].From <= i.MaxDisplaysSupported && i.MaxDisplaysSupported <= ranges["max_displays_supported"].To
                    );
                }
            }

            if (ranges.ContainsKey("price")) {
                if (ranges["price"].IsValid()) {
                    filters_.Add(
                        i => ranges["price"].From <= i.Price && i.Price <= ranges["price"].To
                    );
                }
            }

            if (ranges.ContainsKey("tdp")) {
                if (ranges["tdp"].IsValid()) {
                    filters_.Add(
                        i => ranges["tdp"].From <= i.TDP && i.TDP <= ranges["tdp"].To
                    );
                }
            }

            if (choices.ContainsKey("vendors")) {
                filters_.Add(
                    i => choices["vendors"].CreateFilterClosure(n => n.Contains(i.Vendor))
                );
            }
            
            if (filters.Name.Length != 0) {
                filters_.Add(
                    i => i.Name.Contains(filters.Name)
                );
            }
            
            var result = List();
            foreach (var filter in filters_) {
                result = result.Where(filter).ToList();
            }
            return result;
            
        }
        
        /// <summary>
        /// Получение данных о возможных фильтрах товара
        /// </summary>
        /// <returns>Список возможных фильтров</returns>
        public IList<FilterDescription> FilterData() {
            return new List<FilterDescription> {
                new FilterDescription {
                    Type = FilterType.Range,
                    Name = "price",
                    VerboseName = "Цена"
                },
                new FilterDescription {
                    Type = FilterType.Choice,
                    Name = "vendors",
                    VerboseName = "Производители",
                    Choices = _repositories.Items.Characteristics.Vendors.List().Select(i => i.Name).ToList()
                },
                new FilterDescription {
                    Type = FilterType.Range,
                    Name = "tdp",
                    VerboseName = "Потребляемая мощность"
                },
                new FilterDescription {
                    Type = FilterType.Choice,
                    Name = "gpu",
                    VerboseName = "Модель ГП",
                    Choices = _repositories.Items.SimpleComputerComponents.GPUs.List().Select(i => i.Name).ToList()
                },
                new FilterDescription {
                    Type = FilterType.Choice,
                    Name = "video_port",
                    VerboseName = "Видеопорты",
                    Choices = _repositories.Items.Characteristics.VideoPorts.List().Select(i => i.Name).ToList()
                },
                new FilterDescription {
                    Type = FilterType.Range,
                    Name = "video_ram",
                    VerboseName = "Размер видеопамяти"
                },
                new FilterDescription {
                    Type = FilterType.Range,
                    Name = "max_displays_supported",
                    VerboseName = "Максимальное количество подд. дисплеев"
                },
            };
        }
    }
}