using GenosStorExpress.Domain.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;

// public enum MatrixType {
// 	TN,
// 	IPS,
// 	VA,
// 	OLED
// }

namespace GenosStorExpress.Domain.Entity.Item.Characteristic {
	[Table("public.MatrixTypes")]
	public class MatrixType: Named {
		public long Id { get; set; }
	}
}