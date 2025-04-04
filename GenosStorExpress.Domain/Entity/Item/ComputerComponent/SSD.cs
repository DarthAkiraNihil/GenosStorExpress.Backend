﻿using GenosStorExpress.Domain.Entity.Item.SimpleComputerComponent;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.Item.ComputerComponent {
	[Table("SSDs")]
	public abstract class SSD: DiskDrive {
		[Required]
		public int TBW { get; set; }
		[Required]
		public float DWPD { get; set; }
		[Required]
		public byte BitsForCell { get; set; }
		
		public SSDController Controller { get; set; }

		protected SSD() {
			Controller = new SSDController();
		}
	}
}

