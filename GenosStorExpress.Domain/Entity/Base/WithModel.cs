using System.ComponentModel.DataAnnotations;

namespace GenosStorExpress.Domain.Entity.Base {
	public abstract class WithModel: Named {
		[Required]
		public string Model {  get; set; }
	}
}
