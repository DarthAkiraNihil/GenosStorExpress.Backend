using GenosStorExpress.Domain.Entity.Item.SimpleComputerComponent;
using GenosStorExpress.Domain.Interface.Item.SimpleComputerComponent;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Item.SimpleComputerComponent {
    public class CPUCoreRepository: ICPUCoreRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public CPUCoreRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<CPUCore> List() {
            return _context.CPUCores.ToList();
        }

        public CPUCore Get(int id) {
            return _context.CPUCores.Find(id);
        }

        public void Create(CPUCore cpuCore) {
            _context.CPUCores.Add(cpuCore);
        }

        public void Update(CPUCore cpuCore) {
            _context.Entry(cpuCore).State = EntityState.Modified;
        }

        public void Delete(int id) {
            CPUCore cpuCore = _context.CPUCores.Find(id);
            if (cpuCore != null)
                _context.CPUCores.Remove(cpuCore);
        }
        
    }
}