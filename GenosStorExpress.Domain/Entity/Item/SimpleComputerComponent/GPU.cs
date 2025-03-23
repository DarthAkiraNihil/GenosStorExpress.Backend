using GenosStorExpress.Domain.Entity.Base;
using GenosStorExpress.Domain.Entity.Item.Characteristic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.Item.SimpleComputerComponent {
	[Table("GPUs")]
	public class GPU: WithModel {
		[Required]
		public int Id { get; set; }
		
		public Vendor Vendor { get; set; }

		public GPU() {
			Vendor = new Vendor();
		}
	}
}
