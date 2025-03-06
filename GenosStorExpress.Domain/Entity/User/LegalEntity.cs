using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.User {
	[Table("public.LegalEntities")]
	public class LegalEntity: Customer {
		public override UserType UserType => UserType.LegalEntity;
		[Required]
		public long INN { get; set; }
		[Required]
		public long KPP { get; set; }
		[Required]
		public string PhysicalAddress { get; set; }
		[Required]
		public string LegalAddress { get; set; }
		[Required]
		public bool IsVerified { get; set; }
	}
}


