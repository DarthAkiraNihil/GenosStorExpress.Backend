namespace GenosStorExpress.Utils.Operations {
	public interface ISupportsGet<T> where T: class {
		T Get(int id);
	}
}