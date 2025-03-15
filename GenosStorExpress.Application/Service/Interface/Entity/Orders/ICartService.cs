using GenosStorExpress.Domain.Entity.Item;
using GenosStorExpress.Domain.Entity.User;

namespace GenosStorExpress.Application.Service.Interface.Entity.Orders {
    public interface ICartService {
        void AddToCart(Item item, Customer customer);
        void RemoveFromCart(Item item, Customer customer);
        void IncrementCartItemQuantity(Item item, Customer customer);
        void DecrementCartItemQuantity(Item item, Customer customer);
        bool IsInCart(Item item, Customer customer);
        void ClearCart(Customer customer);
        
    }
}