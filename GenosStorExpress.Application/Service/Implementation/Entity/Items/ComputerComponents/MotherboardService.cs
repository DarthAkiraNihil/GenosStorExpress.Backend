using GenosStorExpress.Application.Service.Implementation.Base;
using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Application.Service.Interface.Entity.Items.SimpleComputerComponents;
using GenosStorExpress.Application.Service.Interface.Entity.Orders;
using GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;
using GenosStorExpress.Application.Wrappers.Entity.Item.SimpleComputerComponent;
using GenosStorExpress.Application.Wrappers.Filters;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.ComputerComponents {
    /// <summary>
    /// Реализация сервиса материнских плат
    /// </summary>
    public class MotherboardService: AbstractComputerComponentService, IMotherboardService {

        private readonly IGenosStorExpressRepositories _repositories;
        private readonly IMotherboardRepository _motherboards;
        private readonly ICPUCoreService _cpuCoreService;
        private readonly IRAMTypeService _ramTypeService;
        private readonly IVideoPortService _videoPortService;
        private readonly IMotherboardFormFactorService _motherboardFormFactorService;
        private readonly ICPUSocketService _cpusocketService;
        private readonly IPCIEVersionService _pCIEVersionService;
        private readonly IMotherboardChipsetService _motherboardChipsetService;
        private readonly IAudioChipsetService _audioChipsetService;
        private readonly INetworkAdapterService _networkAdapterService;

        /// <summary>
        /// Создание сущности материнской платы из обёртки
        /// </summary>
        /// <param name="item">Обёрнутая материнская плата</param>
        /// <exception cref="NullReferenceException">
        ///     Если какого-то из параметров, указывающих на другие сущности, не существует
        /// </exception>
        public void Create(MotherboardWrapper item) {
            
            var created = new Motherboard();
            
            _setEntityPropertiesFromWrapper(created, item);

            created.SupportedCPUCores = item.SupportedCPUCores.Select(i => {
                var core = _cpuCoreService.GetRaw(i.Id);
                if (core == null) {
                    throw new NullReferenceException($"Ядра процессора с номером {i.Id} ({i.Name}) не существует)");
                }

                return core;
            }).ToList();
            created.SupportedRAMTypes = item.SupportedRAMTypes.Select(i => {
                var type = _ramTypeService.GetEntityFromString(i);
                if (type == null) {
                    throw new NullReferenceException($"Типа ОЗУ {i} не существует)");
                }

                return type;
            }).ToList();
            created.RAMSlots = item.RAMSlots;
            created.RAMChannels = item.RAMChannels;
            created.MaxRAMFrequency = item.MaxRAMFrequency;
            created.PCIESlotsCount = item.PCIESlotsCount;
            created.HasNVMeSupport = item.HasNVMeSupport;
            created.M2SlotsCount = item.M2SlotsCount;
            created.SataPortsCount = item.SataPortsCount;
            created.USBPortsCount = item.USBPortsCount;
            created.VideoPorts = item.VideoPorts.Select(i => {
                var port = _videoPortService.GetEntityFromString(i);
                if (port == null) {
                    throw new NullReferenceException($"Видеопорта {i} не существует)");
                }

                return port;
            }).ToList();
            created.RJ45PortsCount = item.RJ45PortsCount;
            created.DigitalAudioPortsCount = created.DigitalAudioPortsCount;
            created.NetworkAdapterSpeed = item.NetworkAdapterSpeed;
            
            var formFactor = _motherboardFormFactorService.GetEntityFromString(item.FormFactor);
            if (formFactor == null) {
                throw new NullReferenceException($"Форм-фактора материнской платы {item.FormFactor} не существует");
            }
            created.FormFactor = formFactor;
            
            var socket = _cpusocketService.GetEntityFromString(item.CPUSocket);
            if (socket == null) {
                throw new NullReferenceException($"Сокета процессора {item.CPUSocket} не существует");
            }
            created.CPUSocket = socket;

            var pcieVersion = _pCIEVersionService.GetEntityFromString(item.PCIEVersion);
            if (pcieVersion == null) {
                throw new NullReferenceException($"Версии PCI-e {item.PCIEVersion} не существует");
            }
            created.PCIEVersion = pcieVersion;
            
            var motherboardChipset = _motherboardChipsetService.GetRaw((int) item.MotherboardChipset.Id);
            if (motherboardChipset == null) {
                throw new NullReferenceException($"Чипсета с номером {item.MotherboardChipset.Id} ({item.MotherboardChipset.Name}) не существует");
            }
            created.MotherboardChipset = motherboardChipset;
            
            var audioChipset = _audioChipsetService.GetRaw((int) item.AudioChipset.Id);
            if (audioChipset == null) {
                throw new NullReferenceException($"Аудиочипсета с номером {item.AudioChipset.Id} ({item.AudioChipset.Name}) не существует");
            }
            created.AudioChipset = audioChipset;
            
            var networkAdapter = _networkAdapterService.GetRaw((int) item.MotherboardChipset.Id);
            if (networkAdapter == null) {
                throw new NullReferenceException($"Сетевого адаптера с номером {item.NetworkAdapter.Id} ({item.NetworkAdapter.Name}) не существует");
            }
            
            _motherboards.Create(created);
            item.Id = created.Id;
        }

        /// <summary>
        /// Получение объекта-обёртки сущности материнской платы по его номеру
        /// </summary>
        /// <param name="id">Номер платаа</param>
        /// <returns>Обёрнутая плата или null в случае его отсутствия</returns>
        public MotherboardWrapper? Get(int id) {
            Motherboard? obj =  _motherboards.Get(id);
            if (obj == null) {
                return null;
            }
            var wrapped = new MotherboardWrapper();
            
            _setWrapperPropertiesFromEntity(obj, wrapped);
            
            wrapped.SupportedCPUCores = obj.SupportedCPUCores.Select(i => new CPUCoreWrapper {Id = i.Id, Name = i.Name, Vendor = i.Vendor.Name}).ToList();
            wrapped.SupportedRAMTypes = obj.SupportedRAMTypes.Select(i => i.Name).ToList();
            wrapped.RAMSlots = obj.RAMSlots;
            wrapped.RAMChannels = obj.RAMChannels;
            wrapped.MaxRAMFrequency = obj.MaxRAMFrequency;
            wrapped.PCIESlotsCount = obj.PCIESlotsCount;
            wrapped.HasNVMeSupport = obj.HasNVMeSupport;
            wrapped.M2SlotsCount = obj.M2SlotsCount;
            wrapped.SataPortsCount = obj.SataPortsCount;
            wrapped.USBPortsCount = obj.USBPortsCount;
            wrapped.VideoPorts = obj.VideoPorts.Select(i => i.Name).ToList();
            wrapped.RJ45PortsCount = obj.RJ45PortsCount;
            wrapped.DigitalAudioPortsCount = wrapped.DigitalAudioPortsCount;
            wrapped.NetworkAdapterSpeed = obj.NetworkAdapterSpeed;
            wrapped.FormFactor = obj.FormFactor.Name;
            wrapped.CPUSocket = obj.CPUSocket.Name;
            wrapped.PCIEVersion = obj.PCIEVersion.Name;
            wrapped.MotherboardChipset = new MotherboardChipsetWrapper { Id = obj.MotherboardChipset.Id, Name = obj.MotherboardChipset.Name, Model = obj.MotherboardChipset.Model, Type = obj.MotherboardChipset.Type.Name };
            wrapped.AudioChipset = new AudioChipsetWrapper { Id = obj.AudioChipset.Id, Name = obj.AudioChipset.Name, Model = obj.AudioChipset.Model, Type = obj.AudioChipset.Type.Name };
            wrapped.NetworkAdapter = new NetworkAdapterWrapper { Id = obj.NetworkAdapter.Id, Name = obj.NetworkAdapter.Name, Model = obj.NetworkAdapter.Model, Type = obj.NetworkAdapter.Type.Name };

            return wrapped;
        }

        /// <summary>
        /// Получение списка всех материнских плат в виде обёрток
        /// </summary>
        /// <returns>Список обёрнутых материнских плат</returns>
        public List<MotherboardWrapper> List() {
            return _motherboards.List().Select(obj => {
                var wrapped = new MotherboardWrapper();
            
                _setWrapperPropertiesFromEntity(obj, wrapped);
            
                wrapped.SupportedCPUCores = obj.SupportedCPUCores.Select(i => new CPUCoreWrapper {Id = i.Id, Name = i.Name, Vendor = i.Vendor.Name}).ToList();
                wrapped.SupportedRAMTypes = obj.SupportedRAMTypes.Select(i => i.Name).ToList();
                wrapped.RAMSlots = obj.RAMSlots;
                wrapped.RAMChannels = obj.RAMChannels;
                wrapped.MaxRAMFrequency = obj.MaxRAMFrequency;
                wrapped.PCIESlotsCount = obj.PCIESlotsCount;
                wrapped.HasNVMeSupport = obj.HasNVMeSupport;
                wrapped.M2SlotsCount = obj.M2SlotsCount;
                wrapped.SataPortsCount = obj.SataPortsCount;
                wrapped.USBPortsCount = obj.USBPortsCount;
                wrapped.VideoPorts = obj.VideoPorts.Select(i => i.Name).ToList();
                wrapped.RJ45PortsCount = obj.RJ45PortsCount;
                wrapped.DigitalAudioPortsCount = wrapped.DigitalAudioPortsCount;
                wrapped.NetworkAdapterSpeed = obj.NetworkAdapterSpeed;
                wrapped.FormFactor = obj.FormFactor.Name;
                wrapped.CPUSocket = obj.CPUSocket.Name;
                wrapped.PCIEVersion = obj.PCIEVersion.Name;
                wrapped.MotherboardChipset = new MotherboardChipsetWrapper { Id = obj.MotherboardChipset.Id, Name = obj.MotherboardChipset.Name, Model = obj.MotherboardChipset.Model, Type = obj.MotherboardChipset.Type.Name };
                wrapped.AudioChipset = new AudioChipsetWrapper { Id = obj.AudioChipset.Id, Name = obj.AudioChipset.Name, Model = obj.AudioChipset.Model, Type = obj.AudioChipset.Type.Name };
                wrapped.NetworkAdapter = new NetworkAdapterWrapper { Id = obj.NetworkAdapter.Id, Name = obj.NetworkAdapter.Name, Model = obj.NetworkAdapter.Model, Type = obj.NetworkAdapter.Type.Name };

                return wrapped;
            }).ToList();
        }

        /// <summary>
        /// Обновление сущности материнской платы
        /// </summary>
        /// <param name="id">Номер платы</param>
        /// <param name="item">Изменённые данные</param>
        /// <exception cref="NullReferenceException">Если какого-то из параметров, указывающих на другие сущности, или самой сущности не существует</exception>
        public void Update(int id, MotherboardWrapper item) {
            
            var obj = _motherboards.Get(id);
            if (obj == null) {
                throw new NullReferenceException($"Материнской платы с номером {id} не существует");
            }
            
            _setEntityPropertiesFromWrapper(obj, item);

            obj.SupportedCPUCores = item.SupportedCPUCores.Select(i => {
                var core = _cpuCoreService.GetRaw(i.Id);
                if (core == null) {
                    throw new NullReferenceException($"Ядра процессора с номером {i.Id} ({i.Name}) не существует)");
                }

                return core;
            }).ToList();
            obj.SupportedRAMTypes = item.SupportedRAMTypes.Select(i => {
                var type = _ramTypeService.GetEntityFromString(i);
                if (type == null) {
                    throw new NullReferenceException($"Типа ОЗУ {i} не существует)");
                }

                return type;
            }).ToList();
            obj.RAMSlots = item.RAMSlots;
            obj.RAMChannels = item.RAMChannels;
            obj.MaxRAMFrequency = item.MaxRAMFrequency;
            obj.PCIESlotsCount = item.PCIESlotsCount;
            obj.HasNVMeSupport = item.HasNVMeSupport;
            obj.M2SlotsCount = item.M2SlotsCount;
            obj.SataPortsCount = item.SataPortsCount;
            obj.USBPortsCount = item.USBPortsCount;
            obj.VideoPorts = item.VideoPorts.Select(i => {
                var port = _videoPortService.GetEntityFromString(i);
                if (port == null) {
                    throw new NullReferenceException($"Видеопорта {i} не существует)");
                }

                return port;
            }).ToList();
            obj.RJ45PortsCount = item.RJ45PortsCount;
            obj.DigitalAudioPortsCount = obj.DigitalAudioPortsCount;
            obj.NetworkAdapterSpeed = item.NetworkAdapterSpeed;
            
            var formFactor = _motherboardFormFactorService.GetEntityFromString(item.FormFactor);
            if (formFactor == null) {
                throw new NullReferenceException($"Форм-фактора материнской платы {item.FormFactor} не существует");
            }
            obj.FormFactor = formFactor;
            
            var socket = _cpusocketService.GetEntityFromString(item.CPUSocket);
            if (socket == null) {
                throw new NullReferenceException($"Сокета процессора {item.CPUSocket} не существует");
            }
            obj.CPUSocket = socket;

            var pcieVersion = _pCIEVersionService.GetEntityFromString(item.PCIEVersion);
            if (pcieVersion == null) {
                throw new NullReferenceException($"Версии PCI-e {item.PCIEVersion} не существует");
            }
            obj.PCIEVersion = pcieVersion;
            
            var motherboardChipset = _motherboardChipsetService.GetRaw((int) item.MotherboardChipset.Id);
            if (motherboardChipset == null) {
                throw new NullReferenceException($"Чипсета с номером {item.MotherboardChipset.Id} ({item.MotherboardChipset.Name}) не существует");
            }
            obj.MotherboardChipset = motherboardChipset;
            
            var audioChipset = _audioChipsetService.GetRaw((int) item.AudioChipset.Id);
            if (audioChipset == null) {
                throw new NullReferenceException($"Аудиочипсета с номером {item.AudioChipset.Id} ({item.AudioChipset.Name}) не существует");
            }
            obj.AudioChipset = audioChipset;
            
            var networkAdapter = _networkAdapterService.GetRaw((int) item.MotherboardChipset.Id);
            if (networkAdapter == null) {
                throw new NullReferenceException($"Сетевого адаптера с номером {item.NetworkAdapter.Id} ({item.NetworkAdapter.Name}) не существует");
            }
            
            _motherboards.Update(obj);
            
        }

        /// <summary>
        /// Удаление сущности материнской платы
        /// </summary>
        /// <param name="id">Номер материнской платы</param>
        public void Delete(int id) {
            _motherboards.Delete(id);
        }

        /// <summary>
        /// Фильтрация списка сущностей материнских плат
        /// </summary>
        /// <param name="filters">Список фильтров</param>
        /// <returns>Отфильтрованный список обёрток материнских плат</returns>
        public IList<MotherboardWrapper> Filter(FilterContainerWrapper filters) {
            
            var filters_ = new List<Func<MotherboardWrapper, bool>>();

            IDictionary<string, RangeFilterWrapper> ranges = filters.Ranges;
            IDictionary<string, ChoiceFilterWrapper> choices = filters.Choices;
            IDictionary<string, bool> havings = filters.Havings;

            if (choices.ContainsKey("formfactor")) {
                filters_.Add(
                    i => choices["form_factor"].CreateFilterClosure(n => n.Contains(i.FormFactor))
                );
            }
            
            if (choices.ContainsKey("socket")) {
                filters_.Add(
                    i => choices["socket"].CreateFilterClosure(n => n.Contains(i.CPUSocket))
                );
            }

            if (choices.ContainsKey("supported_cpu_cores")) {
                filters_.Add(
                    i => choices["supported_cpu_cores"].CreateFilterClosure(n => {
                        foreach (var entry in i.SupportedCPUCores) {
                            if (entry.Name == n) {
                                return true;
                            }
                        }

                        return false;
                    })
                );
            }

            if (choices.ContainsKey("suppported_ram_types")) {
                filters_.Add(
                    i => choices["suppported_ram_types"].CreateFilterClosure(n => {
                        foreach (var entry in i.SupportedRAMTypes) {
                            if (entry == n) {
                                return true;
                            }
                        }

                        return false;
                    })
                );
            }
            

            if (ranges.ContainsKey("ram_slots_count")) {
                if (ranges["ram_slots_count"].IsValid()) {
                    filters_.Add(
                        i => ranges["ram_slots_count"].From <= i.RAMSlots && i.RAMSlots <= ranges["ram_slots_count"].To
                    );
                }
            }

            if (ranges.ContainsKey("pcie_slots_count")) {
                if (ranges["pcie_slots_count"].IsValid()) {
                    filters_.Add(
                        i => ranges["pcie_slots_count"].From <= i.PCIESlotsCount && i.PCIESlotsCount <= ranges["pcie_slots_count"].To
                    );
                }
            }

            if (havings.ContainsKey("has_nvme_suppport")) {
                filters_.Add(
                    i => i.HasNVMeSupport == havings["has_nvme_support"]
                );
            }

            if (ranges.ContainsKey("sata_ports_count")) {
                if (ranges["sata_ports_count"].IsValid()) {
                    filters_.Add(
                        i => ranges["sata_ports_count"].From <= i.SataPortsCount && i.SataPortsCount <= ranges["sata_ports_count"].To
                    );
                }
            }

            if (ranges.ContainsKey("usb_ports_count")) {
                if (ranges["usb_ports_count"].IsValid()) {
                    filters_.Add(
                        i => ranges["usb_ports_count"].From <= i.USBPortsCount && i.USBPortsCount <= ranges["usb_ports_count"].To
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
                    Name = "form_factor",
                    VerboseName = "form_factor",
                    Choices = _repositories.Items.Characteristics.MotherboardFormFactors.List().Select(i => i.Name).ToList()
                },
                new FilterDescription {
                    Type = FilterType.Choice,
                    Name = "socket",
                    VerboseName = "socket",
                    Choices = _repositories.Items.Characteristics.CPUSockets.List().Select(i => i.Name).ToList()
                },
                new FilterDescription {
                    Type = FilterType.Choice,
                    Name = "supported_cpu_cores",
                    VerboseName = "supported_cpu_cores",
                    Choices = _repositories.Items.SimpleComputerComponents.CPUCores.List().Select(i => i.Name).ToList()
                },
                new FilterDescription {
                    Type = FilterType.Choice,
                    Name = "supported_ram_types",
                    VerboseName = "supported_ram_types",
                    Choices = _repositories.Items.Characteristics.RAMTypes.List().Select(i => i.Name).ToList()
                },
                new FilterDescription {
                    Type = FilterType.Range,
                    Name = "ram_slots_count",
                    VerboseName = "ram_slots_count",
                },
                new FilterDescription {
                    Type = FilterType.Range,
                    Name = "pcie_slots_count",
                    VerboseName = "pcie_slots_count",
                },
                new FilterDescription {
                    Type = FilterType.Range,
                    Name = "sata_ports_count",
                    VerboseName = "sata_ports_count",
                },
                new FilterDescription {
                    Type = FilterType.Range,
                    Name = "usb_ports_count",
                    VerboseName = "usb_ports_count",
                },
                new FilterDescription {
                    Type = FilterType.Having,
                    Name = "has_nvme_support",
                    VerboseName = "has_nvme_support",
                },
            };
        }

        /// <summary>
        /// Сохранение базы данных
        /// </summary>
        /// <returns>Количество изменений</returns>
        public int Save() {
            return _repositories.Save();
        }

        /// <summary>
        /// Стандартный конструктор
        /// </summary>
        /// <param name="itemTypeService">Сервис типов предметов</param>
        /// <param name="activeDiscountService">Сервис скидок</param>
        /// <param name="vendorService">Сервис производителей</param>
        /// <param name="repositories">Репозитории проекта</param>
        /// <param name="cpuCoreService">Сервис ядер процессоров</param>
        /// <param name="ramTypeService">Сервис типов ОЗУ</param>
        /// <param name="videoPortService">Сервис видеопортов</param>
        /// <param name="motherboardFormFactorService">Сервис форм-факторов материнских плат</param>
        /// <param name="cpusocketService">Сервис сокетов процессоров</param>
        /// <param name="pCieVersionService">Сервис версий PCI-e</param>
        /// <param name="motherboardChipsetService">Сервис чипсетов материнских плат</param>
        /// <param name="audioChipsetService">Сервис аудиочипсетов</param>
        /// <param name="networkAdapterService">Сервис сетевых адаптеров</param>
        public MotherboardService(IItemTypeService itemTypeService, IActiveDiscountService activeDiscountService, IVendorService vendorService, IGenosStorExpressRepositories repositories, ICPUCoreService cpuCoreService, IRAMTypeService ramTypeService, IVideoPortService videoPortService, IMotherboardFormFactorService motherboardFormFactorService, ICPUSocketService cpusocketService, IPCIEVersionService pCieVersionService, IMotherboardChipsetService motherboardChipsetService, IAudioChipsetService audioChipsetService, INetworkAdapterService networkAdapterService) : base(itemTypeService, activeDiscountService, vendorService) {
            _repositories = repositories;
            _cpuCoreService = cpuCoreService;
            _ramTypeService = ramTypeService;
            _videoPortService = videoPortService;
            _motherboardFormFactorService = motherboardFormFactorService;
            _cpusocketService = cpusocketService;
            _pCIEVersionService = pCieVersionService;
            _motherboardChipsetService = motherboardChipsetService;
            _audioChipsetService = audioChipsetService;
            _networkAdapterService = networkAdapterService;
            _motherboards = _repositories.Items.ComputerComponents.Motherboards;
        }
    }
}