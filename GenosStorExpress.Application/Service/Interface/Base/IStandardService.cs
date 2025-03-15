using GenosStorExpress.Utils.Operations;

namespace GenosStorExpress.Application.Service.Interface.Base {
    public interface IStandardService<T>:
        ISupportsGet<T>,
        ISupportsCreate<T>,
        ISupportsList<T>,
        ISupportsUpdateWrapped<T>,
        ISupportsDelete,
        ISupportsSave where T : class {
    }
}