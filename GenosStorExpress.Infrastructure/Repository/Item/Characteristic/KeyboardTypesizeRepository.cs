using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface.Item.Characteristic;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Item.Characteristic {
    public class KeyboardTypesizeRepository: IKeyboardTypesizeRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public KeyboardTypesizeRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<KeyboardTypesize> List() {
            return _context.KeyboardTypesizes.ToList();
        }

        public KeyboardTypesize? Get(int id) {
            return _context.KeyboardTypesizes.Find(id);
        }

        public void Create(KeyboardTypesize keyboardTypesize) {
            _context.KeyboardTypesizes.Add(keyboardTypesize);
        }

        public void Update(KeyboardTypesize keyboardTypesize) {
            _context.Entry(keyboardTypesize).State = EntityState.Modified;
        }

        public void Delete(int id) {
            KeyboardTypesize? keyboardTypesize = _context.KeyboardTypesizes.Find(id);
            if (keyboardTypesize != null) {
                _context.KeyboardTypesizes.Remove(keyboardTypesize);
            }
        }
        
    }
}