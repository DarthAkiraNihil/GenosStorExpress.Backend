using GenosStorExpress.Application.Service.Interface.Base;
using GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;
using GenosStorExpress.Application.Wrappers.Filters;
using GenosStorExpress.Utils.Operations;

namespace GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents {
    /// <summary>
    /// Интерфейс сервиса ОЗУ
    /// </summary>
    public interface IRAMService:
        IStandardService<RAMWrapper>,
        ISupportsFilter<RAMWrapper, FilterContainerWrapper, FilterDescription> {
		
    }
}