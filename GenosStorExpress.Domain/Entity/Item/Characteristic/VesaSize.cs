using GenosStorExpress.Domain.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;

// public enum VesaSize {
// 	Vesa100x100,
// 	vesa120x120,
// }

namespace GenosStorExpress.Domain.Entity.Item.Characteristic {
	[Table("public.VesaSizes")]
	public class VesaSize: Named {
		public long Id { get; set; }
	}
}