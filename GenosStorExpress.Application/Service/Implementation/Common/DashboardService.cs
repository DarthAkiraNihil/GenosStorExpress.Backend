using GenosStorExpress.Application.Service.Interface.Common;
using GenosStorExpress.Application.Service.Interface.Entity.Orders;
using GenosStorExpress.Application.Service.Interface.Entity.Users;
using GenosStorExpress.Application.Wrappers.Dashboard;
using GenosStorExpress.Domain.Entity.User;

namespace GenosStorExpress.Application.Service.Implementation.Common {
    /// <summary>
    /// Реализация сервиса дэшборда
    /// </summary>
    public class DashboardService: IDashboardService {
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        private readonly IPaymentService _paymentService;

        /// <summary>
        /// Стандартный конструктор
        /// </summary>
        /// <param name="userService">Сервис пользователей</param>
        /// <param name="orderService">Сервис заказов</param>
        /// <param name="paymentService">Сервис оплаты</param>
        public DashboardService(IUserService userService, IOrderService orderService, IPaymentService paymentService) {
            _userService = userService;
            _orderService = orderService;
            _paymentService = paymentService;
        }

        /// <summary>
        /// Получение информации дэшборда. Только под администратором
        /// </summary>
        /// <param name="sudoId">Номер администратора</param>
        /// <returns>Информацию дэшборда</returns>
        /// <exception cref="NullReferenceException">Если метод вызывается не администратором</exception>
        public DashboardInfoWrapper GetDashboardInfo(string sudoId) {
            
            Administrator? sudo = _userService.GetAdmin(sudoId);

            if (sudo == null) {
                throw new NullReferenceException("Запрещено! Данный метод может быть вызван только администратором");
            }
            
            var dashboardInfo = new DashboardInfoWrapper();
            
            dashboardInfo.LoggedAdmin = sudo.Email!;

            var users = _userService.List();
            
            dashboardInfo.RegisteredUsers = users.Count;
            dashboardInfo.RegisteredIndividualEntities = users.Where(
                u => u.UserType == UserType.IndividualEntity
                ).ToList().Count;
            
            dashboardInfo.RegisteredLegalVerifiedEntities = users.Where(
                u => {
                    if (u.UserType != UserType.LegalEntity) return false;

                    return (u as LegalEntity)!.IsVerified;
                }).ToList().Count;
            
            dashboardInfo.LegalEntitiesWaitingForVerification = users.Where(
                u => {
                    if (u.UserType != UserType.LegalEntity) return false;

                    return !(u as LegalEntity)!.IsVerified;
                }).ToList().Count;

            //dashboardInfo.ActiveOrdersCount = _orderService.GetActiveOrders().Count;
            dashboardInfo.LastOrder = _getLastOrderDashboardInfo(sudoId);
            return dashboardInfo;
        }

        private DashboardOrderWrapper _getLastOrderDashboardInfo(string sudoId) {
            var collected = new DashboardOrderWrapper();

            var list = _orderService.GetActiveOrdersRaw(sudoId);
            list.Sort((x, y) => x.CreatedAt < y.CreatedAt ? -1 : 1);
            var last = list.Last();
            
            collected.Id = last.Id;
            collected.CreatedAt = last.CreatedAt.ToString("dd/MM/yyyy HH:mm");
            collected.Total = _orderService.CalculateTotal((int) last.Id);
            collected.ItemsCount = last.Items.Count;
            collected.Orderer = _paymentService.GetOrdererInfo(last.CustomerId);
            
            return collected;
        }
    }
}