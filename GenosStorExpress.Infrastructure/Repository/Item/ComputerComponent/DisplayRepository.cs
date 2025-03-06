using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Item.ComputerComponent {
    public class DisplayRepository: IDisplayRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public DisplayRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<Display> List() {
            return _context.Displays.ToList();
        }

        public Display Get(int id) {
            return _context.Displays.Find(id);
        }

        public void Create(Display display) {
            _context.Displays.Add(display);
        }

        public void Update(Display display) {
            _context.Entry(display).State = EntityState.Modified;
        }

        public void Delete(int id) {
            Display display = _context.Displays.Find(id);
            if (display != null)
                _context.Displays.Remove(display);
        }
        
    }
}