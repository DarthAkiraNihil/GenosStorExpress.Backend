﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenosStorExpress.Domain.Entity.Orders {
	[Table("public.BankCards")]
	public class BankCard {
		public int Id { get; set; }
		[Required]
		public long Number { get; set; }
		[Required]
		public byte ValidThruMonth { get; set; }
		[Required]
		public byte ValidThruYear { get; set; }
		[Required]
		public byte CVC { get; set; }
		[Required]
		public string Owner { get; set; }
		
		public BankSystem BankSystem { get; set; }

		public BankCard() {
			Owner = string.Empty;
			BankSystem = new BankSystem();
		}
	}
}
