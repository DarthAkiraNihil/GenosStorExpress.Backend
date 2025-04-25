using GenosStorExpress.Application.Service.Interface.Common;

namespace GenosStorExpress.Application.Service.Implementation.Common {
    public class CommonServices: ICommonServices {
	    
        private readonly IPaymentService _paymentService;
        private readonly IReportService _reportService;
        private readonly IDashboardService _dashboardService;
        
        public IPaymentService Payment {
            get {
                return _paymentService;
            }
        }

        public IReportService Reports {
            get {
                return _reportService;
            }
        }

        public IDashboardService Dashboard {
            get {
                return _dashboardService;
            }
        }

        public CommonServices(
	        IPaymentService paymentService,
            IReportService reportService,
	        IDashboardService dashboardService
            ) {
	        _paymentService = paymentService;
            _reportService = reportService;
            _dashboardService = dashboardService;
        }
    }
}