using GenosStorExpress.Domain.Entity.Base;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using System.ComponentModel.DataAnnotations.Schema;

// public enum RAMType {
// 	DDR3,
// 	DDR4,
// 	DDR5,
// }

namespace GenosStorExpress.Domain.Entity.Item.Characteristic {
	[Table("public.RAMTypes")]
	public class RAMType: Named {
		public long Id { get; set; }
		public List<Motherboard> Motherboards { get; set; }
		public List<CPU> CPUs { get; set; }

		public RAMType() {
			Motherboards = new List<Motherboard>();
			CPUs = new List<CPU>();
		}
	}
}