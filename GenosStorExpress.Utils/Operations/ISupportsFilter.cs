namespace GenosStorExpress.Utils.Operations {
    public interface ISupportsFilter<T> where T : class {
        List<T> Filter(List<Func<T, bool>> filters);
    }
}