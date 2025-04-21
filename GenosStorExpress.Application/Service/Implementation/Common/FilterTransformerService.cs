using System.Text.Json;
using GenosStorExpress.Application.Service.Interface.Common;
using GenosStorExpress.Application.Wrappers.Filters;

namespace GenosStorExpress.Application.Service.Implementation.Common;

/// <summary>
/// Интерфейс сервиса трансформера фильтров в контейнер фильтров
/// </summary>
public class FilterTransformerService: IFilterTransformerService {
    /// <summary>
    /// Трансформация аморфного словаря фильтров в структурированный контейнер фильтров
    /// </summary>
    /// <param name="filters">Аморфный словарь фильтров</param>
    /// <returns>Контейнер фильтров</returns>
    /// <exception cref="ArgumentException">Если какой-то из фильтров не соответствует одному из ожидаемых форматов</exception>
    public FilterContainerWrapper TransformFilters(IDictionary<string, dynamic> filters) {
        
        var container = new FilterContainerWrapper();
        
        foreach(KeyValuePair<string, dynamic> entry in filters) {
            if (entry.Key.Equals("name")) {
                container.Name = entry.Value.GetString();
                continue;
            }
            
            RangeFilterWrapper? range = _getRangeFilter(entry.Value);
            if (range != null) {
                container.Ranges.Add(entry.Key, range);
                continue;
            }
            
            ChoiceFilterWrapper? choice = _getChoiceFilter(entry.Value);
            if (choice != null) {
                container.Choices.Add(entry.Key, choice);
                continue;
            }
            
            bool? having = _getBooleanFilter(entry.Value);
            if (having != null) {
                container.Havings.Add(entry.Key, (bool) having);
                continue;
            }
            
            throw new ArgumentException($"Невозможно прочитать содержимое фильтра {entry.Key}");
        }
        
        return container;
    }

    private RangeFilterWrapper? _getRangeFilter(dynamic filter) {
        try {
            return new RangeFilterWrapper {
                From = filter.GetProperty("from").GetInt32(),
                To = filter.GetProperty("to").GetInt32()
            };
        } catch (Exception) {
            return null;
        }
    }

    private ChoiceFilterWrapper? _getChoiceFilter(dynamic filter) {
        try {
            return new ChoiceFilterWrapper {
                Selected = ((JsonElement) filter.GetProperty("selected")).EnumerateArray().Select(x => x.GetString()!).ToList()
            };
        } catch (Exception) {
            return null;
        }
    }

    private bool? _getBooleanFilter(dynamic filter) {
        try {
            return filter.GetBoolean();
        } catch (Exception) {
            return null;
        }
    }
}