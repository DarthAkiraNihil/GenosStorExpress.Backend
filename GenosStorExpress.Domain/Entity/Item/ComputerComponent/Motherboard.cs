using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Entity.Item.SimpleComputerComponent;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.Item.ComputerComponent {
	[Table("public.Motherboards")]
	public class Motherboard: ComputerComponent {

		[Required]
		public virtual List<CPUCore> SupportedCPUCores { get; set; }
		[Required]
		public virtual List<RAMType> SupportedRAMTypes { get; set; }

		[Required]
		public byte RAMSlots { get; set; }
		[Required]
		public byte RAMChannels { get; set; }
		[Required]
		public int MaxRAMFrequency { get; set; }
		[Required]
		public byte PCIESlotsCount { get; set; }
		[Required]
		public int PCIEVersionId { get; set; }

		[Required]
		public bool HasNVMeSupport { get; set; }
		[Required]
		public byte M2SlotsCount { get; set; }
		[Required]
		public byte SataPortsCount { get; set; }
		[Required]
		public byte USBPortsCount { get; set; }
		[Required]
		public virtual List<VideoPort> VideoPorts { get; set; }
		[Required]
		public byte RJ45PortsCount { get; set; }
		[Required]
		public byte DigitalAudioPortsCount { get; set; }

		public float NetworkAdapterSpeed { get; set; }
		
		public virtual MotherboardFormFactor FormFactor { get; set; }
		public virtual CPUSocket CPUSocket { get; set; }
		public virtual PCIEVersion PCIEVersion { get; set; }
		
		public virtual MotherboardChipset MotherboardChipset { get; set; }
		public virtual AudioChipset AudioChipset { get; set; }
		public virtual NetworkAdapter NetworkAdapter { get; set; }

	}
}
