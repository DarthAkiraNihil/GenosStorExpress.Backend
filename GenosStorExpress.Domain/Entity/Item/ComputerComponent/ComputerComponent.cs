using GenosStorExpress.Domain.Entity.Item.Characteristic;
using System.ComponentModel.DataAnnotations;

namespace GenosStorExpress.Domain.Entity.Item.ComputerComponent {
	public abstract class ComputerComponent: Item {
		[Required]
		public double TDP { get; set; }

		public virtual Vendor Vendor { get; set; }
	}
}
