using GenosStorExpress.Application.Service.Interface.Common;
using GenosStorExpress.Application.Service.Interface.Entity;

namespace GenosStorExpress.Application.Service.Interface {
    public interface IServices {
        IEntityServices Entity { get; }
        ICommonServices Common { get; }
    }
}