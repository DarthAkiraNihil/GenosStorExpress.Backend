using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Item.ComputerComponent {
    public class RAMRepository: IRAMRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public RAMRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<RAM> List() {
            return _context.RAMs
                           .Include(i => i.Reviews)
                           .ToList();
        }

        public RAM? Get(int id) {
            return _context.RAMs
                           .Include(i => i.Reviews)
                           .FirstOrDefault(r => r.Id == id);
        }

        public void Create(RAM ram) {
            _context.RAMs.Add(ram);
            _context.SaveChanges();
        }

        public void Update(RAM ram) {
            _context.Entry(ram).State = EntityState.Modified;
        }

        public void Delete(int id) {
            RAM? ram = _context.RAMs.Find(id);
            if (ram != null) {
                _context.RAMs.Remove(ram);
            }
        }
        
    }
}