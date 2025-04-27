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
    
    /// <summary>
    /// Реализация сервиса кулеров для процессора
    /// </summary>
    public class CPUCoolerService: AbstractComputerComponentService, ICPUCoolerService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly ICPUCoolerRepository _cpuCoolers;
        private readonly ICoolerMaterialService _coolerMaterials;
        
        /// <summary>
        /// Стандартный конструктор
        /// </summary>
        /// <param name="itemTypeService">Сервис типов предметов</param>
        /// <param name="activeDiscountService">Сервис скидок</param>
        /// <param name="vendorService">Сервис производителей</param>
        /// <param name="repositories">Репозитории проекта</param>
        /// <param name="coolerMaterials">Сервис материалов кулеров</param>

        public CPUCoolerService(IItemTypeService itemTypeService, IActiveDiscountService activeDiscountService, IVendorService vendorService, IGenosStorExpressRepositories repositories, ICoolerMaterialService coolerMaterials) : base(itemTypeService, activeDiscountService, vendorService) {
            _repositories = repositories;
            _coolerMaterials = coolerMaterials;
            _cpuCoolers = _repositories.Items.ComputerComponents.CPUCoolers;
        }
        
        /// <summary>
        /// Создание сущности кулера для процессора из обёртки
        /// </summary>
        /// <param name="item">Обёрнутый кулер для процессора</param>
        /// <exception cref="NullReferenceException">Если указанного производителя или материала кулера не существует</exception>
        public void Create(CPUCoolerWrapper item) {
            
            var created = new CPUCooler();
            _setEntityPropertiesFromWrapper(created, item);
            
            created.MaxFanRPM = item.MaxFanRPM;
            created.TubesCount = item.TubesCount;
            created.TubesDiameter = item.TubesDiameter;
            created.FanCount = item.FanCount;
            
            var foundationMaterial = _coolerMaterials.GetEntityFromString(item.FoundationMaterial);
            if (foundationMaterial == null) {
                throw new NullReferenceException($"Материала основания {item.FoundationMaterial} не существует");
            }
            created.FoundationMaterial = foundationMaterial;
            
            var radiatorMaterial = _coolerMaterials.GetEntityFromString(item.RadiatorMaterial);
            if (radiatorMaterial == null) {
                throw new NullReferenceException($"Материала радиатора {item.RadiatorMaterial} не существует");
            }
            created.RadiatorMaterial = radiatorMaterial;
            
            _cpuCoolers.Create(created);
            item.Id = created.Id;
        }
        
        /// <summary>
        /// Получение объекта-обёртки сущности кулера для процессора по его номеру
        /// </summary>
        /// <param name="id">Номер кулера</param>
        /// <returns>Обёрнутый кулер или null в случае его отсутствия</returns>
        public CPUCoolerWrapper? Get(int id) {
            
            CPUCooler? obj = _cpuCoolers.Get(id);
            
            if (obj == null) {
                return null;
            }
            
            var wrapped = new CPUCoolerWrapper();
            _setWrapperPropertiesFromEntity(obj, wrapped);
            
            wrapped.MaxFanRPM = obj.MaxFanRPM;
            wrapped.TubesCount = obj.TubesCount;
            wrapped.TubesDiameter = obj.TubesDiameter;
            wrapped.FanCount = obj.FanCount;
            wrapped.RadiatorMaterial = obj.RadiatorMaterial.Name;
            wrapped.FoundationMaterial = obj.FoundationMaterial.Name;
            
            return wrapped;
        }

        /// <summary>
        /// Получение списка всех кулеров для процессора в виде обёрток
        /// </summary>
        /// <returns>Список обёрнутых кулеров для процессора</returns>
        public List<CPUCoolerWrapper> List() {
            return _cpuCoolers.List().Select(obj => {
                var wrapped = new CPUCoolerWrapper();
                _setWrapperPropertiesFromEntity(obj, wrapped);
            
                wrapped.MaxFanRPM = obj.MaxFanRPM;
                wrapped.TubesCount = obj.TubesCount;
                wrapped.TubesDiameter = obj.TubesDiameter;
                wrapped.FanCount = obj.FanCount;
                wrapped.RadiatorMaterial = obj.RadiatorMaterial.Name;
                wrapped.FoundationMaterial = obj.FoundationMaterial.Name;
            
                return wrapped;
            }).ToList();
        }

        /// <summary>
        /// Обновление сущности кулера для процессора
        /// </summary>
        /// <param name="id">Номер кулера</param>
        /// <param name="item">Изменённые данные</param>
        /// <exception cref="NullReferenceException">Если указанного производителя или материала кулера не существует</exception>
        public void Update(int id, CPUCoolerWrapper item) {
            var obj = _cpuCoolers.Get(id);
            
            if (obj == null) {
                throw new NullReferenceException($"Кулера для процессора с номером {id} не существует");
            }
            _setEntityPropertiesFromWrapper(obj, item);
            
            obj.MaxFanRPM = item.MaxFanRPM;
            obj.TubesCount = item.TubesCount;
            obj.TubesDiameter = item.TubesDiameter;
            obj.FanCount = item.FanCount;
            var foundationMaterial = _coolerMaterials.GetEntityFromString(item.FoundationMaterial);
            if (foundationMaterial == null) {
                throw new NullReferenceException($"Материала основания {item.FoundationMaterial} не существует");
            }
            obj.FoundationMaterial = foundationMaterial;
            
            var radiatorMaterial = _coolerMaterials.GetEntityFromString(item.RadiatorMaterial);
            if (radiatorMaterial == null) {
                throw new NullReferenceException($"Материала радиатора {item.RadiatorMaterial} не существует");
            }
            obj.RadiatorMaterial = radiatorMaterial;
            
            _cpuCoolers.Update(obj);
        }

        /// <summary>
        /// Удаление сущности кулера для процессора
        /// </summary>
        /// <param name="id">Номер кулера для процессора</param>
        public void Delete(int id) {
            _cpuCoolers.Delete(id);
        }

        /// <summary>
        /// Сохранение базы данных
        /// </summary>
        /// <returns>Количество изменений</returns>
        public int Save() {
            return _repositories.Save();
        }
        
        /// <summary>
        /// Фильтрация списка сущностей кулеров для процессора
        /// </summary>
        /// <param name="filters">Список фильтров</param>
        /// <returns>Отфильтрованный список обёрток кулеров для процессора</returns>
        public IList<CPUCoolerWrapper> Filter(FilterContainerWrapper filters) {
            var filters_ = new List<Func<CPUCoolerWrapper, bool>>();

            IDictionary<string, RangeFilterWrapper> ranges = filters.Ranges;
            IDictionary<string, ChoiceFilterWrapper> choices = filters.Choices;

            if (choices.ContainsKey("foundation_material")) {
                filters_.Add(
                    i => choices["foundation_material"].CreateFilterClosure(n => n.Contains(i.FoundationMaterial))
                );
            }

            if (choices.ContainsKey("radiator_material")) {
                filters_.Add(
                    i => choices["radiator_material"].CreateFilterClosure(n => n.Contains(i.RadiatorMaterial))
                );
            }

            if (ranges.ContainsKey("max_fan_rpm")) {
                if (ranges["max_fan_rpm"].IsValid()) {
                    filters_.Add(
                        i => ranges["max_fan_rpm"].From <= i.MaxFanRPM && i.MaxFanRPM <= ranges["max_fan_rpm"].To
                    );
                }
            }

            if (ranges.ContainsKey("tubes_count")) {
                if (ranges["tubes_count"].IsValid()) {
                    filters_.Add(
                        i => ranges["tubes_count"].From <= i.TubesCount && i.TubesCount <= ranges["tubes_count"].To
                    );
                }
            }

            if (ranges.ContainsKey("tubes_diameter")) {
                if (ranges["tubes_diameter"].IsValid()) {
                    filters_.Add(
                        i => ranges["tubes_diameter"].From <= i.TubesDiameter && i.TubesDiameter <= ranges["tubes_diameter"].To
                    );
                }
            }

            if (ranges.ContainsKey("fan_count")) {
                if (ranges["fan_count"].IsValid()) {
                    filters_.Add(
                        i => ranges["fan_count"].From <= i.FanCount && i.FanCount <= ranges["fan_count"].To
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
                    VerboseName = "tdp"
                },
                new FilterDescription {
                    Type = FilterType.Range,
                    Name = "max_fan_rpm",
                    VerboseName = "max_fan_rpm"
                },
                new FilterDescription {
                    Type = FilterType.Range,
                    Name = "tubes_count",
                    VerboseName = "tubes_count"
                },
                new FilterDescription {
                    Type = FilterType.Range,
                    Name = "tubes_diameter",
                    VerboseName = "tubes_diameter"
                },
                new FilterDescription {
                    Type = FilterType.Range,
                    Name = "fan_count",
                    VerboseName = "fan_count"
                },
                new FilterDescription {
                    Type = FilterType.Choice,
                    Name = "foundation_material",
                    VerboseName = "foundation_material",
                    Choices = _repositories.Items.Characteristics.CoolerMaterials.List().Select(i => i.Name).ToList()
                },
                new FilterDescription {
                    Type = FilterType.Choice,
                    Name = "radiator_material",
                    VerboseName = "radiator_material",
                    Choices = _repositories.Items.Characteristics.CoolerMaterials.List().Select(i => i.Name).ToList()
                },
            };
        }
    }
}