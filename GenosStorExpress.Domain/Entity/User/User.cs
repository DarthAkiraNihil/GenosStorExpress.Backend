using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace GenosStorExpress.Domain.Entity.User {
	public abstract class User: IdentityUser {
		[Required]
		public string Email { get; set; }
		//[Required]
		//public string PasswordHash { get; set; }
		//[Required]
		//public string Salt { get; set; }

		[Required]
		public abstract UserType UserType { get; }
	}
}
