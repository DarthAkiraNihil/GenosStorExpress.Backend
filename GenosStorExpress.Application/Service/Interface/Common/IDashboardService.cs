using GenosStorExpress.Application.Wrappers.Special;
using GenosStorExpress.Domain.Entity.User;

namespace GenosStorExpress.Application.Service.Interface.Common {
    public interface IDashboardService {
        DashboardInfo GetDashboardInfo(Administrator sudo);
    }
}