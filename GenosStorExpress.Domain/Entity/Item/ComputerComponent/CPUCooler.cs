using GenosStorExpress.Domain.Entity.Item.Characteristic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.Item.ComputerComponent {
	[Table("public.CPUCoolers")]
	public class CPUCooler: ComputerComponent {

		[Required]
		public long MaxFanRPM { get; set; }

		[Required]
		public byte TubesCount { get; set; }
		[Required]
		public float TubesDiameter { get; set; }
		[Required]
		public byte FanCount { get; set; }
		
		public CoolerMaterial FoundationMaterial { get; set; }
		public CoolerMaterial RadiatorMaterial { get; set; }

		public CPUCooler() {
			FoundationMaterial = new CoolerMaterial();
			RadiatorMaterial = new CoolerMaterial();
		}
	}
}
