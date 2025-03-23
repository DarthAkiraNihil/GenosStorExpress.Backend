using GenosStorExpress.Domain.Entity.Base;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using System.ComponentModel.DataAnnotations.Schema;

// public enum MotherboardFormFactor {
// 	miniATX,
// 	ATX,
// 	microATX,
// 	miniITX,
// }

namespace GenosStorExpress.Domain.Entity.Item.Characteristic {
	[Table("MotherboardFormFactors")]
	public class MotherboardFormFactor: Named {
		public long Id { get; set; }
		public IList<ComputerCase> ComputerCases { get; set; }

		public MotherboardFormFactor() {
			ComputerCases = new List<ComputerCase>();
		}

	}
}