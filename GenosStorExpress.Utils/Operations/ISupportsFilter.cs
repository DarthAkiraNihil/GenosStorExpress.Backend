namespace GenosStorExpress.Utils.Operations {
    public interface ISupportsFilter<T, F>
        where T : class
        where F : class {
        IList<T> Filter(F filters);
    }
}