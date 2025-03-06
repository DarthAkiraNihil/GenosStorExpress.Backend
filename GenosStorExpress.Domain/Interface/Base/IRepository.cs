using GenosStorExpress.Utils.Operations;

namespace GenosStorExpress.Domain.Repository.Base {
    public interface IRepository<T>:
        ISupportsCreate<T>,
        ISupportsGet<T>,
        ISupportsList<T>,
        ISupportsUpdate<T>,
        ISupportsDelete where T: class {
        
    }
}