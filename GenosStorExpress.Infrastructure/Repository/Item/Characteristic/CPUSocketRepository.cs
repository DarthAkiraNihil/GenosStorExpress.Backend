using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface.Item.Characteristic;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Item.Characteristic {
    public class CPUSocketRepository: ICPUSocketRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public CPUSocketRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<CPUSocket> List() {
            return _context.CPUSockets.ToList();
        }

        public CPUSocket Get(int id) {
            return _context.CPUSockets.Find(id);
        }

        public void Create(CPUSocket cpuSocket) {
            _context.CPUSockets.Add(cpuSocket);
        }

        public void Update(CPUSocket cpuSocket) {
            _context.Entry(cpuSocket).State = EntityState.Modified;
        }

        public void Delete(int id) {
            CPUSocket cpuSocket = _context.CPUSockets.Find(id);
            if (cpuSocket != null)
                _context.CPUSockets.Remove(cpuSocket);
        }
        
    }
}