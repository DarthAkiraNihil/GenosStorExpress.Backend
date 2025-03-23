using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using GenosStorExpress.Domain.Entity.Item;

namespace GenosStorExpress.Domain.Entity.Orders {
	[Table("OrderItems")]
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
		public Order Order { get; set; }
		[Column(Order = 2)]
		public Item.Item Item { get; set; }

		public OrderItems() {
			Order = new Order();
			Item = new VoidItem();
		}
	}
}
