using GenosStorExpress.Application.Service.Interface.Entity.Users;
using GenosStorExpress.Domain.Entity.User;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Users {
    public class UserService: IUserService {
        
        private IGenosStorExpressRepositories _repositories;
        
        public UserService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
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

        public List<User> List() {
            return _repositories.Users.Users.List();
        }

        public Administrator? GetAdmin(string id) {
            return _repositories.Users.Administrators.Get(id);
        }
    }
}