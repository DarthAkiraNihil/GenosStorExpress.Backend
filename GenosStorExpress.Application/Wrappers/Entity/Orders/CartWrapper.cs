namespace GenosStorExpress.Application.Wrappers.Entity.Orders;

/// <summary>
/// Класс обёртки корзины
/// </summary>
public class CartWrapper {
    /// <summary>
    /// Содержимое корзины
    /// </summary>
    public IList<CartItemWrapper> Items { get; set; }

    /// <summary>
    /// Стандартный конструктор
    /// </summary>
    public CartWrapper() {
        Items = new List<CartItemWrapper>();
    }
}