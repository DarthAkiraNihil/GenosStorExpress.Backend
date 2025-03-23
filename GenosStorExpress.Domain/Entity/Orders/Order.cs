using GenosStorExpress.Domain.Entity.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.Orders {
	[Table("Orders")]
	public class Order {
		public long Id { get; set; }

		[Required]
		public Customer Customer { get; set; }
		[Required]
		public List<OrderItems> Items { get; set; }
		[Required]
		public OrderStatus OrderStatus { get; set; }
		[Required]
		public DateTime CreatedAt { get; set; }

		public Order() {
			OrderStatus = new OrderStatus();
			Items = new List<OrderItems>();
			Customer = new VoidCustomer();
		}
	}
}
