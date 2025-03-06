// public enum ItemType {
// 	CPU,
// 	RAM,
// 	Motherboard,
// 	GraphicsCard,
// 	PowerSupply,
// 	HDD,
// 	SataSSD,
// 	NVMeSSD,
// 	Display,
// 	CPUCooler,
// 	ComputerCase,
// 	Keyboard,
// 	Mouse,
// 	PreparedAssembly
// }

using GenosStorExpress.Domain.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.Item {
	[Table("public.ItemTypes")]
	public class ItemType: Named {
		public int Id { get; set; }
	}
}
