using GenosStorExpress.Domain.Entity.Base;
using GenosStorExpress.Domain.Entity.Orders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.Item {
	[Table("Items")]
	public abstract class Item: WithModel {
		public int Id { get; set; }

		[Required]
		public double Price { get; set; }
		
		public string ImageBase64 { get; set; }
		[Required]
		[MaxLength(Int32.MaxValue)]
		public string Description { get; set; }
		
		public IList<Cart> Carts { get; set; }
		public IList<Review> Reviews { get; set; }

		public ActiveDiscount? ActiveDiscount { get; set; }
		public ItemType ItemType { get; set; }

		public Item() {
			Carts = new List<Cart>();
			Reviews = new List<Review>();
			Description = string.Empty;
			ImageBase64 = string.Empty;
			ItemType = new ItemType();
		}
	}
}
