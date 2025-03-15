using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface.Item.Characteristic;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Item.Characteristic {
    public class VendorRepository: IVendorRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public VendorRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<Vendor> List() {
            return _context.Vendors.ToList();
        }

        public Vendor? Get(int id) {
            return _context.Vendors.Find(id);
        }

        public void Create(Vendor vendor) {
            _context.Vendors.Add(vendor);
        }

        public void Update(Vendor vendor) {
            _context.Entry(vendor).State = EntityState.Modified;
        }

        public void Delete(int id) {
            Vendor? vendor = _context.Vendors.Find(id);
            if (vendor != null) {
                _context.Vendors.Remove(vendor);
            }
        }
        
    }
}