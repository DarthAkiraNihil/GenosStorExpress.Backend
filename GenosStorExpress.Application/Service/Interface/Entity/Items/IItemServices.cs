using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Application.Service.Interface.Entity.Items.SimpleComputerComponents;

namespace GenosStorExpress.Application.Service.Interface.Entity.Items {
    public interface IItemServices {
		ICharacteristicsService Characteristics { get; }
        IComputerComponentServices ComputerComponents { get; }
        ISimpleComputerComponentService SimpleComputerComponents { get; }
        IItemTypeService ItemTypes { get; }
        IPreparedAssemblyService PreparedAssemblies { get; }
        IAllItemsService All { get; }
    }
}