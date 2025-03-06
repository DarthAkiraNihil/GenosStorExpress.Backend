using GenosStorExpress.Domain.Entity.Item.SimpleComputerComponent;
using GenosStorExpress.Domain.Interface.Item.SimpleComputerComponent;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Item.SimpleComputerComponent {
    public class GPURepository: IGPURepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public GPURepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<GPU> List() {
            return _context.GPUs.ToList();
        }

        public GPU Get(int id) {
            return _context.GPUs.Find(id);
        }

        public void Create(GPU gpu) {
            _context.GPUs.Add(gpu);
        }

        public void Update(GPU gpu) {
            _context.Entry(gpu).State = EntityState.Modified;
        }

        public void Delete(int id) {
            GPU gpu = _context.GPUs.Find(id);
            if (gpu != null)
                _context.GPUs.Remove(gpu);
        }
        
    }
}