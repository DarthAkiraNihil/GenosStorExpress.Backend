using GenosStorExpress.Domain.Entity.User;
using GenosStorExpress.Domain.Interface.User;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.User {
    public class AdministratorRepository: IAdministratorRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public AdministratorRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<Administrator> List() {
            return _context.Administrators.ToList();
        }

        public Administrator? Get(string id) {
            return _context.Administrators.Find(id);
        }

        public void Create(Administrator administrator) {
            _context.Administrators.Add(administrator);
        }

        public void Update(Administrator administrator) {
            _context.Entry(administrator).State = EntityState.Modified;
        }

        public void Delete(string id) {
            Administrator? administrator = _context.Administrators.Find(id);
            if (administrator != null) {
                _context.Administrators.Remove(administrator);
            }
        }
        
    }
}