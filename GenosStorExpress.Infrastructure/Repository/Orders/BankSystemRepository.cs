using GenosStorExpress.Domain.Entity.Orders;
using GenosStorExpress.Domain.Interface.Orders;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Orders {
    public class BankSystemRepository: IBankSystemRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public BankSystemRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<BankSystem> List() {
            return _context.BankSystems.ToList();
        }

        public BankSystem Get(int id) {
            return _context.BankSystems.Find(id);
        }

        public void Create(BankSystem bankSystem) {
            _context.BankSystems.Add(bankSystem);
        }

        public void Update(BankSystem bankSystem) {
            _context.Entry(bankSystem).State = EntityState.Modified;
        }

        public void Delete(int id) {
            BankSystem bankSystem = _context.BankSystems.Find(id);
            if (bankSystem != null)
                _context.BankSystems.Remove(bankSystem);
        }
        
    }
}