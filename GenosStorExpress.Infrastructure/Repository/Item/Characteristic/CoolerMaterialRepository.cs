using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface.Item.Characteristic;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Item.Characteristic {
    public class CoolerMaterialRepository: ICoolerMaterialRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public CoolerMaterialRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<CoolerMaterial> List() {
            return _context.CoolerMaterials.ToList();
        }

        public CoolerMaterial? Get(int id) {
            return _context.CoolerMaterials.Find(id);
        }

        public void Create(CoolerMaterial coolerMaterial) {
            _context.CoolerMaterials.Add(coolerMaterial);
        }

        public void Update(CoolerMaterial coolerMaterial) {
            _context.Entry(coolerMaterial).State = EntityState.Modified;
        }

        public void Delete(int id) {
            CoolerMaterial? coolerMaterial = _context.CoolerMaterials.Find(id);
            if (coolerMaterial != null) {
                _context.CoolerMaterials.Remove(coolerMaterial);
            }
        }
        
    }
}