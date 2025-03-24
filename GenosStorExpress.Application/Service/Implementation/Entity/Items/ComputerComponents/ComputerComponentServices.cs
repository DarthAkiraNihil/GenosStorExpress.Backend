using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.ComputerComponents {
    /// <summary>
    /// Реализация сервиса, объединяющего все сервисы компьютерных комплектующих
    /// </summary>
    public class ComputerComponentServices: IComputerComponentServices {
        
        private IMotherboardService _motherboardService;
        private IComputerCaseService _computerCasesService;
        private ICPUCoolerService _cpuCoolersService;
        private ICPUService _cpusService;
        private IDisplayService _displaysService;
        private IGraphicsCardService _graphicsCardsService;
        private IHDDService _hddsService;
        private IKeyboardService _keyboardsService;
        private IMouseService _mousesService;
        private INVMeSSDService _nvmeSSDsService;
        private IPowerSupplyService _powerSuppliesService;
        private IRAMService _ramsService;
        private ISataSSDService _sataSSDsService;
        
        /// <summary>
        /// Сервис материнских плат
        /// </summary>
        public IMotherboardService Motherboards {
            get {
                return _motherboardService;
            }
        }
        
        /// <summary>
        /// Сервис компьютерных корпусов
        /// </summary>
        public IComputerCaseService ComputerCases { get 
            {
                return _computerCasesService;
            }
        }
        
        /// <summary>
        /// Сервис кулеров для процессора
        /// </summary>
        public ICPUCoolerService CPUCoolers { get 
            {
                return _cpuCoolersService;
            }
        }
        
        /// <summary>
        /// Сервис центральных процессоров
        /// </summary>
        public ICPUService CPUs { get 
            {
                return _cpusService;
            }
        }
        
        //IDiskDriveService DiskDrives { get; }
        /// <summary>
        /// Сервис мониторов
        /// </summary>
        public IDisplayService Displays { get 
            {
                return _displaysService;
            }
        }
        
        /// <summary>
        /// Сервис видеокарт
        /// </summary>
        public IGraphicsCardService GraphicsCards { get 
            {
                return _graphicsCardsService;
            }
        }
        
        /// <summary>
        /// Сервис жёстких дисков
        /// </summary>
        public IHDDService HDDs { get 
            {
                return _hddsService;
            }
        }
        
        /// <summary>
        /// Сервис клавиатур
        /// </summary>
        public IKeyboardService Keyboards { get 
            {
                return _keyboardsService;
            }
        }
        
        /// <summary>
        /// Сервис мышей
        /// </summary>
        public IMouseService Mouses { get 
            {
                return _mousesService;
            }
        }
        
        /// <summary>
        /// Сервис твердотельных накопителей NVMe
        /// </summary>
        public INVMeSSDService NVMeSSDs { get 
            {
                return _nvmeSSDsService;
            }
        }
        
        /// <summary>
        /// Сервис блоков питания
        /// </summary>
        public IPowerSupplyService PowerSupplies { get 
            {
                return _powerSuppliesService;
            }
        }
        
        /// <summary>
        /// Сервис ОЗУ
        /// </summary>
        public IRAMService RAMs { get 
            {
                return _ramsService;
            }
        }
        
        /// <summary>
        /// Сервис твердотельных накопителей Sata
        /// </summary>
        public ISataSSDService SataSSDs { get 
            {
                return _sataSSDsService;
            }
        }
        
        /// <summary>
        /// Стандартный конструктор
        /// </summary>
        /// <param name="motherboardService">Сервис мышей</param>
        /// <param name="computerCasesService">Сервис компьютерных корпусов</param>
        /// <param name="cpuCoolerService">Сервис кулеров для процессора</param>
        /// <param name="cpuService">Сервис центральных процессоров</param>
        /// <param name="displaysService">Сервис мониторов</param>
        /// <param name="graphicsCardsService">Сервис видеокарт</param>
        /// <param name="hddsService">Сервис жёстких дисков</param>
        /// <param name="keyboardsService">Сервис материнских плат</param>
        /// <param name="mouseService">Сервис мышей</param>
        /// <param name="nvmeSSDsService">Сервис твердотельных накопителей NVMe</param>
        /// <param name="powerSuppliesService">Сервис блоков питания</param>
        /// <param name="ramsService">Сервис ОЗУ</param>
        /// <param name="sataSSDsService">Сервис твердотельных накопителей Sata</param>
        public ComputerComponentServices(
            IMotherboardService motherboardService,
            IComputerCaseService computerCasesService,
            ICPUCoolerService cpuCoolerService,
            ICPUService cpuService,
            IDisplayService displaysService,
            IGraphicsCardService graphicsCardsService,
            IHDDService hddsService,
            IKeyboardService keyboardsService,
            IMouseService mouseService,
            INVMeSSDService nvmeSSDsService,
            IPowerSupplyService powerSuppliesService,
            IRAMService ramsService,
            ISataSSDService sataSSDsService
        ) {
            _motherboardService = motherboardService;
            _computerCasesService = computerCasesService;
            _cpuCoolersService = cpuCoolerService;
            _hddsService = hddsService;
            _keyboardsService = keyboardsService;
            _mousesService = mouseService;
            _nvmeSSDsService = nvmeSSDsService;
            _powerSuppliesService = powerSuppliesService;
            _ramsService = ramsService;
            _sataSSDsService = sataSSDsService;
            _displaysService = displaysService;
            _graphicsCardsService = graphicsCardsService;
            _cpusService = cpuService;
        }
    }
}