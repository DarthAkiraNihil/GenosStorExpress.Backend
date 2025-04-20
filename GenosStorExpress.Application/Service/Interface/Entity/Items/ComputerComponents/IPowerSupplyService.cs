using GenosStorExpress.Application.Service.Interface.Base;
using GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;
using GenosStorExpress.Application.Wrappers.Filters;
using GenosStorExpress.Utils.Operations;

namespace GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents {
    /// <summary>
    /// Интерфейс сервиса блоков питания
    /// </summary>
    public interface IPowerSupplyService:
        IStandardService<PowerSupplyWrapper>,
        ISupportsFilter<PowerSupplyWrapper, FilterContainerWrapper, FilterDescription> {
		
    }
}