using GenosStorExpress.Application.Service.Implementation.Base;
using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;
using GenosStorExpress.Application.Wrappers.Filters;
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
            created.DPIModes = item.DPIModes.Select(i => {
                var mode = _dpiModeService.GetByValue(i);
                if (mode == null) {
                    throw new NullReferenceException($"Режима DPI {i} мыши не существует");
                }

                return mode;
            }).ToList();
            created.IsWireless = item.IsWireless;
            
            _mouses.Create(created);
            item.Id = created.Id;
        }

        public MouseWrapper? Get(int id) {
            Mouse? obj = _mouses.Get(id);
            if (obj == null) {
                return null;
            }
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
            if (obj == null) {
                throw new NullReferenceException($"Мыши с номером {id} не существует");
            }
            _setEntityPropertiesFromWrapper(obj, item);
            
            obj.ButtonsCount = item.ButtonsCount;
            obj.HasProgrammableButtons = item.HasProgrammableButtons;
            obj.DPIModes = item.DPIModes.Select(i => {
                var mode = _dpiModeService.GetByValue(i);
                if (mode == null) {
                    throw new NullReferenceException($"Режима DPI {i} мыши не существует");
                }

                return mode;
            }).ToList();
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

        public IList<MouseWrapper> Filter(FilterContainerWrapper filters) {
            
            var filters_ = new List<Func<MouseWrapper, bool>>();

            IDictionary<string, RangeFilterWrapper> ranges = filters.Ranges;
            IDictionary<string, ChoiceFilterWrapper> choices = filters.Choices;
            IDictionary<string, bool> havings = filters.Havings;

            if (choices.ContainsKey("dpi_modes")) {
                filters_.Add(
                    i => choices["dpi_modes"].CreateFilterClosure(n => {
                        foreach (var entry in i.DPIModes) {
                            if (entry.ToString() == n) {
                                return true;
                            }
                        }

                        return false;
                    })
                );
            }

            if (havings.ContainsKey("is_wireless")) {
                filters_.Add(
                    i => i.IsWireless == havings["is_wireless"]
                );
            }

            if (havings.ContainsKey("has_programmable_buttons")) {
                filters_.Add(
                    i => i.HasProgrammableButtons == havings["has_programmable_buttons"]
                );
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
                    VerboseName = "tdp"
                },
                new FilterDescription {
                    Type = FilterType.Choice,
                    Name = "dpi",
                    VerboseName = "dpi",
                    Choices = _repositories.Items.Characteristics.DPIModes.List().Select(i => i.DPI.ToString()).ToList()
                },
                new FilterDescription {
                    Type = FilterType.Having,
                    Name = "has_programmable_buttons",
                    VerboseName = "has_programmable_buttons",
                },
                new FilterDescription {
                    Type = FilterType.Having,
                    Name = "is_wireless",
                    VerboseName = "is_wireless",
                },
            };
        }
    }
}