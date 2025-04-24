using GenosStorExpress.Application.Wrappers.Entity.Base;

namespace GenosStorExpress.Application.Wrappers.Entity.Orders;

/// <summary>
/// Класс-обёртка пагинированного списка банковских карт
/// </summary>
public class PaginatedBankCardWrapper : ListPageWrapper<BankCardWrapper>;