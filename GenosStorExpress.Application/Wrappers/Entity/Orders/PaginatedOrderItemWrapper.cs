using GenosStorExpress.Application.Wrappers.Entity.Base;

namespace GenosStorExpress.Application.Wrappers.Entity.Orders;

/// <summary>
/// Класс-обёртка пагинированного списка товаров в заказе
/// </summary>
public class PaginatedOrderItemWrapper: ListPageWrapper<OrderItemWrapper>;