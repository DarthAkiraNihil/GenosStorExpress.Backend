using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.Item.ComputerComponent {
	[Table("DiskDrives")]
	public abstract class DiskDrive: ComputerComponent {

		[Required]
		public long Capacity { get; set; }
		[Required]
		public long ReadSpeed { get; set; }
		[Required]
		public long WriteSpeed { get; set; }

		public IList<PreparedAssembly> PreparedAssemblies { get; set; }

		public DiskDrive() {
			PreparedAssemblies = new List<PreparedAssembly>();
		}
	}
}
