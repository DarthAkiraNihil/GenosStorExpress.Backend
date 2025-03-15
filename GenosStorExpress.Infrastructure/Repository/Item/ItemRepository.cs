using GenosStorExpress.Domain.Interface.Item;
using GenosStorExpress.Domain.Interface.Item.Characteristic;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface.Item.SimpleComputerComponent;
using GenosStorExpress.Infrastructure.Context;
using GenosStorExpress.Infrastructure.Repository.Item.Characteristic;
using GenosStorExpress.Infrastructure.Repository.Item.ComputerComponent;
using GenosStorExpress.Infrastructure.Repository.Item.SimpleComputerComponent;

namespace GenosStorExpress.Infrastructure.Repository.Item {
    public class ItemRepository: IItemRepository {

        private GenosStorExpressDatabaseContext _context;

        private CharacteristicRepository? _characteristics;
        private ComputerComponentRepository? _computerComponents;
        private SimpleComputerComponentRepository? _simpleComputerComponents;
        private ItemTypeRepository? _itemTypes;
        private AllItemsRepository? _allItems;
        private PreparedAssemblyRepository? _preparedAssemblies;
        
        public ItemRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }
        
        public ICharacteristicRepository Characteristics {
            get {
                if (_characteristics == null) {
                    _characteristics = new CharacteristicRepository(_context);
                }
                return _characteristics;
            }
        }
        
        public IComputerComponentRepository ComputerComponents {
            get {
                if (_computerComponents == null) {
                    _computerComponents = new ComputerComponentRepository(_context);
                }
                return _computerComponents;
            }
        }
        
        public ISimpleComputerComponentRepository SimpleComputerComponents {
            get {
                if (_simpleComputerComponents == null) {
                    _simpleComputerComponents = new SimpleComputerComponentRepository(_context);
                }
                return _simpleComputerComponents;
            }
        }

        public IItemTypeRepository ItemTypes {
            get {
                if (_itemTypes == null) {
                    _itemTypes = new ItemTypeRepository(_context);
                }
                return _itemTypes;
            }
        }
        public IPreparedAssemblyRepository PreparedAssemblies {
            get {
                if (_preparedAssemblies == null) {
                    _preparedAssemblies = new PreparedAssemblyRepository(_context);
                }
                return _preparedAssemblies;
            }
        }

        public IAllItemsRepository All {
            get {
                if (_allItems == null) {
                    _allItems = new AllItemsRepository(_context);
                }
                return _allItems;
            }
        }

    }
}