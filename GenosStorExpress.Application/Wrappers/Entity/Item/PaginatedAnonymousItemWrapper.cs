using GenosStorExpress.Application.Wrappers.Entity.Base;

namespace GenosStorExpress.Application.Wrappers.Entity.Item;

/// <summary>
/// Класс-обёртка для страницы пагинированного списка товаров
/// </summary>
public class PaginatedAnonymousItemWrapper: ListPageWrapper<AnonymousItemWrapper>;