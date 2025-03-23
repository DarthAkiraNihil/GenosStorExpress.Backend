using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.Orders {
	[Table("ActiveDiscounts")]
	public class ActiveDiscount {
		public int Id { get; set; }
		[Required]
		public DateTime CreatedAt { get; set; }
		[Required]
		public DateTime EndsAt { get; set; }
		[Required]
		public double Value { get; set; }

	}
}
