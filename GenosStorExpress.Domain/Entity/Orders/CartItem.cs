using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using GenosStorExpress.Domain.Entity.Item;

namespace GenosStorExpress.Domain.Entity.Orders {
    [Table("CartItems")]
    [PrimaryKey(nameof(CartId), nameof(ItemId))]
    public class CartItem {
        [Column(Order = 1)]
        [ForeignKey("Cart")]
		public string CartId { get; set; }
        [Column(Order = 2)]
        [ForeignKey("Item")]
		public int ItemId { get; set; }
		
        [Column(Order = 3)]
        public int Quantity { get; set; }

        [Column(Order = 1)]
		public Item.Item Item { get; set; }
		[Column(Order = 2)]
		public Cart Cart { get; set; }

		public CartItem() {
			Cart = new Cart();
			Item = new VoidItem();
		}
    }
}