using GenosStorExpress.Domain.Entity.Item.Characteristic;
using System.ComponentModel.DataAnnotations;

namespace GenosStorExpress.Domain.Entity.Item.ComputerComponent {
	public abstract class ComputerComponent: Item {
		[Required]
		public double TDP { get; set; }

		public Vendor Vendor { get; set; }

		protected ComputerComponent() {
			Vendor = new Vendor();
		}
	}
}
