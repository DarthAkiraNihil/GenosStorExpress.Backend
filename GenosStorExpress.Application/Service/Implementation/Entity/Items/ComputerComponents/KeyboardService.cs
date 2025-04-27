using GenosStorExpress.Application.Service.Implementation.Base;
using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Application.Service.Interface.Entity.Orders;
using GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;
using GenosStorExpress.Application.Wrappers.Filters;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.ComputerComponents {
    public class KeyboardService: AbstractComputerComponentService, IKeyboardService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly IKeyboardRepository _keyboards;
        private readonly IKeyboardTypesizeService _keyboardTypesizes;
        private readonly IKeyboardTypeService _keyboardTypes;

        public KeyboardService(IItemTypeService itemTypeService, IActiveDiscountService activeDiscountService, IVendorService vendorService, IGenosStorExpressRepositories repositories, IKeyboardTypesizeService keyboardTypesizes, IKeyboardTypeService keyboardTypes) : base(itemTypeService, activeDiscountService, vendorService) {
            _repositories = repositories;
            _keyboardTypesizes = keyboardTypesizes;
            _keyboardTypes = keyboardTypes;
            _keyboards = _repositories.Items.ComputerComponents.Keyboards;
        }

        public void Create(KeyboardWrapper item) {
            
            var created = new Keyboard();
            _setEntityPropertiesFromWrapper(created, item);
            
            created.HasRGBLighting = item.HasRGBLighting;
            created.IsWireless = item.IsWireless;
            
            var keyboardType = _keyboardTypes.GetEntityFromString(item.Type);
            if (keyboardType == null) {
                throw new NullReferenceException($"Типа клавиатуры {item.Type} не существует");
            }
            created.KeyboardType = keyboardType;
            
            var typesize =  _keyboardTypesizes.GetEntityFromString(item.Typesize);
            if (typesize == null) {
                throw new NullReferenceException($"Типоразмера клавиатуры {item.Typesize} не существует");
            }
            created.Typesize = typesize;
            
            _keyboards.Create(created);
            item.Id = created.Id;
        }

        public KeyboardWrapper? Get(int id) {
            Keyboard? obj = _keyboards.Get(id);
            if (obj == null) {
                return null;
            }
            var wrapped = new KeyboardWrapper();
            
            _setWrapperPropertiesFromEntity(obj, wrapped);
            
            wrapped.HasRGBLighting = obj.HasRGBLighting;
            wrapped.IsWireless = obj.IsWireless;
            wrapped.Type = obj.KeyboardType.Name;
            wrapped.Typesize = obj.Typesize.Name;
            
            return wrapped;
        }

        public List<KeyboardWrapper> List() {
            return _keyboards.List().Select(obj => {
                var wrapped = new KeyboardWrapper();
            
                _setWrapperPropertiesFromEntity(obj, wrapped);
            
                wrapped.HasRGBLighting = obj.HasRGBLighting;
                wrapped.IsWireless = obj.IsWireless;
                wrapped.Type = obj.KeyboardType.Name;
                wrapped.Typesize = obj.Typesize.Name;
            
                return wrapped;
            }).ToList();
        }

        public void Update(int id, KeyboardWrapper item) {
            var obj = _keyboards.Get(id);
            if (obj == null) {
                throw new NullReferenceException($"Клавиатуры с номером {id} не существует");
            }
            _setEntityPropertiesFromWrapper(obj, item);
            
            obj.HasRGBLighting = item.HasRGBLighting;
            obj.IsWireless = item.IsWireless;
            
            var keyboardType = _keyboardTypes.GetEntityFromString(item.Type);
            if (keyboardType == null) {
                throw new NullReferenceException($"Типа клавиатуры {item.Type} не существует");
            }
            obj.KeyboardType = keyboardType;
            
            var typesize =  _keyboardTypesizes.GetEntityFromString(item.Typesize);
            if (typesize == null) {
                throw new NullReferenceException($"Типоразмера клавиатуры {item.Typesize} не существует");
            }
            obj.Typesize = typesize;
            
            _keyboards.Update(obj);
        }

        public void Delete(int id) {
            _keyboards.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }
        

        public IList<KeyboardWrapper> Filter(FilterContainerWrapper filters) {
            
            var filters_ = new List<Func<KeyboardWrapper, bool>>();

            IDictionary<string, RangeFilterWrapper> ranges = filters.Ranges;
            IDictionary<string, ChoiceFilterWrapper> choices = filters.Choices;
            IDictionary<string, bool> havings = filters.Havings;

            if (choices.ContainsKey("typesize")) {
                filters_.Add(
                    i => choices["typesize"].CreateFilterClosure(n => n.Contains(i.Typesize))
                );
            }

            if (choices.ContainsKey("type")) {
                filters_.Add(
                    i => choices["type"].CreateFilterClosure(n => n.Contains(i.Type))
                );
            }

            if (havings.ContainsKey("has_rgb_lighting")) {
                filters_.Add(
                    i => i.HasRGBLighting == havings["has_rgb_lighting"]
                );
            }

            if (havings.ContainsKey("is_wireless")) {
                filters_.Add(
                    i => i.IsWireless == havings["is_wireless"]
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
                    Name = "typesize",
                    VerboseName = "typesize",
                    Choices = _repositories.Items.Characteristics.KeyboardTypesizes.List().Select(i => i.Name).ToList()
                },
                new FilterDescription {
                    Type = FilterType.Choice,
                    Name = "type",
                    VerboseName = "types",
                    Choices = _repositories.Items.Characteristics.KeyboardTypes.List().Select(i => i.Name).ToList()
                },
                new FilterDescription {
                    Type = FilterType.Having,
                    Name = "has_rgb_lighting",
                    VerboseName = "has_rgb_lighting",
                },
            };
        }
    }
}