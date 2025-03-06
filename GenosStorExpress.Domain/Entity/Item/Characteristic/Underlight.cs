using GenosStorExpress.Domain.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;

// public enum Underlight {
// 	LED,
// 	CCFL,
// 	RGB,
// }

namespace GenosStorExpress.Domain.Entity.Item.Characteristic {
	[Table("public.Underlights")]
	public class Underlight: Named {
		public long Id { get; set; }
	}
}