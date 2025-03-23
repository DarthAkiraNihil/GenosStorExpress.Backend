using GenosStorExpress.Domain.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;

// public enum PCIEVersion {
// 	V3,
// 	V4,
// }

namespace GenosStorExpress.Domain.Entity.Item.Characteristic {
	[Table("PCIEVersions")]
	public class PCIEVersion: Named {
		public long Id { get; set; }
	}
}