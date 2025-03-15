using GenosStorExpress.Domain.Interface.Item.Characteristic;
using GenosStorExpress.Infrastructure.Context;

namespace GenosStorExpress.Infrastructure.Repository.Item.Characteristic {
    public class CharacteristicRepository: ICharacteristicRepository {
        
        private GenosStorExpressDatabaseContext _context;
        
        // Characteristic
        private Certificate80PlusRepository? _certificates80Plus;
        private ComputerCaseTypesizeRepository? _computerCaseTypesizes;
        private CoolerMaterialRepository? _coolerMaterials;
        private CPUSocketRepository? _cpuSockets;
        private DefinitionRepository? _definitions;
        private DPIModeRepository? _dpiModes;
        private KeyboardTypeRepository? _keyboardTypes;
        private KeyboardTypesizeRepository? _keyboardTypesizes;
        private MatrixTypeRepository? _matrixTypes;
        private MotherboardFormFactorRepository? _motherboardFormFactors;
        private PCIEVersionRepository? _pCIEVersions;
        private RAMTypeRepository? _ramTypes;
        private UnderlightRepository? _underlights;
        private VendorRepository? _vendors;
        private VesaSizeRepository? _vesaSizes;
        private VideoPortRepository? _videoPorts;

        public CharacteristicRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }
        
        // Characteristic
        public ICertificate80PlusRepository Certificates80Plus {
            get {
                if (_certificates80Plus == null) {
                    _certificates80Plus = new Certificate80PlusRepository(_context);
                }
                return _certificates80Plus;
            }
        }
        public IComputerCaseTypesizeRepository ComputerCaseTypesizes {
            get {
                if (_computerCaseTypesizes == null) {
                    _computerCaseTypesizes = new ComputerCaseTypesizeRepository(_context);
                }
                return _computerCaseTypesizes;
            }
        }
        public ICoolerMaterialRepository CoolerMaterials {
            get {
                if (_coolerMaterials == null) {
                    _coolerMaterials = new CoolerMaterialRepository(_context);
                }
                return _coolerMaterials;
            }
        }
        public ICPUSocketRepository CPUSockets {
            get {
                if (_cpuSockets == null) {
                    _cpuSockets = new CPUSocketRepository(_context);
                }
                return _cpuSockets;
            }
        }
        public IDefinitionRepository Definitions {
            get {
                if (_definitions == null) {
                    _definitions = new DefinitionRepository(_context);
                }
                return _definitions;
            }
        }
        public IDPIModeRepository DPIModes {
            get {
                if (_dpiModes == null) {
                    _dpiModes = new DPIModeRepository(_context);
                }
                return _dpiModes;
            }
        }
        public IKeyboardTypeRepository KeyboardTypes {
            get {
                if (_keyboardTypes == null) {
                    _keyboardTypes = new KeyboardTypeRepository(_context);
                }
                return _keyboardTypes;
            }
        }
        public IKeyboardTypesizeRepository KeyboardTypesizes {
            get {
                if (_keyboardTypesizes == null) {
                    _keyboardTypesizes = new KeyboardTypesizeRepository(_context);
                }
                return _keyboardTypesizes;
            }
        }
        public IMatrixTypeRepository MatrixTypes {
            get {
                if (_matrixTypes == null) {
                    _matrixTypes = new MatrixTypeRepository(_context);
                }
                return _matrixTypes;
            }
        }
        public IMotherboardFormFactorRepository MotherboardFormFactors {
            get {
                if (_motherboardFormFactors == null) {
                    _motherboardFormFactors = new MotherboardFormFactorRepository(_context);
                }
                return _motherboardFormFactors;
            }
        }
        public IPCIEVersionRepository PCIEVersions {
            get {
                if (_pCIEVersions == null) {
                    _pCIEVersions = new PCIEVersionRepository(_context);
                }
                return _pCIEVersions;
            }
        }
        public IRAMTypeRepository RAMTypes {
            get {
                if (_ramTypes == null) {
                    _ramTypes = new RAMTypeRepository(_context);
                }
                return _ramTypes;
            }
        }
        public IUnderlightRepository Underlights {
            get {
                if (_underlights == null) {
                    _underlights = new UnderlightRepository(_context);
                }
                return _underlights;
            }
        }
        public IVendorRepository Vendors {
            get {
                if (_vendors == null) {
                    _vendors = new VendorRepository(_context);
                }
                return _vendors;
            }
        }
        public IVesaSizeRepository VesaSizes {
            get {
                if (_vesaSizes == null) {
                    _vesaSizes = new VesaSizeRepository(_context);
                }
                return _vesaSizes;
            }
        }
        public IVideoPortRepository VideoPorts {
            get {
                if (_videoPorts == null) {
                    _videoPorts = new VideoPortRepository(_context);
                }
                return _videoPorts;
            }
        }
    }
}