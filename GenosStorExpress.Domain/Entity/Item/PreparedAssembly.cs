using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.Item {
	[Table("public.PreparedAssemblies")]
	public class PreparedAssembly: Item {

		[Required]
		public IList<RAM> RAM { get; set; }
		[Required]
		public IList<DiskDrive> Disks { get; set; }
		
		[Required]
		public CPU CPU { get; set; }
		[Required]
		public Motherboard Motherboard { get; set; }
		[Required]
		public GraphicsCard GraphicsCard { get; set; }
		[Required]
		public PowerSupply PowerSupply { get; set; }
		
		public Display? Display { get; set; }
		[Required]
		public ComputerCase ComputerCase { get; set; }
		public Keyboard? Keyboard { get; set; }
		public Mouse? Mouse { get; set; }
		[Required]
		public CPUCooler CPUCooler { get; set; }

		public PreparedAssembly() {
			RAM = new List<RAM>();
			Disks = new List<DiskDrive>();
			CPU = new CPU();
			Motherboard = new Motherboard();
			GraphicsCard = new GraphicsCard();
			PowerSupply = new PowerSupply();
			Display = new Display();
			ComputerCase = new ComputerCase();
			Keyboard = new Keyboard();
			Mouse = new Mouse();
			CPUCooler = new CPUCooler();
		}
	}
}
