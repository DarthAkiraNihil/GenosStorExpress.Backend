namespace GenosStorExpress.Utils.Operations;

public interface ISupportsGetByStringId<T> where T : class {
    T? Get(string id);
}