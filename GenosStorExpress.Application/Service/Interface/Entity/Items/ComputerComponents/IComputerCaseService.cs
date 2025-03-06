using GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;
using GenosStorExpress.Utils.Operations;

namespace GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents {
    public interface IComputerCaseService:
        ISupportsCreate<ComputerCaseWrapper>,
        ISupportsGet<ComputerCaseWrapper>,
        ISupportsList<ComputerCaseWrapper>,
        ISupportsUpdateWrapped<ComputerCaseWrapper>,
        ISupportsDelete,
        ISupportsSave,
        ISupportsFilter<ComputerCaseWrapper> {
    }
}