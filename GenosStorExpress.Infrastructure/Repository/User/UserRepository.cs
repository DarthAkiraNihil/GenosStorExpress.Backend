using GenosStorExpress.Domain.Interface.User;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.User {
    public class UserRepository: IUserRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public UserRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<Domain.Entity.User.User> List() {
            return _context.Users.ToList();
        }

        public Domain.Entity.User.User? Get(string id) {
            return _context.Users.Find(id);
        }

        public void Create(Domain.Entity.User.User user) {
            _context.Users.Add(user);
        }

        public void Update(Domain.Entity.User.User user) {
            _context.Entry(user).State = EntityState.Modified;
        }

        public void Delete(string id) {
			Domain.Entity.User.User? user = _context.Users.Find(id);
            if (user != null)
                _context.Users.Remove(user);
        }
        
    }
}