using GenosStorExpress.Domain.Interface.Item;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Item {
    public class PreparedAssemblyRepository: IPreparedAssemblyRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public PreparedAssemblyRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<Domain.Entity.Item.PreparedAssembly> List() {
            return _context.PreparedAssemblies.ToList();
        }

        public Domain.Entity.Item.PreparedAssembly? Get(int id) {
            return _context.PreparedAssemblies.Find(id);
        }

        public void Create(Domain.Entity.Item.PreparedAssembly preparedAssembly) {
            _context.PreparedAssemblies.Add(preparedAssembly);
        }

        public void Update(Domain.Entity.Item.PreparedAssembly preparedAssembly) {
            _context.Entry(preparedAssembly).State = EntityState.Modified;
        }

        public void Delete(int id) {
	        Domain.Entity.Item.PreparedAssembly? preparedAssembly = _context.PreparedAssemblies.Find(id);
            if (preparedAssembly != null) {
                _context.PreparedAssemblies.Remove(preparedAssembly);
            }
        }
        
    }
}