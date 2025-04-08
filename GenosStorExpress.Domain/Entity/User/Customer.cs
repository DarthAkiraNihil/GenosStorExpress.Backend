using GenosStorExpress.Domain.Entity.Orders;
using System.ComponentModel.DataAnnotations;

namespace GenosStorExpress.Domain.Entity.User {
	public abstract class Customer: User {
		public IList<Order> Orders {  get; set; }
		public IList<BankCard> BankCards {  get; set; }
		[Required]
		public Cart Cart { get; set; }
		public string CartId { get; set; }

		protected Customer() {
			Orders = new List<Order>();
			BankCards = new List<BankCard>();
			Cart = new Cart();
		}
	}
}
