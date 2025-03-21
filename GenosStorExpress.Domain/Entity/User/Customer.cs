﻿using GenosStorExpress.Domain.Entity.Orders;
using System.ComponentModel.DataAnnotations;

namespace GenosStorExpress.Domain.Entity.User {
	public abstract class Customer: User {
		public virtual IList<Order> Orders {  get; set; }
		public virtual IList<BankCard> BankCards {  get; set; }
		[Required]
		public virtual Cart Cart { get; set; }
		public int CartId { get; set; }

		public Customer() {
			//Orders = new List<Order>();
			//BankCards = new List<BankCard>();
		}
	}
}
