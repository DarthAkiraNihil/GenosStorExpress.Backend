using GenosStorExpress.Domain.Interface.User;
using GenosStorExpress.Infrastructure.Context;

namespace GenosStorExpress.Infrastructure.Repository.User {
    public class UserEntitiesRepository: IUserEntitiesRepository {
        private GenosStorExpressDatabaseContext _context;
        // User
        private AdministratorRepository _administrators;
        //private CustomerRepository _Customers;
        private IndividualEntityRepository _individualEntities;
        private LegalEntityRepository _legalEntities;
        private UserRepository _users;

        public UserEntitiesRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }
        
        // User
        public IAdministratorRepository Administrators {
            get {
                if (_administrators == null) {
                    _administrators = new AdministratorRepository(_context);
                }
                return _administrators;
            }
        }
        // public ICustomerRepository Customers {
        //     get {
        //         if (_certificates80Plus == null) {
        //             _certificates80Plus = new Certificate80PlusRepository(_context);
        //         }
        //         return _certificates80Plus;
        //     }
        // }
        public IIndividualEntityRepository IndividualEntities {
            get {
                if (_individualEntities == null) {
                    _individualEntities = new IndividualEntityRepository(_context);
                }
                return _individualEntities;
            }
        }
        public ILegalEntityRepository LegalEntities {
            get {
                if (_legalEntities == null) {
                    _legalEntities = new LegalEntityRepository(_context);
                }
                return _legalEntities;
            }
        }
        public IUserRepository Users {
            get {
                if (_users == null) {
                    _users = new UserRepository(_context);
                }
                return _users;
            }
        }
    }
}