using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Item.ComputerComponent {
    public class NVMeSSDRepository: INVMeSSDRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public NVMeSSDRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<NVMeSSD> List() {
            return _context.NVMeSSDs
                           .Include(i => i.Reviews)
                           .ToList();
        }

        public NVMeSSD? Get(int id) {
            return _context.NVMeSSDs
                           .Include(i => i.Reviews)
                           .FirstOrDefault(i => i.Id == id);
        }

        public void Create(NVMeSSD nvmeSSD) {
            _context.NVMeSSDs.Add(nvmeSSD);
            _context.SaveChanges();
        }

        public void Update(NVMeSSD nvmeSSD) {
            _context.Entry(nvmeSSD).State = EntityState.Modified;
        }

        public void Delete(int id) {
            NVMeSSD? nvmeSSD = _context.NVMeSSDs.Find(id);
            if (nvmeSSD != null) {
                _context.NVMeSSDs.Remove(nvmeSSD);
            }
        }
        
    }
}