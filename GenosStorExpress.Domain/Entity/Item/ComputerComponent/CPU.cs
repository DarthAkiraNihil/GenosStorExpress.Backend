using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Entity.Item.SimpleComputerComponent;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.Item.ComputerComponent {
	[Table("public.CPUs")]
	public class CPU: ComputerComponent {

		//public override ItemType Type => ItemType.CPU;

		//public CPUSocket Socket { get; set; }
		public virtual CPUCore Core { get; set; }
		public virtual CPUSocket Socket { get; set; }

		[Required]
		public int CoresCount { get; set; }
		[Required]
		public int ThreadsCount { get; set; }
		[Required]
		public float L2CahceSize { get; set; }
		[Required]
		public float L3CacheSize { get; set; }
		[Required]
		public float TechnicalProcess {  get; set; }
		[Required]
		public float BaseFrequency { get; set; }
		[Required]
		public virtual List<RAMType> SupportedRamType { get; set; }
		[Required]
		public int SupportedRAMSize { get; set; }
		[Required]
		public bool HasIntegratedGraphics { get; set; }
		
	}
}
