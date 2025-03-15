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
            return _context.LegalEntities
                           .Include(i => i.BankCards)
                           .Include(i => i.Orders)
                           .ToList();
        }

        public LegalEntity? Get(string id) {
            return _context.LegalEntities
                           .Include(i => i.BankCards)
                           .Include(i => i.Orders)
                           .FirstOrDefault(i => i.Id == id);
        }

        public void Create(LegalEntity legalEntity) {
            _context.LegalEntities.Add(legalEntity);
        }

        public void Update(LegalEntity legalEntity) {
            _context.Entry(legalEntity).State = EntityState.Modified;
        }

        public void Delete(string id) {
            LegalEntity? legalEntity = _context.LegalEntities.Find(id);
            if (legalEntity != null) {
                _context.LegalEntities.Remove(legalEntity);
            }
        }
        
    }
}