using GenosStorExpress.Domain.Entity.Item.Characteristic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.Item.ComputerComponent {
	[Table("ComputerComponents")]
	public abstract class ComputerComponent: Item {
		[Required]
		public double TDP { get; set; }

		public Vendor Vendor { get; set; }

		protected ComputerComponent() {
			Vendor = new Vendor();
		}
	}
}
