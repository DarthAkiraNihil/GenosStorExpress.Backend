using GenosStorExpress.Domain.Entity.Item.Characteristic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.Item.ComputerComponent {
	[Table("public.Keyboards")]
	public class Keyboard: ComputerComponent {

		[Required]
		public bool HasRGBLighting { get; set; }
		[Required]
		public bool IsWireless { get; set; }
		
		public KeyboardTypesize Typesize { get; set; }
		public KeyboardType KeyboardType { get; set; }

	}
}
