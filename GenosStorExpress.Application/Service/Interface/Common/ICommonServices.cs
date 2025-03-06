namespace GenosStorExpress.Application.Service.Interface.Common {
    public interface ICommonServices {
	    IPaymentService Payment { get; }
        // IReportService Reports { get; }
        IDashboardService Dashboard { get; }
    }
}