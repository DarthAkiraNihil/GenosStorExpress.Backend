using GenosStorExpress.Domain.Entity.Item.Characteristic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.Item.ComputerComponent {
	[Table("public.Displays")]
	public class Display: ComputerComponent {

		[Required]
		public int MaxUpdateFrequency { get; set; }
		[Required]
		public double ScreedDiagonal { get; set; }

		public virtual Definition Definition { get; set; }
		public virtual MatrixType MatrixType { get; set; }
		public virtual Underlight Underlight { get; set; }
		public virtual VesaSize VesaSize { get; set; }
	}
}
