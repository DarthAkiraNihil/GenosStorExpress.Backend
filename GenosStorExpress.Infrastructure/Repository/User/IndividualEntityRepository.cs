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
            return _context.IndividualEntities.ToList();
        }

        public IndividualEntity Get(int id) {
            return _context.IndividualEntities.Find(id);
        }

        public void Create(IndividualEntity individualEntity) {
            _context.IndividualEntities.Add(individualEntity);
        }

        public void Update(IndividualEntity individualEntity) {
            _context.Entry(individualEntity).State = EntityState.Modified;
        }

        public void Delete(int id) {
            IndividualEntity individualEntity = _context.IndividualEntities.Find(id);
            if (individualEntity != null)
                _context.IndividualEntities.Remove(individualEntity);
        }
        
    }
}