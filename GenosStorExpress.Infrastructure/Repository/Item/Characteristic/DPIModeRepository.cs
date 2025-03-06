using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface.Item.Characteristic;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Item.Characteristic {
    public class DPIModeRepository: IDPIModeRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public DPIModeRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<DPIMode> List() {
            return _context.DPIModes.ToList();
        }

        public DPIMode Get(int id) {
            return _context.DPIModes.Find(id);
        }

        public void Create(DPIMode dpiMode) {
            _context.DPIModes.Add(dpiMode);
        }

        public void Update(DPIMode dpiMode) {
            _context.Entry(dpiMode).State = EntityState.Modified;
        }

        public void Delete(int id) {
            DPIMode dpiMode = _context.DPIModes.Find(id);
            if (dpiMode != null)
                _context.DPIModes.Remove(dpiMode);
        }
        
    }
}