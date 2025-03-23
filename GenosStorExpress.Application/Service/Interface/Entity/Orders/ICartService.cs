using GenosStorExpress.Application.Wrappers.Entity.Orders;

namespace GenosStorExpress.Application.Service.Interface.Entity.Orders {
    public interface ICartService {
        void AddToCart(int itemId, string customerId);
        void RemoveFromCart(int itemId, string customerId);
        void IncrementCartItemQuantity(int itemId, string customerId);
        void DecrementCartItemQuantity(int itemId, string customerId);
        bool IsInCart(int itemId, string customerId);
        void ClearCart(string customerId);
        CartWrapper GetCart(string customerId);
        
    }
}