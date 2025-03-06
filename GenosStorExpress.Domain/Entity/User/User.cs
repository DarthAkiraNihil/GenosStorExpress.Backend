using System.ComponentModel.DataAnnotations;

namespace GenosStorExpress.Domain.Entity.User {
	public abstract class User {
		public int Id { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string PasswordHash { get; set; }
		[Required]
		public string Salt { get; set; }

		[Required]
		public abstract UserType UserType { get; }
	}
}
