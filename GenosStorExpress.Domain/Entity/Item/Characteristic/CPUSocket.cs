using GenosStorExpress.Domain.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;

// public enum CPUSocket {
// 	LGA1700,
// 	LGA1200,
// 	Socket4,
// }

namespace GenosStorExpress.Domain.Entity.Item.Characteristic {
	[Table("public.CPUSocket")]
	public class CPUSocket: Named {
		public long Id { get; set; }
	}
}