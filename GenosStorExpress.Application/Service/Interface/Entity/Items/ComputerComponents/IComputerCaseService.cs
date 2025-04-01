using GenosStorExpress.Application.Service.Interface.Base;
using GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;
using GenosStorExpress.Application.Wrappers.Filters;
using GenosStorExpress.Utils.Operations;

namespace GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents {
    /// <summary>
    /// Интерфейс сервиса компьютерных корпусов
    /// </summary>
    public interface IComputerCaseService:
        IStandardService<ComputerCaseWrapper>,
        ISupportsFilter<ComputerCaseWrapper, FilterContainerWrapper> {
    }
}