using GenosStorExpress.Domain.Entity.Orders;
using GenosStorExpress.Domain.Interface.Orders;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Orders {
    public class BankCardRepository: IBankCardRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public BankCardRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<BankCard> List() {
            return _context.BankCards.ToList();
        }

        public BankCard Get(int id) {
            return _context.BankCards.Find(id);
        }

        public void Create(BankCard bankCard) {
            _context.BankCards.Add(bankCard);
        }

        public void Update(BankCard bankCard) {
            _context.Entry(bankCard).State = EntityState.Modified;
        }

        public void Delete(int id) {
            BankCard bankCard = _context.BankCards.Find(id);
            if (bankCard != null)
                _context.BankCards.Remove(bankCard);
        }
        
    }
}