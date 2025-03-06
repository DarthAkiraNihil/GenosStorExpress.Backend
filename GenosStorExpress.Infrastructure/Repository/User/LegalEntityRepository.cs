using GenosStorExpress.Domain.Entity.User;
using GenosStorExpress.Domain.Interface.User;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.User {
    public class LegalEntityRepository: ILegalEntityRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public LegalEntityRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<LegalEntity> List() {
            return _context.LegalEntities.ToList();
        }

        public LegalEntity Get(int id) {
            return _context.LegalEntities.Find(id);
        }

        public void Create(LegalEntity legalEntity) {
            _context.LegalEntities.Add(legalEntity);
        }

        public void Update(LegalEntity legalEntity) {
            _context.Entry(legalEntity).State = EntityState.Modified;
        }

        public void Delete(int id) {
            LegalEntity legalEntity = _context.LegalEntities.Find(id);
            if (legalEntity != null)
                _context.LegalEntities.Remove(legalEntity);
        }
        
    }
}