using GenosStorExpress.Domain.Entity.Orders;
using GenosStorExpress.Domain.Interface.Orders;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Orders {
    public class ActiveDiscountRepository: IActiveDiscountRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public ActiveDiscountRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<ActiveDiscount> List() {
            return _context.ActiveDiscounts.ToList();
        }

        public ActiveDiscount? Get(int id) {
            return _context.ActiveDiscounts.Find(id);
        }

        public void Create(ActiveDiscount activeDiscount) {
            _context.ActiveDiscounts.Add(activeDiscount);
        }

        public void Update(ActiveDiscount activeDiscount) {
            _context.Entry(activeDiscount).State = EntityState.Modified;
        }

        public void Delete(int id) {
            ActiveDiscount? activeDiscount = _context.ActiveDiscounts.Find(id);
            if (activeDiscount != null) {
                _context.ActiveDiscounts.Remove(activeDiscount);
            }
        }
        
    }
}