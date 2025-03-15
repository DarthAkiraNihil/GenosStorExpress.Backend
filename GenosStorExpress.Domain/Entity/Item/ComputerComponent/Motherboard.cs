using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Entity.Item.SimpleComputerComponent;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.Item.ComputerComponent {
	[Table("public.Motherboards")]
	public class Motherboard: ComputerComponent {

		[Required]
		public IList<CPUCore> SupportedCPUCores { get; set; }
		[Required]
		public IList<RAMType> SupportedRAMTypes { get; set; }

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
		public IList<VideoPort> VideoPorts { get; set; }
		[Required]
		public byte RJ45PortsCount { get; set; }
		[Required]
		public byte DigitalAudioPortsCount { get; set; }

		public float NetworkAdapterSpeed { get; set; }
		
		public MotherboardFormFactor FormFactor { get; set; }
		public CPUSocket CPUSocket { get; set; }
		public PCIEVersion PCIEVersion { get; set; }
		
		public MotherboardChipset MotherboardChipset { get; set; }
		public AudioChipset AudioChipset { get; set; }
		public NetworkAdapter NetworkAdapter { get; set; }

		public Motherboard() {
			SupportedCPUCores = new List<CPUCore>();
			SupportedRAMTypes = new List<RAMType>();
			VideoPorts = new List<VideoPort>();
			FormFactor = new MotherboardFormFactor();
			CPUSocket = new CPUSocket();
			PCIEVersion = new PCIEVersion();
			MotherboardChipset = new MotherboardChipset();
			AudioChipset = new AudioChipset();
			NetworkAdapter = new NetworkAdapter();
		}
	}
}
