using GenosStorExpress.Utils.Operations;

namespace GenosStorExpress.Application.Service.Interface.Base {
    public interface IFilterService<T>: ISupportsFilter<T> where T : class {
        
    }
}