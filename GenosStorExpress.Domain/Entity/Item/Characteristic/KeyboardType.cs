using GenosStorExpress.Domain.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;

// public enum KeyboardType {
// 	Optical,
// 	Mechanic,
// 	Membrane,
// }

namespace GenosStorExpress.Domain.Entity.Item.Characteristic {
	[Table("KeyboardType")]
	public class KeyboardType: Named {
		public long Id { get; set; }
	}
}