using GenosStorExpress.Domain.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;

// public enum ComputerCaseTypesize {
// 	MiniTower,
// 	MidTower,
// 	BigTower,
// }

namespace GenosStorExpress.Domain.Entity.Item.Characteristic {
	[Table("ComputerCaseTypesizes")]
	public class ComputerCaseTypesize: Named {
		public long Id { get; set; }
	}
}