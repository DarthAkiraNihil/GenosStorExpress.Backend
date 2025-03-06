// public enum SimpleComputerComponentType {
// 	MotherboardChipset,
// 	AudioChipset,
// 	NetworkAdapter,
// 	SSDController
// }

using GenosStorExpress.Domain.Entity.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.Item.SimpleComputerComponent {
	[Table("public.SimpleComputerComponentTypes")]
	public class SimpleComputerComponentType: Named {
		[Required]
		public long Id { get; set; }
	}
}