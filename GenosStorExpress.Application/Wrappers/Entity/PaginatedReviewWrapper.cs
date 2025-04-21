using GenosStorExpress.Application.Wrappers.Entity.Base;
using GenosStorExpress.Application.Wrappers.Entity.Item;

namespace GenosStorExpress.Application.Wrappers.Entity;

/// <summary>
/// Класс-обёртка пагинированнаго списка отзывов
/// </summary>
public class PaginatedReviewWrapper: ListPageWrapper<ReviewWrapper>;
