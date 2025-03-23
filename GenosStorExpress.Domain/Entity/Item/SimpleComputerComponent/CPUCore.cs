using GenosStorExpress.Domain.Entity.Base;
using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.Item.SimpleComputerComponent {
	[Table("CPUCores")]
	public class CPUCore: Named {
		[Required]
		public int Id { get; set; }
		
		public Vendor Vendor { get; set; }
	
		public IList<Motherboard> Motherboards { get; set; }

		public CPUCore() {
			Motherboards = new List<Motherboard>();
			Vendor = new Vendor();
		}
	}
}
