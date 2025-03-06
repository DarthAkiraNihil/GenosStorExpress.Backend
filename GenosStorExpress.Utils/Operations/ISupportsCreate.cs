namespace GenosStorExpress.Utils.Operations {
	public interface ISupportsCreate<T> where T: class {
		void Create(T item);
	}
}