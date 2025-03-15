using GenosStorExpress.Application.Service.Interface.Entity.Users;
using GenosStorExpress.Domain.Entity.User;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Users {
    public class UserService: IUserService {
        
        private IGenosStorExpressRepositories _repositories;
        
        public UserService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }

        public bool Exists(string email) {
            return _repositories
                   .Users
                   .Users
                   .List()
                   .Where(u => u.Email == email)
                   .Count() > 0;
        }

        public void Create(User item) {
            switch (item.UserType) {
                case UserType.IndividualEntity: {
                    _repositories
                        .Users
                        .IndividualEntities
                        .Create((IndividualEntity)item);
                    break;
                }
                    
                case UserType.LegalEntity: {
                    _repositories
                        .Users
                        .LegalEntities
                        .Create((LegalEntity)item);
                    break;
                }
                    
                case UserType.Administrator: {
                    _repositories
                        .Users
                        .Administrators
                        .Create((Administrator)item);
                    break;
                }
            }
        }

        public User? FindByEmail(string email) {
            return _repositories
                         .Users
                         .Users
                         .List()
                         .Where(u => u.Email == email)
                         .FirstOrDefault();
        }

        public List<User> List() {
            return _repositories.Users.Users.List();
        }
    }
}