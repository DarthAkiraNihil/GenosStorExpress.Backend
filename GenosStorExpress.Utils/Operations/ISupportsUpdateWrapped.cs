namespace GenosStorExpress.Utils.Operations;

public interface ISupportsUpdateWrapped<T> {
    void Update(int id, T wrapped);
}