using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface.Item.Characteristic;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Item.Characteristic {
    public class MotherboardFormFactorRepository: IMotherboardFormFactorRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public MotherboardFormFactorRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<MotherboardFormFactor> List() {
            return _context.MotherboardFormFactors.ToList();
        }

        public MotherboardFormFactor Get(int id) {
            return _context.MotherboardFormFactors.Find(id);
        }

        public void Create(MotherboardFormFactor motherboardFormFactor) {
            _context.MotherboardFormFactors.Add(motherboardFormFactor);
        }

        public void Update(MotherboardFormFactor motherboardFormFactor) {
            _context.Entry(motherboardFormFactor).State = EntityState.Modified;
        }

        public void Delete(int id) {
            MotherboardFormFactor motherboardFormFactor = _context.MotherboardFormFactors.Find(id);
            if (motherboardFormFactor != null)
                _context.MotherboardFormFactors.Remove(motherboardFormFactor);
        }
        
    }
}