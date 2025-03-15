using GenosStorExpress.Application.Service.Interface.Base;
using GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;
using GenosStorExpress.Utils.Operations;

namespace GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents {
    public interface IComputerCaseService:
        IStandardService<ComputerCaseWrapper>,
        ISupportsFilter<ComputerCaseWrapper> {
    }
}