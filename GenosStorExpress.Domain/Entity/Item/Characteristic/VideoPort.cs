using GenosStorExpress.Domain.Entity.Base;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using System.ComponentModel.DataAnnotations.Schema;

// public enum VideoPort {
// 	HDMI,
// 	DisplayPort,
// 	VGA,
// 	DVI,
// }

namespace GenosStorExpress.Domain.Entity.Item.Characteristic {
	[Table("public.VideoPorts")]
	public class VideoPort: Named {
		public long Id { get; set; }
		
		public List<GraphicsCard> GraphicsCards { get; set; }
		public List<Motherboard> Motherboards { get; set; }

		public VideoPort() {
			GraphicsCards = new List<GraphicsCard>();
			Motherboards = new List<Motherboard>();
		}

	}
}