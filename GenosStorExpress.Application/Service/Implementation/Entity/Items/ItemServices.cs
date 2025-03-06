using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Application.Service.Interface.Entity.Items.SimpleComputerComponents;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items {
    public class ItemServices: IItemServices {
        private readonly IComputerComponentServices _computerComponentServices;
        private readonly ICharacteristicsService _characteristicsService;
        private readonly ISimpleComputerComponentService _simpleComputerComponentService;
        private readonly IPreparedAssemblyService _preparedAssemblyService;
        private readonly IItemTypeService _itemTypeService;
        private readonly IAllItemsService _allItemsService;

        public IComputerComponentServices ComputerComponents {
            get {
                return _computerComponentServices;
            }
        }

        public ICharacteristicsService Characteristics {
            get {
                return _characteristicsService;
            }
        }

        public ISimpleComputerComponentService SimpleComputerComponents {
            get {
                return _simpleComputerComponentService;
            }
        }

        public IPreparedAssemblyService PreparedAssemblies {
            get {
                return _preparedAssemblyService;
            }
        }

        public IItemTypeService ItemTypes {
            get {
                return _itemTypeService;
            }
        }

        public IAllItemsService All {
            get {
                return _allItemsService;
            }
        }

        public ItemServices(
            IComputerComponentServices computerComponentServices,
            ICharacteristicsService characteristicsService,
            ISimpleComputerComponentService simpleComputerComponentService,
            IPreparedAssemblyService preparedAssemblyService,
            IItemTypeService itemTypeService,
            IAllItemsService allItemsService
            ) {
            _computerComponentServices = computerComponentServices;
            _characteristicsService = characteristicsService;
            _simpleComputerComponentService = simpleComputerComponentService;
            _preparedAssemblyService = preparedAssemblyService;
            _itemTypeService = itemTypeService;
            _allItemsService = allItemsService;
        }
        
    }
}