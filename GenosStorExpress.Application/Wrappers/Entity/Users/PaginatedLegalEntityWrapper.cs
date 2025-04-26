using GenosStorExpress.Application.Wrappers.Entity.Base;

namespace GenosStorExpress.Application.Wrappers.Entity.Users;

/// <summary>
/// Класс-обёртки пагинированнаго списка юридических лиц
/// </summary>
public class PaginatedLegalEntityWrapper: ListPageWrapper<LegalEntityWrapper>;
