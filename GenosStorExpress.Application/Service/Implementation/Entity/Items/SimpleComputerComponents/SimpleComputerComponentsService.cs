using GenosStorExpress.Application.Service.Interface.Entity.Items.SimpleComputerComponents;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.SimpleComputerComponents {
    public class SimpleComputerComponentsService: ISimpleComputerComponentService {
        private readonly ICPUCoreService _cpuCoreService;
        private readonly IGPUService _gpusService;
        private readonly IAudioChipsetService _audioChipsetService;
        private readonly IMotherboardChipsetService _motherboardChipsetService;
        private readonly INetworkAdapterService _networkAdapterService;
        private readonly ISimpleComputerComponentTypeService _simpleComputerComponentTypeService;
        private readonly ISSDControllerService _ssdControllerService;

        public SimpleComputerComponentsService(
            ICPUCoreService cpuCoreService,
            IGPUService gpusService,
            IAudioChipsetService audioChipsetService,
            IMotherboardChipsetService motherboardChipsetService,
            INetworkAdapterService networkAdapterService,
            ISimpleComputerComponentTypeService simpleComputerComponentTypeService,
            ISSDControllerService ssdControllerService) {
            _cpuCoreService = cpuCoreService;
            _gpusService = gpusService;
            _audioChipsetService = audioChipsetService;
            _motherboardChipsetService = motherboardChipsetService;
            _networkAdapterService = networkAdapterService;
            _simpleComputerComponentTypeService = simpleComputerComponentTypeService;
            _ssdControllerService = ssdControllerService;
        }

        public ICPUCoreService CPUCores {
            get {
                return _cpuCoreService;
            }
        }

        public IGPUService GPUs {
            get {
                return _gpusService;
            }
        }

        public ISSDControllerService SSDControllers {
            get {
                return _ssdControllerService;
            }
        }

        public ISimpleComputerComponentTypeService Types {
            get {
                return _simpleComputerComponentTypeService;
            }
        }

        public INetworkAdapterService NetworkAdapters {
            get {
                return _networkAdapterService;
            }
        }

        public IMotherboardChipsetService MotherboardChipsets {
            get {
                return _motherboardChipsetService;
            }
        }

        public IAudioChipsetService AudioChipsets {
            get {
                return _audioChipsetService;
            }
        }
    }
}