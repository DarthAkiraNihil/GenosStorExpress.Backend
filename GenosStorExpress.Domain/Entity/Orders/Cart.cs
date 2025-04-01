using GenosStorExpress.Domain.Entity.User;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.Orders {

	[Table("Carts")]
	public class Cart {
		public int CustomerId { get; set; }
		public Customer Customer { get; set; }
		public IList<CartItem> Items { get; set; }

		public Cart() {
			Items = new List<CartItem>();
			// Customer = new VoidCustomer();
		}
	}
}
