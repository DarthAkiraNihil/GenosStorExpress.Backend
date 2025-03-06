using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface.Item.Characteristic;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Item.Characteristic {
    public class Certificate80PlusRepository: ICertificate80PlusRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public Certificate80PlusRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<Certificate80Plus> List() {
            return _context.Certificates80Plus.ToList();
        }

        public Certificate80Plus Get(int id) {
            return _context.Certificates80Plus.Find(id);
        }

        public void Create(Certificate80Plus certificate80Plus) {
            _context.Certificates80Plus.Add(certificate80Plus);
        }

        public void Update(Certificate80Plus certificate80Plus) {
            _context.Entry(certificate80Plus).State = EntityState.Modified;
        }

        public void Delete(int id) {
            Certificate80Plus certificate80Plus = _context.Certificates80Plus.Find(id);
            if (certificate80Plus != null)
                _context.Certificates80Plus.Remove(certificate80Plus);
        }
        
    }
}