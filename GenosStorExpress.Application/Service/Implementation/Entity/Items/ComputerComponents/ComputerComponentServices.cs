using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.ComputerComponents {
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

        public IMotherboardService Motherboards {
            get {
                return _motherboardService;
            }
        }
        
        public IComputerCaseService ComputerCases { get 
            {
                return _computerCasesService;
            }
        }
        public ICPUCoolerService CPUCoolers { get 
            {
                return _cpuCoolersService;
            }
        }
        public ICPUService CPUs { get 
            {
                return _cpusService;
            }
        }
        public IDisplayService Displays { get 
            {
                return _displaysService;
            }
        }
        public IGraphicsCardService GraphicsCards { get 
            {
                return _graphicsCardsService;
            }
        }
        public IHDDService HDDs { get 
            {
                return _hddsService;
            }
        }
        public IKeyboardService Keyboards { get 
            {
                return _keyboardsService;
            }
        }
        public IMouseService Mouses { get 
            {
                return _mousesService;
            }
        }
        public INVMeSSDService NVMeSSDs { get 
            {
                return _nvmeSSDsService;
            }
        }
        public IPowerSupplyService PowerSupplies { get 
            {
                return _powerSuppliesService;
            }
        }
        public IRAMService RAMs { get 
            {
                return _ramsService;
            }
        }
        public ISataSSDService SataSSDs { get 
            {
                return _sataSSDsService;
            }
        }
        
        

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