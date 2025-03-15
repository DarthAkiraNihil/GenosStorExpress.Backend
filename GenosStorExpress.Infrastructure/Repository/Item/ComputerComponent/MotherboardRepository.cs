using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Item.ComputerComponent {
    public class MotherboardRepository: IMotherboardRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public MotherboardRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<Motherboard> List() {
            return _context.Motherboards
                           .Include(i => i.Reviews)
                           .Include(i => i.SupportedCPUCores)
                           .Include(i => i.SupportedRAMTypes)
                           .Include(i => i.VideoPorts)
                           .ToList();
        }

        public Motherboard? Get(int id) {
            return _context.Motherboards
                           .Include(i => i.Reviews)
                           .Include(i => i.SupportedCPUCores)
                           .Include(i => i.SupportedRAMTypes)
                           .Include(i => i.VideoPorts)
                           .FirstOrDefault(i => i.Id == id);
        }

        public void Create(Motherboard motherboard) {
            _context.Motherboards.Add(motherboard);
        }

        public void Update(Motherboard motherboard) {
            _context.Entry(motherboard).State = EntityState.Modified;
        }

        public void Delete(int id) {
            Motherboard? motherboard = _context.Motherboards.Find(id);
            if (motherboard != null) {
                _context.Motherboards.Remove(motherboard);
            }
        }
        
    }
}