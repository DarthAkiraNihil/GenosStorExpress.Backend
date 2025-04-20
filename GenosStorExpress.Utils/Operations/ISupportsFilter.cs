namespace GenosStorExpress.Utils.Operations {
    public interface ISupportsFilter<T, F, D>
        where T : class
        where F : class
        where D : class {
        IList<T> Filter(F filters);
        IList<D> FilterData();
    }
}