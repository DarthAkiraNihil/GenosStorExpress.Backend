using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.User {
	[Table("public.Administrators")]
	public class Administrator: User {

		public override UserType UserType {
			get {
				return UserType.Administrator;
			}
		}
	}
}
