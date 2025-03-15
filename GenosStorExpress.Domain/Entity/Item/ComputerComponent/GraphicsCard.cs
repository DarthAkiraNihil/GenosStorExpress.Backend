using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Entity.Item.SimpleComputerComponent;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.Item.ComputerComponent {
	[Table("public.GraphicsCards")]
	public class GraphicsCard: ComputerComponent {

		[Required]
		public int VideoRAM { get; set; }
		[Required]
		public IList<VideoPort> VideoPorts { get; set; }
		[Required]
		public byte MaxDisplaysSupported { get; set; }
		[Required]
		public byte UsedSlots { get; set; }

		public GPU GPU { get; set; }

		public GraphicsCard() {
			VideoPorts = new List<VideoPort>();
			GPU = new GPU();
		}
	}
}
