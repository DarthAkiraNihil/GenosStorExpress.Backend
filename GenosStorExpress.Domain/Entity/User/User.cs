using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GenosStorExpress.Domain.Entity.User {
	public abstract class User: IdentityUser {

		//[Required]
		//public string PasswordHash { get; set; }
		//[Required]
		//public string Salt { get; set; }

		[Required]
		public abstract UserType UserType { get; }
	}
}
