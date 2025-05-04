using GenosStorExpress.Domain.Entity.Item.SimpleComputerComponent;
using GenosStorExpress.Domain.Interface.Item.SimpleComputerComponent;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Item.SimpleComputerComponent {
    public class SSDControllerRepository: ISSDControllerRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public SSDControllerRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<SSDController> List() {
            return _context.SSDControllers.ToList();
        }

        public SSDController? Get(int id) {
            return _context.SSDControllers.Find((long) id);
        }

        public void Create(SSDController ssdController) {
            _context.SSDControllers.Add(ssdController);
        }

        public void Update(SSDController ssdController) {
            _context.Entry(ssdController).State = EntityState.Modified;
        }

        public void Delete(int id) {
            SSDController? ssdController = _context.SSDControllers.Find(id);
            if (ssdController != null) {
                _context.SSDControllers.Remove(ssdController);
            }
        }
        
    }
}