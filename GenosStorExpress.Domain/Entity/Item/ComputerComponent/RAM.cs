using GenosStorExpress.Domain.Entity.Item.Characteristic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.Item.ComputerComponent {
	[Table("RAMs")]
	public class RAM: ComputerComponent {

		[Required]
		public int TotalSize { get; set; }
		[Required]
		public int ModuleSize { get; set; }
		[Required]
		public byte ModulesCount { get; set; }
		[Required]
		public int Frequency { get; set; }
		[Required]
		public byte CL { get; set; }
		[Required]
		public byte tRCD { get; set; }
		[Required]
		public byte tRP { get; set; }
		[Required]
		public byte tRAS { get; set; }
		
		public RAMType Type { get; set; }

		public IList<PreparedAssembly> PreparedAssemblies { get; set; }

		public RAM() {
			PreparedAssemblies = new List<PreparedAssembly>();
			Type = new RAMType();
		} 
	}
}
