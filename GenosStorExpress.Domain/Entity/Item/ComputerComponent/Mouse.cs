using GenosStorExpress.Domain.Entity.Item.Characteristic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.Item.ComputerComponent {
	[Table("Mouses")]
	public class Mouse: ComputerComponent {

		[Required]
		public byte ButtonsCount { get; set; }
		[Required]
		public bool HasProgrammableButtons { get; set; }
		[Required]
		public IList<DPIMode> DPIModes { get; set; }
		[Required]
		public bool IsWireless { get; set; }

		public Mouse() {
			DPIModes = new List<DPIMode>();
		}
	}
}
