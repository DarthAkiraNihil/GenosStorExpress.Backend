
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Item.ComputerComponent {
    public class KeyboardRepository: IKeyboardRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public KeyboardRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<Keyboard> List() {
            return _context.Keyboards.Include(i => i.Reviews).ToList();
        }

        public Keyboard? Get(int id) {
            return _context.Keyboards
                           .Include(i => i.Reviews)
                           .FirstOrDefault(i => i.Id == id);
        }

        public void Create(Keyboard keyboard) {
            _context.Keyboards.Add(keyboard);
        }

        public void Update(Keyboard keyboard) {
            _context.Entry(keyboard).State = EntityState.Modified;
        }

        public void Delete(int id) {
            Keyboard? keyboard = _context.Keyboards.Find(id);
            if (keyboard != null) {
                _context.Keyboards.Remove(keyboard);
            }
        }
        
    }
}