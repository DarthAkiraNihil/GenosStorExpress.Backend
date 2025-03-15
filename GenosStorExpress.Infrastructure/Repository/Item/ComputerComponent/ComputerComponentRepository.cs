
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;
using GenosStorExpress.Infrastructure.Context;

namespace GenosStorExpress.Infrastructure.Repository.Item.ComputerComponent {
    public class ComputerComponentRepository: IComputerComponentRepository {

        private GenosStorExpressDatabaseContext _context;
        // ComputerComponent
        private ComputerCaseRepository? _computerCases;
        //private ComputerComponentRepository _ComputerComponents;
        private CPUCoolerRepository? _cpuCoolers;
        private CPURepository? _cpus;
        private DiskDriveRepository? _diskDrives;
        private DisplayRepository? _displays;
        private GraphicsCardRepository? _graphicsCards;
        private HDDRepository? _hdds;
        private KeyboardRepository? _keyboards;
        private MotherboardRepository? _motherboards;
        private MouseRepository? _mouses;
        private NVMeSSDRepository? _nvmeSSDs;
        private PowerSupplyRepository? _powerSupplies;
        private RAMRepository? _rams;
        private SataSSDRepository? _sataSSDs;

        public ComputerComponentRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }
        public IComputerCaseRepository ComputerCases {
            get {
                if (_computerCases == null) {
                    _computerCases = new ComputerCaseRepository(_context);
                }
                return _computerCases;
            }
        }
        // public IComputerComponentRepository ComputerComponents {
        //     get {
        //         if (+comp == null) {
        //             _certificates80Plus = new Certificate80PlusRepository(_context);
        //         }
        //         return _certificates80Plus;
        //     }
        // }
        public ICPUCoolerRepository CPUCoolers {
            get {
                if (_cpuCoolers == null) {
                    _cpuCoolers = new CPUCoolerRepository(_context);
                }
                return _cpuCoolers;
            }
        }
        public ICPURepository CPUs {
            get {
                if (_cpus == null) {
                    _cpus = new CPURepository(_context);
                }
                return _cpus;
            }
        }
        public IDiskDriveRepository DiskDrives {
            get {
                if (_diskDrives == null) {
                    _diskDrives = new DiskDriveRepository(_context);
                }
                return _diskDrives;
            }
        }
        public IDisplayRepository Displays {
            get {
                if (_displays == null) {
                    _displays = new DisplayRepository(_context);
                }
                return _displays;
            }
        }
        public IGraphicsCardRepository GraphicsCards {
            get {
                if (_graphicsCards == null) {
                    _graphicsCards = new GraphicsCardRepository(_context);
                }
                return _graphicsCards;
            }
        }
        public IHDDRepository HDDs {
            get {
                if (_hdds == null) {
                    _hdds = new HDDRepository(_context);
                }
                return _hdds;
            }
        }
        public IKeyboardRepository Keyboards {
            get {
                if (_keyboards == null) {
                    _keyboards = new KeyboardRepository(_context);
                }
                return _keyboards;
            }
        }
        public IMotherboardRepository Motherboards {
            get {
                if (_motherboards == null) {
                    _motherboards = new MotherboardRepository(_context);
                }
                return _motherboards;
            }
        }
        public IMouseRepository Mouses {
            get {
                if (_mouses == null) {
                    _mouses = new MouseRepository(_context);
                }
                return _mouses;
            }
        }
        public INVMeSSDRepository NVMeSSDs {
            get {
                if (_nvmeSSDs == null) {
                    _nvmeSSDs = new NVMeSSDRepository(_context);
                }
                return _nvmeSSDs;
            }
        }
        public IPowerSupplyRepository PowerSupplies {
            get {
                if (_powerSupplies == null) {
                    _powerSupplies = new PowerSupplyRepository(_context);
                }
                return _powerSupplies;
            }
        }
        public IRAMRepository RAMs {
            get {
                if (_rams == null) {
                    _rams = new RAMRepository(_context);
                }
                return _rams;
            }
        }
        public ISataSSDRepository SataSSDs {
            get {
                if (_sataSSDs == null) {
                    _sataSSDs = new SataSSDRepository(_context);
                }
                return _sataSSDs;
            }
        }
        
    }
}