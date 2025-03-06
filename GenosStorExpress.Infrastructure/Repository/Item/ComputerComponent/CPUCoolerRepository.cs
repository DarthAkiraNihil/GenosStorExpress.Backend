
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Item.ComputerComponent {
    public class CPUCoolerRepository: ICPUCoolerRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public CPUCoolerRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<CPUCooler> List() {
            return _context.CPUCoolers.ToList();
        }

        public CPUCooler Get(int id) {
            return _context.CPUCoolers.Find(id);
        }

        public void Create(CPUCooler cpuCooler) {
            _context.CPUCoolers.Add(cpuCooler);
        }

        public void Update(CPUCooler cpuCooler) {
            _context.Entry(cpuCooler).State = EntityState.Modified;
        }

        public void Delete(int id) {
            CPUCooler cpuCooler = _context.CPUCoolers.Find(id);
            if (cpuCooler != null)
                _context.CPUCoolers.Remove(cpuCooler);
        }
        
    }
}