using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.User {
	[Table("public.IndividualEntities")]
	public class IndividualEntity: Customer {
		public override UserType UserType => UserType.IndividualEntity;
		[Required]
		[MaxLength(256)]
		public string Name { get; set; }
		[Required]
		[MaxLength(256)]
		public string Surname { get; set; }

		public IndividualEntity() {
			Name = string.Empty;
			Surname = string.Empty;
		}
	}
}
