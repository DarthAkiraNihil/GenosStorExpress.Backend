using System.ComponentModel.DataAnnotations;

namespace GenosStorExpress.Domain.Entity.Base {
	public abstract class Named {
		[Required]
		public string Name { get; set; }

		protected Named() {
			Name = string.Empty;
		}
	}
}
