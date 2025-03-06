using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.Item.Characteristic {
	[Table("public.Definitions")]
	public class Definition {
		public int Id { get; set; }
		[Required]
		public int Width { get; set; }
		[Required]
		public int Height { get; set; }
	}
}
