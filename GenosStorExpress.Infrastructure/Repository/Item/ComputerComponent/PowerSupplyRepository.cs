using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Item.ComputerComponent {
    public class PowerSupplyRepository: IPowerSupplyRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public PowerSupplyRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<PowerSupply> List() {
            return _context.PowerSupplies.ToList();
        }

        public PowerSupply Get(int id) {
            return _context.PowerSupplies.Find(id);
        }

        public void Create(PowerSupply powerSupply) {
            _context.PowerSupplies.Add(powerSupply);
        }

        public void Update(PowerSupply powerSupply) {
            _context.Entry(powerSupply).State = EntityState.Modified;
        }

        public void Delete(int id) {
            PowerSupply powerSupply = _context.PowerSupplies.Find(id);
            if (powerSupply != null)
                _context.PowerSupplies.Remove(powerSupply);
        }
        
    }
}