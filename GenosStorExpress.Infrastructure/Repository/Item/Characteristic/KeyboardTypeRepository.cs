using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface.Item.Characteristic;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Item.Characteristic {
    public class KeyboardTypeRepository: IKeyboardTypeRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public KeyboardTypeRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<KeyboardType> List() {
            return _context.KeyboardTypes.ToList();
        }

        public KeyboardType? Get(int id) {
            return _context.KeyboardTypes.Find(id);
        }

        public void Create(KeyboardType keyboardType) {
            _context.KeyboardTypes.Add(keyboardType);
        }

        public void Update(KeyboardType keyboardType) {
            _context.Entry(keyboardType).State = EntityState.Modified;
        }

        public void Delete(int id) {
            KeyboardType? keyboardType = _context.KeyboardTypes.Find(id);
            if (keyboardType != null) {
                _context.KeyboardTypes.Remove(keyboardType);
            }
        }
        
    }
}