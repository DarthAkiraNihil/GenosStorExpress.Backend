using GenosStorExpress.Domain.Entity.Item.Characteristic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.Item.ComputerComponent {
	[Table("public.PowerSupplies")]
	public class PowerSupply: ComputerComponent {

		[Required]
		public byte SataPorts { get; set; }
		[Required]
		public byte MolexPorts { get; set; }
		[Required]
		public int PowerOutput { get; set; }
		
		public Certificate80Plus Certificate80Plus { get; set; }

		public PowerSupply() {
			Certificate80Plus = new Certificate80Plus();
		}
	}
}
