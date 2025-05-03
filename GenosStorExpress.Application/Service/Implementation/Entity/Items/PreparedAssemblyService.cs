using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Wrappers.Entity.Item.PreaparedAssembly;
using GenosStorExpress.Application.Wrappers.Filters;
using GenosStorExpress.Domain.Entity.Item;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;


namespace GenosStorExpress.Application.Service.Implementation.Entity.Items {
    public class PreparedAssemblyService: IPreparedAssemblyService {
        private readonly IGenosStorExpressRepositories _repositories;
        
        private readonly IPreparedAssemblyRepository _preparedAssemblies;
        private readonly IRAMRepository _rams;
        private readonly IDiskDriveRepository _diskDrives;
        private readonly ICPURepository _cpus;
        private readonly IMotherboardRepository _motherboards;
        private readonly IGraphicsCardRepository _graphicsCards;
        private readonly IPowerSupplyRepository _powerSupplies;
        private readonly IDisplayRepository _displays;
        private readonly IComputerCaseRepository _computerCases;
        private readonly IKeyboardRepository _keyboards;
        private readonly IMouseRepository _mouses;
        private readonly ICPUCoolerRepository _cpuCoolers;

        public PreparedAssemblyService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
            _rams = _repositories.Items.ComputerComponents.RAMs;
            _diskDrives = _repositories.Items.ComputerComponents.DiskDrives;
            _cpus = _repositories.Items.ComputerComponents.CPUs;
            _motherboards = _repositories.Items.ComputerComponents.Motherboards;
            _graphicsCards = _repositories.Items.ComputerComponents.GraphicsCards;
            _powerSupplies = _repositories.Items.ComputerComponents.PowerSupplies;
            _displays = _repositories.Items.ComputerComponents.Displays;
            _diskDrives = _repositories.Items.ComputerComponents.DiskDrives;
            _computerCases = _repositories.Items.ComputerComponents.ComputerCases;
            _keyboards = _repositories.Items.ComputerComponents.Keyboards;
            _mouses = _repositories.Items.ComputerComponents.Mouses;
            _cpuCoolers = _repositories.Items.ComputerComponents.CPUCoolers;
            _preparedAssemblies = _repositories.Items.PreparedAssemblies;
        }
        
        public void Create(PreparedAssemblyWrapper item) {
            
            var cpu = _cpus.Get(item.CPU.Id);
            var motherboard = _motherboards.Get(item.Motherboard.Id);
            var graphicsCard = _graphicsCards.Get(item.GraphicsCard.Id);
            var powerSupply = _powerSupplies.Get(item.PowerSupply.Id);
            var display = item.Display == null ? null : _displays.Get(item.Display.Id);
            var computerCase = _computerCases.Get(item.ComputerCase.Id);
            var keyboard = item.Keyboard == null ? null : _keyboards.Get(item.Keyboard.Id);
            var mouse = item.Mouse == null ? null : _mouses.Get(item.Mouse.Id);
            var cpuCooler = _cpuCoolers.Get(item.CPUCooler.Id);

            if (cpu == null) {
                throw new NullReferenceException($"Процессора с номером {item.CPU.Id} не существует");
            }

            if (motherboard == null) {
                throw new NullReferenceException($"Материнской платы с номером {item.Motherboard.Id} не существует");
            }

            if (graphicsCard == null) {
                throw new NullReferenceException($"Видеокарты с номером {item.GraphicsCard.Id} не существует");
            }

            if (powerSupply == null) {
                throw new NullReferenceException($"Блока питания с номером {item.PowerSupply.Id} не существует");
            }
            
            if (computerCase == null) {
                throw new NullReferenceException($"Корпуса с номером {item.ComputerCase.Id} не существует");
            }

            if (cpuCooler == null) {
                throw new NullReferenceException($"Кулеря для процессора с номером {item.CPUCooler.Id} не существует");
            }
            
            var created = new PreparedAssembly {
                RAM = item.RAMs.Select(i => {
                    var ram = _rams.Get(i.Id);
                    if (ram == null) {
                        throw new NullReferenceException($"ОЗУ с номером {i.Id} не существует");
                    }

                    return ram;
                }).ToList(),
                Disks = item.DiskDrives.Select(i => {
                    var drive = _diskDrives.Get(i.Id);
                    if (drive == null) {
                        throw new NullReferenceException($"Диска с номером {i.Id} не существует");
                    }

                    return drive;
                }).ToList(),
                
                CPU = cpu,
                Motherboard = motherboard,
                GraphicsCard = graphicsCard,
                PowerSupply = powerSupply,
                Display = display,
                ComputerCase = computerCase,
                Keyboard = keyboard,
                Mouse = mouse,
                CPUCooler = cpuCooler
            };
            _preparedAssemblies.Create(created);
        }

        public PreparedAssemblyWrapper? Get(int id) {
            PreparedAssembly? obj = _preparedAssemblies.Get(id);

            if (obj == null) {
                return null;
            }
            
            return new PreparedAssemblyWrapper {
                RAMs = obj.RAM.Select(i => _wrap(i)).ToList(),
                DiskDrives = obj.Disks.Select(i => _wrap(i)).ToList(),
                CPU = _wrap(obj.CPU),
                Motherboard = _wrap(obj.Motherboard),
                GraphicsCard = _wrap(obj.GraphicsCard),
                PowerSupply = _wrap(obj.PowerSupply),
                Display = obj.Display == null ? null : _wrap(obj.Display),
                ComputerCase = _wrap(obj.ComputerCase),
                Keyboard = obj.Keyboard == null ? null : _wrap(obj.Keyboard),
                Mouse = obj.Mouse == null ? null : _wrap(obj.Mouse),
                CPUCooler = _wrap(obj.CPUCooler),
            };
        }

        public List<PreparedAssemblyWrapper> List() {
            return _preparedAssemblies.List().Select(obj => new PreparedAssemblyWrapper {
                RAMs = obj.RAM.Select(i => _wrap(i)).ToList(),
                DiskDrives = obj.Disks.Select(i => _wrap(i)).ToList(),
                CPU = _wrap(obj.CPU),
                Motherboard = _wrap(obj.Motherboard),
                GraphicsCard = _wrap(obj.GraphicsCard),
                PowerSupply = _wrap(obj.PowerSupply),
                Display = obj.Display == null ? null : _wrap(obj.Display),
                ComputerCase = _wrap(obj.ComputerCase),
                Keyboard = obj.Keyboard == null ? null : _wrap(obj.Keyboard),
                Mouse = obj.Mouse == null ? null : _wrap(obj.Mouse),
                CPUCooler = _wrap(obj.CPUCooler),
            }).ToList();
        }

        public void Update(int id, PreparedAssemblyWrapper item) {
            PreparedAssembly? obj = _preparedAssemblies.Get(id);
            if (obj == null) {
                throw new NullReferenceException($"Готовой сборки с номером {id} не существует");
            }
            
            var cpu = _cpus.Get(item.CPU.Id);
            var motherboard = _motherboards.Get(item.Motherboard.Id);
            var graphicsCard = _graphicsCards.Get(item.GraphicsCard.Id);
            var powerSupply = _powerSupplies.Get(item.PowerSupply.Id);
            var display = item.Display == null ? null : _displays.Get(item.Display.Id);
            var computerCase = _computerCases.Get(item.ComputerCase.Id);
            var keyboard = item.Keyboard == null ? null : _keyboards.Get(item.Keyboard.Id);
            var mouse = item.Mouse == null ? null : _mouses.Get(item.Mouse.Id);
            var cpuCooler = _cpuCoolers.Get(item.CPUCooler.Id);

            if (cpu == null) {
                throw new NullReferenceException($"Процессора с номером {item.CPU.Id} не существует");
            }

            if (motherboard == null) {
                throw new NullReferenceException($"Материнской платы с номером {item.Motherboard.Id} не существует");
            }

            if (graphicsCard == null) {
                throw new NullReferenceException($"Видеокарты с номером {item.GraphicsCard.Id} не существует");
            }

            if (powerSupply == null) {
                throw new NullReferenceException($"Блока питания с номером {item.PowerSupply.Id} не существует");
            }
            
            if (computerCase == null) {
                throw new NullReferenceException($"Корпуса с номером {item.ComputerCase.Id} не существует");
            }

            if (cpuCooler == null) {
                throw new NullReferenceException($"Кулеря для процессора с номером {item.CPUCooler.Id} не существует");
            }
            
            var created = new PreparedAssembly {
                RAM = item.RAMs.Select(i => {
                    var ram = _rams.Get(i.Id);
                    if (ram == null) {
                        throw new NullReferenceException($"ОЗУ с номером {i.Id} не существует");
                    }

                    return ram;
                }).ToList(),
                Disks = item.DiskDrives.Select(i => {
                    var drive = _diskDrives.Get(i.Id);
                    if (drive == null) {
                        throw new NullReferenceException($"Диска с номером {i.Id} не существует");
                    }

                    return drive;
                }).ToList(),
                
                CPU = cpu,
                Motherboard = motherboard,
                GraphicsCard = graphicsCard,
                PowerSupply = powerSupply,
                Display = display,
                ComputerCase = computerCase,
                Keyboard = keyboard,
                Mouse = mouse,
                CPUCooler = cpuCooler
            };
            _preparedAssemblies.Create(created);
            
            obj.RAM = item.RAMs.Select(i => {
                var ram = _rams.Get(i.Id);
                if (ram == null) {
                    throw new NullReferenceException($"ОЗУ с номером {i.Id} не существует");
                }

                return ram;
            }).ToList();
            obj.Disks = item.DiskDrives.Select(i => {
                var drive = _diskDrives.Get(i.Id);
                if (drive == null) {
                    throw new NullReferenceException($"Диска с номером {i.Id} не существует");
                }

                return drive;
            }).ToList()
                ;
            obj.CPU = cpu;
            obj.Motherboard = motherboard;
            obj.GraphicsCard = graphicsCard;
            obj.PowerSupply = powerSupply;
            obj.Display = display;
            obj.ComputerCase = computerCase;
            obj.Keyboard = keyboard;
            obj.Mouse = mouse;
            obj.CPUCooler = cpuCooler;
            
            _preparedAssemblies.Update(obj);
        }

        public void Delete(int id) {
            _repositories.Items.PreparedAssemblies.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        private PreparedAssemblyItemWrapper _wrap(Item wrapped) {
            return new PreparedAssemblyItemWrapper {
                Id = wrapped.Id, Model = wrapped.Model
            };
        }

        /// <summary>
        /// Фильтрация списка сущностей готовых сборок
        /// </summary>
        /// <param name="filters">Список фильтров</param>
        /// <returns>Отфильтрованный список обёрток готовых сборок</returns>
        public IList<PreparedAssemblyWrapper> Filter(FilterContainerWrapper filters) {
            var filters_ = new List<Func<PreparedAssemblyWrapper, bool>>();
            
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
            return new List<FilterDescription>();
        }

        private PreparedAssemblyDiskDriveWrapper _wrap(DiskDrive wrapped) {
            return new PreparedAssemblyDiskDriveWrapper() {
                Id = wrapped.Id, Model = wrapped.Model, Type = wrapped.ItemType.Name
            };
        }
    }
}