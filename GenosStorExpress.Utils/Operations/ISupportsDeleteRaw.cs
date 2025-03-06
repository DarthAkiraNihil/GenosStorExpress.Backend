namespace GenosStorExpress.Utils.Operations {
    public interface ISupportsDeleteRaw<T> {
        void DeleteRaw(T item);
    }
}