using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Item.ComputerComponent {
    public class SataSSDRepository: ISataSSDRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public SataSSDRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<SataSSD> List() {
            return _context.SataSSDs
                           .Include(i => i.Reviews)
                           .ToList();
        }

        public SataSSD? Get(int id) {
            return _context.SataSSDs
                           .Include(i => i.Reviews)
                           .FirstOrDefault(i => i.Id == id);
        }

        public void Create(SataSSD sataSSD) {
            _context.SataSSDs.Add(sataSSD);
        }

        public void Update(SataSSD sataSSD) {
            _context.Entry(sataSSD).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id) {
            SataSSD? sataSSD = _context.SataSSDs.Find(id);
            if (sataSSD != null) {
                _context.SataSSDs.Remove(sataSSD);
            }
        }
        
    }
}