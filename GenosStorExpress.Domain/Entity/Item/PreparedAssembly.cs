using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.Item {
	[Table("public.PreparedAssemblies")]
	public class PreparedAssembly: Item {

		[Required]
		public virtual List<RAM> RAM { get; set; }
		[Required]
		public virtual List<DiskDrive> Disks { get; set; }
		
		[Required]
		public virtual CPU CPU { get; set; }
		[Required]
		public virtual Motherboard Motherboard { get; set; }
		[Required]
		public virtual GraphicsCard GraphicsCard { get; set; }
		[Required]
		public virtual PowerSupply PowerSupply { get; set; }
		
		public virtual Display Display { get; set; }
		[Required]
		public virtual ComputerCase ComputerCase { get; set; }
		public virtual Keyboard Keyboard { get; set; }
		public virtual Mouse Mouse { get; set; }
		[Required]
		public virtual CPUCooler CPUCooler { get; set; }
		
	}
}
