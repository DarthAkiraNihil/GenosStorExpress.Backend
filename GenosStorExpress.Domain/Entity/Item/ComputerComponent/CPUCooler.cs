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
		
		public virtual CoolerMaterial FoundationMaterial { get; set; }
		public virtual CoolerMaterial RadiatorMaterial { get; set; }
	}
}
