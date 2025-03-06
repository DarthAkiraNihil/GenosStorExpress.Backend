using GenosStorExpress.Domain.Interface.Item.Characteristic;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface.Item.SimpleComputerComponent;

namespace GenosStorExpress.Domain.Interface.Item {
    public interface IItemRepository {
		
        ICharacteristicRepository Characteristics { get; }
        IComputerComponentRepository ComputerComponents { get; }
        ISimpleComputerComponentRepository SimpleComputerComponents{ get; }
        IItemTypeRepository ItemTypes { get; }
        IPreparedAssemblyRepository PreparedAssemblies { get; }
        IAllItemsRepository All { get; }
        
    }
}