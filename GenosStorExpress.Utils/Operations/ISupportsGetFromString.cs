namespace GenosStorExpress.Utils.Operations {
    public interface ISupportsGetFromString<T> where T : class {
        T GetFromString(string value);
    }
}