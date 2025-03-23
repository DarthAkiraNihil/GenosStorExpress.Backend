using GenosStorExpress.Domain.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;

// public enum Certificate80Plus {
// 	None,
// 	Standard,
// 	Bronze,
// 	Silver,
// 	Gold,
// 	Platinum,
// 	Titanium
// }

namespace GenosStorExpress.Domain.Entity.Item.Characteristic {
	[Table("Certificates80Plus")]
	public class Certificate80Plus: Named {
		public long Id { get; set; }
	}
}