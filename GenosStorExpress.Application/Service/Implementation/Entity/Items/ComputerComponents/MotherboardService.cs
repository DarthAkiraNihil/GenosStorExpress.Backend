﻿using GenosStorExpress.Application.Service.Implementation.Base;
using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Application.Service.Interface.Entity.Items.SimpleComputerComponents;
using GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;
using GenosStorExpress.Application.Wrappers.Entity.Item.SimpleComputerComponent;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.ComputerComponents {
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
            
        }

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

        public void Delete(int id) {
            _motherboards.Delete(id);
        }

        public List<MotherboardWrapper> Filter(List<Func<MotherboardWrapper, bool>> filters) {
            var result = List();
            foreach (var filter in filters) {
                result = result.Where(filter).ToList();
            }
            return result;
        }
        
        public int Save() {
            return _repositories.Save();
        }

        public MotherboardService(IItemTypeService itemTypeService, IVendorService vendorService, IGenosStorExpressRepositories repositories, ICPUCoreService cpuCoreService, IRAMTypeService ramTypeService, IVideoPortService videoPortService, IMotherboardFormFactorService motherboardFormFactorService, ICPUSocketService cpusocketService, IPCIEVersionService pCieVersionService, IMotherboardChipsetService motherboardChipsetService, IAudioChipsetService audioChipsetService, INetworkAdapterService networkAdapterService) : base(itemTypeService, vendorService) {
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