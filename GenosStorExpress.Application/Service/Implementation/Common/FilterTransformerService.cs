using GenosStorExpress.Application.Service.Interface.Common;
using GenosStorExpress.Application.Wrappers.Filters;

namespace GenosStorExpress.Application.Service.Implementation.Common;

public class FilterTransformerService: IFilterTransformerService {
    public FilterContainerWrapper TransformFilters(IDictionary<string, dynamic> filters) {
        
        var container = new FilterContainerWrapper();
        
        foreach(KeyValuePair<string, dynamic> entry in filters) {
            RangeFilterWrapper? range = _getRangeFilter(entry.Value);
            if (range != null) {
                container.Ranges.Add(entry.Key, range);
                continue;
            }
            
        }
        
        throw new NotImplementedException();
    }

    private RangeFilterWrapper? _getRangeFilter(dynamic filter) {
        try {
            return new RangeFilterWrapper {
                From = filter.GetProperty("from").GetInt32(),
                To = filter.GetProperty("to").GetInt32()
            };
        } catch (Exception e) {
            return null;
        }
    }
}