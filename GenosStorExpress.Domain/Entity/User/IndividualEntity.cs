using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.User {
	[Table("public.IndividualEntities")]
	public class IndividualEntity: Customer {
		public override UserType UserType => UserType.IndividualEntity;
		[Required]
		public string Name { get; set; }
		[Required]
		public string Surname { get; set; }
		[Required]
		public string PhoneNumber { get; set; }
		
	}
}
