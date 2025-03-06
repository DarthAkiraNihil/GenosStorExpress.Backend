using GenosStorExpress.Domain.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.Orders {

	[Table("public.BankSystems")]
	public class BankSystem: Named {
		public long Id { get; set; }
	}
	// public enum BankSystem {
	// 	Visa,
	// 	MasterCard,
	// 	UnionPay,
	// 	JBC,
	// 	Mir
	// }
}