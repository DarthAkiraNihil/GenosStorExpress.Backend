using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.Item.ComputerComponent {
	[Table("public.HDDs")]
	public class HDD: DiskDrive {
		[Required]
		public int RPM { get; set; }
	}
}
