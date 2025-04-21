using GenosStorExpress.Domain.Entity.Orders;
using System.ComponentModel.DataAnnotations;
using GenosStorExpress.Domain.Entity.Item;

namespace GenosStorExpress.Domain.Entity.User {
	public abstract class Customer: User {
		public IList<Order> Orders {  get; set; }
		public IList<BankCard> BankCards {  get; set; }
		[Required]
		public Cart Cart { get; set; }
		public string CartId { get; set; }
		public IList<Review> Reviews { get; set; }

		protected Customer() {
			Orders = new List<Order>();
			BankCards = new List<BankCard>();
			Reviews = new List<Review>();	
			Cart = new Cart();
		}
	}
}
