using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Item.ComputerComponent {
    public class MouseRepository: IMouseRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public MouseRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<Mouse> List() {
            return _context.Mouses
                           .Include(i => i.Reviews)
                           .Include(i => i.DPIModes)
                           .ToList();
        }

        public Mouse? Get(int id) {
            return _context.Mouses
                           .Include(i => i.Reviews)
                           .Include(i => i.DPIModes)
                           .FirstOrDefault(i => i.Id == id);
        }

        public void Create(Mouse mouse) {
            _context.Mouses.Add(mouse);
        }

        public void Update(Mouse mouse) {
            _context.Entry(mouse).State = EntityState.Modified;
        }

        public void Delete(int id) {
            Mouse? mouse = _context.Mouses.Find(id);
            if (mouse != null) {
                _context.Mouses.Remove(mouse);
            }
        }
    }
}