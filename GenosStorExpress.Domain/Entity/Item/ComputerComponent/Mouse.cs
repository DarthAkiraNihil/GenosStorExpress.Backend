﻿using GenosStorExpress.Domain.Entity.Item.Characteristic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.Item.ComputerComponent {
	[Table("public.Mouses")]
	public class Mouse: ComputerComponent {

		[Required]
		public byte ButtonsCount { get; set; }
		[Required]
		public bool HasProgrammableButtons { get; set; }
		[Required]
		public virtual List<DPIMode> DPIModes { get; set; }
		[Required]
		public bool IsWireless { get; set; }
		
	}
}
