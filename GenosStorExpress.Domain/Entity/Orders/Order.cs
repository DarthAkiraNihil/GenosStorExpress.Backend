using GenosStorExpress.Domain.Entity.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.Orders {
	[Table("public.Orders")]
	public class Order {
		public long Id { get; set; }

		[Required]
		public virtual Customer Customer { get; set; }
		[Required]
		public virtual List<OrderItems> Items { get; set; }
		[Required]
		public virtual OrderStatus OrderStatus { get; set; }
		[Required]
		public DateTime CreatedAt { get; set; }

	}
}
