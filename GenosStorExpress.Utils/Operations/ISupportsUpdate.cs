namespace GenosStorExpress.Utils.Operations {
	public interface ISupportsUpdate<T> where T: class {
		void Update(T item);
	}
}