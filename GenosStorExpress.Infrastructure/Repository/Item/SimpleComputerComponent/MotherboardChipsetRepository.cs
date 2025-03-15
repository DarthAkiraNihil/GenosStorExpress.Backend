using GenosStorExpress.Domain.Entity.Item.SimpleComputerComponent;
using GenosStorExpress.Domain.Interface.Item.SimpleComputerComponent;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Item.SimpleComputerComponent {
    public class MotherboardChipsetRepository: IMotherboardChipsetRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public MotherboardChipsetRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<MotherboardChipset> List() {
            return _context.MotherboardChipsets.ToList();
        }

        public MotherboardChipset? Get(int id) {
            return _context.MotherboardChipsets.Find(id);
        }

        public void Create(MotherboardChipset motherboardChipset) {
            _context.MotherboardChipsets.Add(motherboardChipset);
        }

        public void Update(MotherboardChipset motherboardChipset) {
            _context.Entry(motherboardChipset).State = EntityState.Modified;
        }

        public void Delete(int id) {
            MotherboardChipset? motherboardChipset = _context.MotherboardChipsets.Find(id);
            if (motherboardChipset != null) {
                _context.MotherboardChipsets.Remove(motherboardChipset);
            }
        }
        
    }
}