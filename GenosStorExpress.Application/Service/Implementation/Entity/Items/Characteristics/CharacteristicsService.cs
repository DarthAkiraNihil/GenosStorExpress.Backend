using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.Characteristics {
    public class CharacteristicsService: ICharacteristicsService {
        private readonly ICPUSocketService _cpusocketService;
        private readonly IMotherboardFormFactorService _motherboardFormFactorService;
        private readonly IRAMTypeService _ramTypeService;
        private readonly IVendorService _vendorService;
        private readonly IComputerCaseTypesizeService _computerCaseTypesizeService;
        private readonly ICoolerMaterialService _coolerMaterialService;
        private readonly IDefinitionService _definitionService;
        private readonly IKeyboardTypeService _keyboardTypeService;
        private readonly IKeyboardTypesizeService _keyboardTypesizeService;
        private readonly IMatrixTypeService _matrixTypeService;
        private readonly IUnderlightService _underlightService;
        private readonly IVesaSizeService _vesaSizeService;
        private readonly IVideoPortService _videoPortService;
        private readonly ICertificate80PlusService _certificate80PlusService;
        private readonly IDPIModeService _dpiModeService;

        public CharacteristicsService(
            ICPUSocketService cpusocketService,
            IMotherboardFormFactorService motherboardFormFactorService,
            IRAMTypeService ramTypeService,
            IVendorService vendorService,
            IComputerCaseTypesizeService computerCaseTypesizeService,
            ICoolerMaterialService coolerMaterialService,
            IDefinitionService definitionService,
            IKeyboardTypeService keyboardTypeService,
            IKeyboardTypesizeService keyboardTypesizeService,
            IMatrixTypeService matrixTypeService,
            IUnderlightService underlightService,
            IVesaSizeService vesaSizeService,
            IVideoPortService videoPortService,
            ICertificate80PlusService certificate80PlusService,
            IDPIModeService dpiModeService
            ) {
            _cpusocketService = cpusocketService;
            _motherboardFormFactorService = motherboardFormFactorService;
            _ramTypeService = ramTypeService;
            _vendorService = vendorService;
            _computerCaseTypesizeService = computerCaseTypesizeService;
            _coolerMaterialService = coolerMaterialService;
            _definitionService = definitionService;
            _keyboardTypeService = keyboardTypeService;
            _keyboardTypesizeService = keyboardTypesizeService;
            _matrixTypeService = matrixTypeService;
            _underlightService = underlightService;
            _vesaSizeService = vesaSizeService;
            _videoPortService = videoPortService;
            _certificate80PlusService = certificate80PlusService;
            _dpiModeService = dpiModeService;
        }

        public ICPUSocketService CPUSockets {
            get {
                return _cpusocketService;
            }
        }

        public IMotherboardFormFactorService MotherboardFormFactors {
            get {
                return _motherboardFormFactorService;
            }
        }

        public IRAMTypeService RAMTypes {
            get {
                return _ramTypeService;
            }
        }

        public IVendorService Vendors {
            get {
                return _vendorService;
            }
        }

        public IComputerCaseTypesizeService ComputerCaseTypesizes {
            get {
                return _computerCaseTypesizeService;
            }
        }

        public ICoolerMaterialService CoolerMaterials {
            get {
                return _coolerMaterialService;
            }
        }

        public IDefinitionService Definitions {
            get {
                return _definitionService;
            }
        }

        public IKeyboardTypeService KeyboardTypes {
            get {
                return _keyboardTypeService;
            }
        }

        public IKeyboardTypesizeService KeyboardTypesizes {
            get {
                return _keyboardTypesizeService;
            }
        }

        public IMatrixTypeService MatrixTypes {
            get {
                return _matrixTypeService;
            }
        }

        public IUnderlightService Underlights {
            get {
                return _underlightService;
            }
        }

        public IVesaSizeService VesaSizes {
            get {
                return _vesaSizeService;
            }
        }

        public IVideoPortService VideoPorts {
            get {
                return _videoPortService;
            }
        }

        public ICertificate80PlusService Certificates80Plus {
            get {
                return _certificate80PlusService;
            }
        }

        public IDPIModeService DPIModes {
            get {
                return _dpiModeService;
            }
        }
    }
}