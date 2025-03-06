using GenosStorExpress.Application.Wrappers.Special;
using GenosStorExpress.Application.Service.Interface.Common;
using GenosStorExpress.Application.Service.Interface.Entity.Orders;
using GenosStorExpress.Application.Service.Interface.Entity.Users;
using GenosStorExpress.Domain.Entity.User;

namespace GenosStorExpress.Application.Service.Implementation.Common {
    public class DashboardService: IDashboardService {
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        private readonly IPaymentService _paymentService;

        public DashboardService(IUserService userService, IOrderService orderService, IPaymentService paymentService) {
            _userService = userService;
            _orderService = orderService;
            _paymentService = paymentService;
        }

        public DashboardInfo GetDashboardInfo(Administrator sudo) {
            var dashboardInfo = new DashboardInfo();
            
            dashboardInfo.LoggedAdmin = sudo.Email;

            var users = _userService.List();
            
            dashboardInfo.RegisteredUsers = users.Count;
            dashboardInfo.RegisteredIndividualEntities = users.Where(
                u => u.UserType == UserType.IndividualEntity
                ).ToList().Count;
            
            dashboardInfo.RegisteredLegalVerifiedEntities = users.Where(
                u => {
                    if (u.UserType != UserType.LegalEntity) return false;

                    return (u as LegalEntity).IsVerified;
                }).ToList().Count;
            
            dashboardInfo.LegalEntitiesWaitingForVerification = users.Where(
                u => {
                    if (u.UserType != UserType.LegalEntity) return false;

                    return !(u as LegalEntity).IsVerified;
                }).ToList().Count;

            dashboardInfo.ActiveOrdersCount = _orderService.GetActiveOrders().Count;
            dashboardInfo.LastOrder = GetLastOrderDashboardInfo();
            return dashboardInfo;
        }

        private LastOrderDashboardInfo GetLastOrderDashboardInfo() {
            var collected = new LastOrderDashboardInfo();

            var list = _orderService.List();
            list.Sort((x, y) => x.CreatedAt < y.CreatedAt ? -1 : 1);
            var last = list.Last();
            
            collected.Id = last.Id;
            collected.CreatedAt = last.CreatedAt.ToString("dd/MM/yyyy HH:mm");
            collected.Total = _orderService.CalculateTotal(last);
            collected.ItemsCount = last.Items.Count;
            collected.Orderer = _paymentService.GetOrdererInfo(last.Customer);
            
            return collected;
        }
    }
}