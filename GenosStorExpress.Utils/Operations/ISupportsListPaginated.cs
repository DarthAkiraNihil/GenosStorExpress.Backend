namespace GenosStorExpress.Utils.Operations;

public interface ISupportsListPaginated<T> where T : class {
    List<T> List(int pageNumber, int pageSize);
}