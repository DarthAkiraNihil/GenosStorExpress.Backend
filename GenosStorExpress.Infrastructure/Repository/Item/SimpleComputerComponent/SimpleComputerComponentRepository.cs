using GenosStorExpress.Domain.Interface.Item.SimpleComputerComponent;
using GenosStorExpress.Infrastructure.Context;

namespace GenosStorExpress.Infrastructure.Repository.Item.SimpleComputerComponent {
    public class SimpleComputerComponentRepository: ISimpleComputerComponentRepository {

        private GenosStorExpressDatabaseContext _context;
        
        public SimpleComputerComponentRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        // SimpleComputerComponent
        private AudioChipsetRepository? _audioChipsets;
        private CPUCoreRepository? _cpuCores;
        private GPURepository? _gpus;
        private MotherboardChipsetRepository? _motherboardChipsets;
        private NetworkAdapterRepository? _networkAdapters;
        //private SimpleComputerComponentRepository _SimpleComputerComponents;
        private SimpleComputerComponentTypeRepository? _simpleComputerComponentTypes;
        private SSDControllerRepository? _ssdControllers;
        
        // SimpleComputerComponent
        public IAudioChipsetRepository AudioChipsets {
            get {
                if (_audioChipsets == null) {
                    _audioChipsets = new AudioChipsetRepository(_context);
                }
                return _audioChipsets;
            }
        }
        public ICPUCoreRepository CPUCores {
            get {
                if (_cpuCores == null) {
                    _cpuCores = new CPUCoreRepository(_context);
                }
                return _cpuCores;
            }
        }
        public IGPURepository GPUs {
            get {
                if (_gpus == null) {
                    _gpus = new GPURepository(_context);
                }
                return _gpus;
            }
        }
        public IMotherboardChipsetRepository MotherboardChipsets {
            get {
                if (_motherboardChipsets == null) {
                    _motherboardChipsets = new MotherboardChipsetRepository(_context);
                }
                return _motherboardChipsets;
            }
        }
        public INetworkAdapterRepository NetworkAdapters {
            get {
                if (_networkAdapters == null) {
                    _networkAdapters = new NetworkAdapterRepository(_context);
                }
                return _networkAdapters;
            }
        }
        // public ISimpleComputerComponentRepository SimpleComputerComponents {
        //     get {
        //         if (_simpleComputerComponentTypes == null) {
        //             _simpleComputerComponentTypes = new Certificate80PlusRepository(_context);
        //         }
        //         return _simpleComputerComponentTypes;
        //     }
        // }
        public ISimpleComputerComponentTypeRepository SimpleComputerComponentTypes {
            get {
                if (_simpleComputerComponentTypes == null) {
                    _simpleComputerComponentTypes = new SimpleComputerComponentTypeRepository(_context);
                }
                return _simpleComputerComponentTypes;
            }
        }
        public ISSDControllerRepository SSDControllers {
            get {
                if (_ssdControllers == null) {
                    _ssdControllers = new SSDControllerRepository(_context);
                }
                return _ssdControllers;
            }
        }
    }
}