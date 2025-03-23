using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.User {
	[Table("LegalEntities")]
	public class LegalEntity: Customer {
		public override UserType UserType => UserType.LegalEntity;
		[Required]
		public long INN { get; set; }
		[Required]
		public long KPP { get; set; }
		[Required]
		[MaxLength(256)]
		public string PhysicalAddress { get; set; }
		[Required]
		[MaxLength(256)]
		public string LegalAddress { get; set; }
		[Required]
		public bool IsVerified { get; set; }

		public LegalEntity() {
			PhysicalAddress = string.Empty;
			LegalAddress = string.Empty;
		}
	}
}


