using GenosStorExpress.Domain.Entity.Item.Characteristic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.Item.ComputerComponent {
	[Table("Displays")]
	public class Display: ComputerComponent {

		[Required]
		public int MaxUpdateFrequency { get; set; }
		[Required]
		public double ScreenDiagonal { get; set; }

		public Definition Definition { get; set; }
		public MatrixType MatrixType { get; set; }
		public Underlight Underlight { get; set; }
		public VesaSize VesaSize { get; set; }

		public Display() {
			Definition = new Definition();
			MatrixType = new MatrixType();
			Underlight = new Underlight();
			VesaSize = new VesaSize();
		}
	}
}
