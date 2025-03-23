using GenosStorExpress.Domain.Entity.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.Item.SimpleComputerComponent {
	[Table("SimpleComputerComponents")]
	public abstract class SimpleComputerComponent: WithModel {
		[Required]
		public long Id { get; set; }
		
		public SimpleComputerComponentType Type { get; set; }

		protected SimpleComputerComponent() {
			Type = new SimpleComputerComponentType();
		}
	}
}
