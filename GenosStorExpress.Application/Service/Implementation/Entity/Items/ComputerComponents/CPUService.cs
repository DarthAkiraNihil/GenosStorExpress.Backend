using GenosStorExpress.Application.Service.Implementation.Base;
using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Application.Service.Interface.Entity.Items.SimpleComputerComponents;
using GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;
using GenosStorExpress.Application.Wrappers.Entity.Item.SimpleComputerComponent;
using GenosStorExpress.Application.Wrappers.Filters;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.ComputerComponents {
    
    /// <summary>
    /// Реализация сервиса центральных процессоров
    /// </summary>
    public class CPUService: AbstractComputerComponentService, ICPUService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly ICPURepository _cpus;
        private readonly ICPUCoreService _cpuCoreService;
        private readonly ICPUSocketService _cpusSocketService;
        private readonly IRAMTypeService _ramTypeService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemTypeService">Сервис типов предметов</param>
        /// <param name="vendorService">Сервис производителей</param>
        /// <param name="repositories">Репозитории проекта</param>
        /// <param name="cpuCoreService">Сервис ядер процессоров</param>
        /// <param name="cpusSocketService">Сервис сокетов процессоров</param>
        /// <param name="ramTypeService">Сервис типов ОЗУ</param>
        public CPUService(IItemTypeService itemTypeService, IVendorService vendorService, IGenosStorExpressRepositories repositories, ICPUCoreService cpuCoreService, ICPUSocketService cpusSocketService, IRAMTypeService ramTypeService) : base(itemTypeService, vendorService) {
            _repositories = repositories;
            _cpuCoreService = cpuCoreService;
            _cpusSocketService = cpusSocketService;
            _ramTypeService = ramTypeService;
            _cpus = _repositories.Items.ComputerComponents.CPUs;
        }

        /// <summary>
        /// Создание сущности центрального процессоров из обёртки
        /// </summary>
        /// <param name="item">Обёрнутый кулер для процессора</param>
        /// <exception cref="NullReferenceException">Если указанного производителя, ядра процессора, сокета или типа ОЗУ не существует</exception>
        public void Create(CPUWrapper item) {

            var created = new CPU();
            _setEntityPropertiesFromWrapper(created, item);

            var core = _cpuCoreService.GetRaw(item.Core.Id);
            if (core == null) {
                throw new NullReferenceException($"Ядра процессора с номером {item.Core.Id} ({item.Core.Name} не существует)");
            }
            created.Core = core;
            
            var socket = _cpusSocketService.GetEntityFromString(item.Socket);
            if (socket == null) {
                throw new NullReferenceException($"Сокета {item.Socket} не существует)");
            }
            created.Socket = socket;
            
            created.CoresCount = item.CoresCount;
            created.ThreadsCount = item.ThreadsCount;
            created.L2CahceSize = item.L2CacheSize;
            created.L3CacheSize = item.L3CacheSize;
            created.TechnicalProcess = item.TechnicalProcess;
            created.BaseFrequency = item.BaseFrequency;
            created.SupportedRamType = item.SupportedRamTypes.Select(i => {
                var type = _ramTypeService.GetEntityFromString(i);
                if (type == null) {
                    throw new NullReferenceException($"Типа ОЗУ {i} не существует)");
                }

                return type;
            }).ToList();
            created.SupportedRAMSize = item.SupportedRAMSize;
            created.HasIntegratedGraphics = item.HasIntegratedGraphics;
            
            _cpus.Create(created);
            item.Id = created.Id;
        }

        /// <summary>
        /// Получение объекта-обёртки сущности центрального процессоров по его номеру
        /// </summary>
        /// <param name="id">Номер процессора</param>
        /// <returns>Обёрнутый процессор или null в случае его отсутствия</returns>
        public CPUWrapper? Get(int id) {
            CPU? obj = _cpus.Get(id);
            if (obj == null) {
                return null;
            }
            var wrapped = new CPUWrapper();
            
            _setWrapperPropertiesFromEntity(obj, wrapped);
            
            wrapped.Core = new CPUCoreWrapper {Id = obj.Core.Id, Name = obj.Core.Name, Vendor = obj.Core.Vendor.Name};
            wrapped.Socket = obj.Socket.Name;
            wrapped.CoresCount = obj.CoresCount;
            wrapped.ThreadsCount = obj.ThreadsCount;
            wrapped.L2CacheSize = obj.L2CahceSize;
            wrapped.L3CacheSize = obj.L3CacheSize;
            wrapped.TechnicalProcess = obj.TechnicalProcess;
            wrapped.BaseFrequency = obj.BaseFrequency;
            wrapped.SupportedRamTypes = obj.SupportedRamType.Select(i => i.Name).ToList();
            wrapped.SupportedRAMSize = obj.SupportedRAMSize;
            wrapped.HasIntegratedGraphics = obj.HasIntegratedGraphics;
            
            return wrapped;
        }

        /// <summary>
        /// Получение списка всех центральных процессоров в виде обёрток
        /// </summary>
        /// <returns>Список обёрнутых центральных процессоров</returns>
        public List<CPUWrapper> List() {
            return _cpus.List().Select(obj => {
                var wrapped = new CPUWrapper();
            
                _setWrapperPropertiesFromEntity(obj, wrapped);
            
                wrapped.Core = new CPUCoreWrapper {Id = obj.Core.Id, Name = obj.Core.Name, Vendor = obj.Core.Vendor.Name};
                wrapped.Socket = obj.Socket.Name;
                wrapped.CoresCount = obj.CoresCount;
                wrapped.ThreadsCount = obj.ThreadsCount;
                wrapped.L2CacheSize = obj.L2CahceSize;
                wrapped.L3CacheSize = obj.L3CacheSize;
                wrapped.TechnicalProcess = obj.TechnicalProcess;
                wrapped.BaseFrequency = obj.BaseFrequency;
                wrapped.SupportedRamTypes = obj.SupportedRamType.Select(i => i.Name).ToList();
                wrapped.SupportedRAMSize = obj.SupportedRAMSize;
                wrapped.HasIntegratedGraphics = obj.HasIntegratedGraphics;
            
                return wrapped;
            }).ToList();
        }

        /// <summary>
        /// Обновление сущности центрального процессоров
        /// </summary>
        /// <param name="id">Номер процессора</param>
        /// <param name="item">Изменённые данные</param>
        /// <exception cref="NullReferenceException">Если указанного производителя, ядра процессора, сокета или типа ОЗУ не существует</exception>
        public void Update(int id, CPUWrapper item) {
            
            var obj = _cpus.Get(id);
            if (obj == null) {
                throw new NullReferenceException($"Процессора с номером {id} не существует");
            }
            _setEntityPropertiesFromWrapper(obj, item);
            
            var core = _cpuCoreService.GetRaw(item.Core.Id);
            if (core == null) {
                throw new NullReferenceException($"Ядра процессора с номером {item.Core.Id} ({item.Core.Name} не существует)");
            }
            obj.Core = core;
            
            var socket = _cpusSocketService.GetEntityFromString(item.Socket);
            if (socket == null) {
                throw new NullReferenceException($"Сокета {item.Socket} не существует)");
            }
            obj.Socket = socket;

            obj.CoresCount = item.CoresCount;
            obj.ThreadsCount = item.ThreadsCount;
            obj.L2CahceSize = item.L2CacheSize;
            obj.L3CacheSize = item.L3CacheSize;
            obj.TechnicalProcess = item.TechnicalProcess;
            obj.BaseFrequency = item.BaseFrequency;
            obj.SupportedRamType = item.SupportedRamTypes.Select(i => {
                var type = _ramTypeService.GetEntityFromString(i);
                if (type == null) {
                    throw new NullReferenceException($"Типа ОЗУ {i} не существует)");
                }

                return type;
            }).ToList();
            obj.SupportedRAMSize = item.SupportedRAMSize;
            obj.HasIntegratedGraphics = item.HasIntegratedGraphics;
            
            _cpus.Update(obj);
        }

        /// <summary>
        /// Удаление сущности центрального процессоров
        /// </summary>
        /// <param name="id">Номер центрального процессоров</param>
        public void Delete(int id) {
            _cpus.Delete(id);
        }
        
        /// <summary>
        /// Сохранение базы данных
        /// </summary>
        /// <returns>Количество изменений</returns>
        public int Save() {
            return _repositories.Save();
        }
        
        /// <summary>
        /// Фильтрация списка сущностей центральных процессоров
        /// </summary>
        /// <param name="filters">Список фильтров</param>
        /// <returns>Отфильтрованный список обёрток центральных процессоров</returns>
        public IList<CPUWrapper> Filter(FilterContainerWrapper filters) {
            
            var filters_ = new List<Func<CPUWrapper, bool>>();

            IDictionary<string, RangeFilterWrapper> ranges = filters.Ranges;
            IDictionary<string, ChoiceFilterWrapper> choices = filters.Choices;
            IDictionary<string, bool> havings = filters.Havings;

            if (choices.ContainsKey("socket")) {
                filters_.Add(
                    i => choices["socket"].CreateFilterClosure(n => n.Contains(i.Socket))
                );
            }

            if (choices.ContainsKey("supported_ram_types")) {
                filters_.Add(
                    i => choices["supported_ram_types"].CreateFilterClosure(n => {
                        foreach (var entry in i.SupportedRamTypes) {
                            if (entry == n) {
                                return true;
                            }
                        }

                        return false;
                    })
                );
            }

            if (havings.ContainsKey("has_integrated_graphics")) {
                filters_.Add(
                    i => i.HasIntegratedGraphics == havings["has_integrated_graphics"]
                );
            }

            if (ranges.ContainsKey("cores_count")) {
                if (ranges["cores_count"].IsValid()) {
                    filters_.Add(
                        i => ranges["cores_count"].From <= i.CoresCount && i.CoresCount <= ranges["cores_count"].To
                    );
                }
            }

            if (ranges.ContainsKey("technical_process")) {
                if (ranges["technical_process"].IsValid()) {
                    filters_.Add(
                        i => ranges["technical_process"].From <= i.TechnicalProcess && i.TechnicalProcess <= ranges["technical_process"].To
                    );
                }
            }

            if (ranges.ContainsKey("base_frequency")) {
                if (ranges["base_frequency"].IsValid()) {
                    filters_.Add(
                        i => ranges["base_frequency"].From <= i.BaseFrequency && i.BaseFrequency <= ranges["base_frequency"].To
                    );
                }
            }

            if (ranges.ContainsKey("supported_ram_size")) {
                if (ranges["supported_ram_size"].IsValid()) {
                    filters_.Add(
                        i => ranges["supported_ram_size"].From <= i.SupportedRAMSize && i.SupportedRAMSize <= ranges["supported_ram_size"].To
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
                    Type = FilterType.Choice,
                    Name = "socket",
                    VerboseName = "socket",
                    Choices = _repositories.Items.Characteristics.CPUSockets.List().Select(i => i.Name).ToList()
                },
                new FilterDescription {
                    Type = FilterType.Choice,
                    Name = "supported_ram_types",
                    VerboseName = "supported_ram_types",
                    Choices = _repositories.Items.Characteristics.RAMTypes.List().Select(i => i.Name).ToList()
                },
                new FilterDescription {
                    Type = FilterType.Range,
                    Name = "cores_count",
                    VerboseName = "cores_count"
                },
                new FilterDescription {
                    Type = FilterType.Range,
                    Name = "threads_count",
                    VerboseName = "threads_count"
                },
                new FilterDescription {
                    Type = FilterType.Range,
                    Name = "technical_process",
                    VerboseName = "technical_process"
                },
                new FilterDescription {
                    Type = FilterType.Range,
                    Name = "base_frequency",
                    VerboseName = "base_frequency"
                },
                new FilterDescription {
                    Type = FilterType.Range,
                    Name = "supported_ram_size",
                    VerboseName = "supported_ram_size"
                },
                new FilterDescription {
                    Type = FilterType.Having,
                    Name = "has_integrated_graphics",
                    VerboseName = "has_integrated_graphics"
                },
            };
        }
    }
}