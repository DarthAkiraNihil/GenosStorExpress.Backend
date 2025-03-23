using GenosStorExpress.Application.Wrappers.Dashboard;

namespace GenosStorExpress.Application.Service.Interface.Common {
    public interface IDashboardService {
        DashboardInfoWrapper GetDashboardInfo(string sudoId);
    }
}