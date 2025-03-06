using GenosStorExpress.Domain.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;

// public enum OrderStatus {
// 	Created,
// 	Confirmed,
// 	AwaitsPayment,
// 	Paid,
// 	Processing,
// 	Delivering,
// 	Recieved,
// 	Canceled
// }

namespace GenosStorExpress.Domain.Entity.Orders {

	[Table("public.OrderStatus")]
	public class OrderStatus: Named {
		public long Id { get; set; }
	}
}