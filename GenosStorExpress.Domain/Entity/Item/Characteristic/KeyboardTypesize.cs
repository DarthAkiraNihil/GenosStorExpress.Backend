using GenosStorExpress.Domain.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;

// public enum KeyboardTypesize {
// 	TKL,
// 	Percent60,
// 	Full,
// 	FullPlusNumpad
// }

namespace GenosStorExpress.Domain.Entity.Item.Characteristic {
	[Table("KeyboardTypesizes")]
	public class KeyboardTypesize: Named {
		public long Id { get; set; }
	}
}