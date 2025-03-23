using GenosStorExpress.Domain.Entity.Base;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using System.ComponentModel.DataAnnotations.Schema;

// public enum RAMType {
// 	DDR3,
// 	DDR4,
// 	DDR5,
// }

namespace GenosStorExpress.Domain.Entity.Item.Characteristic {
	[Table("RAMTypes")]
	public class RAMType: Named {
		public long Id { get; set; }
		public IList<Motherboard> Motherboards { get; set; }
		public IList<CPU> CPUs { get; set; }

		public RAMType() {
			Motherboards = new List<Motherboard>();
			CPUs = new List<CPU>();
		}
	}
}