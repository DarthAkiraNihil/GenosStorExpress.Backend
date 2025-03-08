namespace GenosStorExpress.Utils.Operations;

public interface ISupportsGetRaw<T> where T : class {
    T GetRaw(int id);
}