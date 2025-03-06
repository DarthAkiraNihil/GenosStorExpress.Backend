using GenosStorExpress.Domain.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.Item.Characteristic {
	[Table("public.Vendors")]
	public class Vendor: Named {
		public int Id { get; set; }
	}
}
