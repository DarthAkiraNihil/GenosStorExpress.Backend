using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.Characteristics {
    /// <summary>
    /// Реализация сервиса, объединяющего все сервисы характеристик
    /// </summary>
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
        
        /// <summary>
        /// Стандартный конструктор
        /// </summary>
        /// <param name="cpusocketService">Сервис сокетов процессоров</param>
        /// <param name="motherboardFormFactorService">Сервис форм-факторов материнских плат</param>
        /// <param name="ramTypeService">Сервис типов ОЗУ</param>
        /// <param name="vendorService">Сервис производителей</param>
        /// <param name="computerCaseTypesizeService">Сервис типоразмеров компьютерных корпусов</param>
        /// <param name="coolerMaterialService">Сервис материалов для кулера</param>
        /// <param name="definitionService">Сервис разрешений мониторов</param>
        /// <param name="keyboardTypeService">Сервис типов клавиатур</param>
        /// <param name="keyboardTypesizeService">Сервис типоразмеров клавиатур</param>
        /// <param name="matrixTypeService">Сервис типов матриц</param>
        /// <param name="underlightService">Сервис подсветок мониторов</param>
        /// <param name="vesaSizeService">Сервис размеров Vesa</param>
        /// <param name="videoPortService">Сервис видеопортов</param>
        /// <param name="certificate80PlusService">Сервис сертификатов 80Plus</param>
        /// <param name="dpiModeService">Сервис режимов работы DPI</param>

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

        /// <summary>
        /// Сервис сокетов процессоров
        /// </summary>
        public ICPUSocketService CPUSockets {
            get {
                return _cpusocketService;
            }
        }

        /// <summary>
        /// Сервис форм-факторов материнских плат
        /// </summary>
        public IMotherboardFormFactorService MotherboardFormFactors {
            get {
                return _motherboardFormFactorService;
            }
        }

        /// <summary>
        /// Сервис подсветок мониторов
        /// </summary>
        public IRAMTypeService RAMTypes {
            get {
                return _ramTypeService;
            }
        }

        /// <summary>
        /// Сервис размеров Vesa
        /// </summary>
        public IVendorService Vendors {
            get {
                return _vendorService;
            }
        }

        /// <summary>
        /// Сервис типоразмеров компьютерных корпусов
        /// </summary>
        public IComputerCaseTypesizeService ComputerCaseTypesizes {
            get {
                return _computerCaseTypesizeService;
            }
        }

        /// <summary>
        /// Сервис материалов для кулера
        /// </summary>
        public ICoolerMaterialService CoolerMaterials {
            get {
                return _coolerMaterialService;
            }
        }

        /// <summary>
        /// Сервис разрешений мониторов
        /// </summary>
        public IDefinitionService Definitions {
            get {
                return _definitionService;
            }
        }

        /// <summary>
        /// Сервис типов клавиатур
        /// </summary>
        public IKeyboardTypeService KeyboardTypes {
            get {
                return _keyboardTypeService;
            }
        }

        /// <summary>
        /// Сервис типоразмеров клавиатур
        /// </summary>
        public IKeyboardTypesizeService KeyboardTypesizes {
            get {
                return _keyboardTypesizeService;
            }
        }

        /// <summary>
        /// Сервис типов матриц
        /// </summary>
        public IMatrixTypeService MatrixTypes {
            get {
                return _matrixTypeService;
            }
        }

        /// <summary>
        /// Сервис подсветок мониторов
        /// </summary>
        public IUnderlightService Underlights {
            get {
                return _underlightService;
            }
        }

        /// <summary>
        /// Сервис размеров Vesa
        /// </summary>
        public IVesaSizeService VesaSizes {
            get {
                return _vesaSizeService;
            }
        }
        
        /// <summary>
        /// Сервис видеопортов
        /// </summary>
        public IVideoPortService VideoPorts {
            get {
                return _videoPortService;
            }
        }

        /// <summary>
        /// Сервис сертификатов 80Plus
        /// </summary>
        public ICertificate80PlusService Certificates80Plus {
            get {
                return _certificate80PlusService;
            }
        }

        /// <summary>
        /// Сервис режимов DPI
        /// </summary>
        public IDPIModeService DPIModes {
            get {
                return _dpiModeService;
            }
        }
    }
}