using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.Orders {
    [Table("public.CartItems")]
    [PrimaryKey(nameof(CartId), nameof(ItemId))]
    public class CartItem {
        [Column(Order = 1)]
        [ForeignKey("Cart")]
		public int CartId { get; set; }
        [Column(Order = 2)]
        [ForeignKey("Item")]
		public int ItemId { get; set; }
		
        [Column(Order = 3)]
        public int Quantity { get; set; }

        [Column(Order = 1)]
		public virtual Item.Item Item { get; set; }
		[Column(Order = 2)]
		public virtual Cart Cart { get; set; }
    }
}