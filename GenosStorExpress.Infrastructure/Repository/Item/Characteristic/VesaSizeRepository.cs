using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface.Item.Characteristic;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Item.Characteristic {
    public class VesaSizeRepository: IVesaSizeRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public VesaSizeRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<VesaSize> List() {
            return _context.VesaSizes.ToList();
        }

        public VesaSize? Get(int id) {
            return _context.VesaSizes.Find(id);
        }

        public void Create(VesaSize vesaSize) {
            _context.VesaSizes.Add(vesaSize);
        }

        public void Update(VesaSize vesaSize) {
            _context.Entry(vesaSize).State = EntityState.Modified;
        }

        public void Delete(int id) {
            VesaSize? vesaSize = _context.VesaSizes.Find(id);
            if (vesaSize != null) {
                _context.VesaSizes.Remove(vesaSize);
            }
        }
        
    }
}