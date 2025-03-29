using GenosStorExpress.Application.Wrappers.Filters;

namespace GenosStorExpress.Application.Service.Interface.Common;

/// <summary>
/// Интерфейс сервиса трансформера фильтров в контейнер фильтров
/// </summary>
public interface IFilterTransformerService {
    /// <summary>
    /// Трансформация аморфного словаря фильтров в структурированный контейнер фильтров
    /// </summary>
    /// <param name="filters">Аморфный словарь фильтров</param>
    /// <returns>Контейнер фильтров</returns>
    FilterContainerWrapper TransformFilters(IDictionary<string, dynamic> filters);
}