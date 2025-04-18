using GenosStorExpress.Application.Wrappers.Entity.Base;

namespace GenosStorExpress.Application.Wrappers.Entity.Orders;

/// <summary>
/// Класс-обёртка пагинированного содержимого корзины
/// </summary>
public class PaginatedCartWrapper: ListPageWrapper<CartItemWrapper>;