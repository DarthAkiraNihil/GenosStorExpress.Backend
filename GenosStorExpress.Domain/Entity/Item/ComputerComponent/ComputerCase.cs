using GenosStorExpress.Domain.Entity.Item.Characteristic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.Item.ComputerComponent {
	[Table("public.ComputerCases")]
	public class ComputerCase: ComputerComponent {
		public virtual ComputerCaseTypesize Typesize { get; set; }
		[Required]
		public float Length { get; set; }
		[Required]
		public float Width { get; set; }
		[Required]
		public float Height { get; set; }

		[Required]
		public virtual List<MotherboardFormFactor> SupportedMotherboardFormFactors { get; set; }
		[Required]
		public bool HasARGBLighting { get; set; }
		[Required]
		public byte DrivesSlotsCount;

	}
}
