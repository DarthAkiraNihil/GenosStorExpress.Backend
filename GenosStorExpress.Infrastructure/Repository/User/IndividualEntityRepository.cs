using GenosStorExpress.Domain.Entity.User;
using GenosStorExpress.Domain.Interface.User;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.User {
    public class IndividualEntityRepository: IIndividualEntityRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public IndividualEntityRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<IndividualEntity> List() {
            return _context.IndividualEntities
                           .Include(i => i.BankCards)
                           .Include(i => i.Orders)
                           .ToList();
        }

        public IndividualEntity? Get(string id) {
            return _context.IndividualEntities
                           .Include(i => i.BankCards)
                           .Include(i => i.Orders)
                           .FirstOrDefault(i => i.Id == id);
        }

        public void Create(IndividualEntity individualEntity) {
            _context.IndividualEntities.Add(individualEntity);
        }

        public void Update(IndividualEntity individualEntity) {
            _context.Entry(individualEntity).State = EntityState.Modified;
        }

        public void Delete(string id) {
            IndividualEntity? individualEntity = _context.IndividualEntities.Find(id);
            if (individualEntity != null) {
                _context.IndividualEntities.Remove(individualEntity);
            }
        }
        
    }
}