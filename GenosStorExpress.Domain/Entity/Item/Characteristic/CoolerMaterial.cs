using GenosStorExpress.Domain.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;

// public enum CoolerMaterial {
// 	Cooper,
// 	Aluminium
// }

namespace GenosStorExpress.Domain.Entity.Item.Characteristic {
	[Table("CoolerMaterials")]
	public class CoolerMaterial: Named {
		public long Id { get; set; }
	}
}