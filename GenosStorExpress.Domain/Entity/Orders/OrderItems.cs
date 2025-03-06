using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.Orders {
	[Table("public.OrderItems")]
	[PrimaryKey(nameof(OrderId), nameof(ItemId))]
	public class OrderItems {
		//public long Id { get; set; }
		[Column(Order = 1)]
		[ForeignKey("Order")]
		public long OrderId { get; set; }
		[Column(Order = 2)]
		[ForeignKey("Item")]
		public int ItemId { get; set; }
		
		[Column(Order = 3)]
		public int Quantity { get; set; }
		[Column(Order = 4)]
		public double BoughtFor { get; set; }

		[Column(Order = 1)]
		public virtual Order Order { get; set; }
		[Column(Order = 2)]
		public virtual Item.Item Item { get; set; }
		
	}
}
