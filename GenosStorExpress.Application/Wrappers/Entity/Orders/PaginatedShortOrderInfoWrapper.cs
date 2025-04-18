using GenosStorExpress.Application.Wrappers.Entity.Base;

namespace GenosStorExpress.Application.Wrappers.Entity.Orders;

/// <summary>
/// Класс-обёртка пагинированного списка краткой информации по заказам
/// </summary>
public class PaginatedShortOrderInfoWrapper: ListPageWrapper<ShortOrderWrapper>;
