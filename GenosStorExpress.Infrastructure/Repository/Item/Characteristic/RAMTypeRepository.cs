using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface.Item.Characteristic;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Item.Characteristic {
    public class RAMTypeRepository: IRAMTypeRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public RAMTypeRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<RAMType> List() {
            return _context.RAMTypes.ToList();
        }

        public RAMType? Get(int id) {
            return _context.RAMTypes.Find(id);
        }

        public void Create(RAMType ramType) {
            _context.RAMTypes.Add(ramType);
        }

        public void Update(RAMType ramType) {
            _context.Entry(ramType).State = EntityState.Modified;
        }

        public void Delete(int id) {
            RAMType? ramType = _context.RAMTypes.Find(id);
            if (ramType != null) {
                _context.RAMTypes.Remove(ramType);
            }
        }
        
    }
}