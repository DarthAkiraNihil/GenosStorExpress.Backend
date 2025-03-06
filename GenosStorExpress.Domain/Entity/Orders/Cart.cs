using GenosStorExpress.Domain.Entity.User;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.Orders {

	[Table("public.Carts")]
	public class Cart {
		public int CustomerId { get; set; }
		public virtual Customer Customer { get; set; }
		public virtual List<CartItem> Items { get; set; }
		
	}
}
