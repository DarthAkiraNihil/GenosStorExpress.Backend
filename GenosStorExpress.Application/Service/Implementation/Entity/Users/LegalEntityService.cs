using GenosStorExpress.Application.Service.Interface.Entity.Users;
using GenosStorExpress.Domain.Entity.User;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Users {
    public class LegalEntityService: ILegalEntityService {
        
        private readonly IGenosStorExpressRepositories _repositories;

        public LegalEntityService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }

        public void RevokeVerification(User sudo, LegalEntity legalEntity) {
            foreach (var bankCard in legalEntity.BankCards) {
                _repositories.Orders.BankCards.Delete(bankCard.Id);
            }
            _repositories.Orders.Carts.DeleteRaw(legalEntity.Cart);
            _repositories.Users.LegalEntities.Delete(legalEntity.Id);
            _repositories.Save();
        }

        public void Verify(User sudo, LegalEntity legalEntity) {
            legalEntity.IsVerified = true;
            _repositories.Users.LegalEntities.Update(legalEntity);
            _repositories.Save();
        }

        public List<LegalEntity> GetVerified(User sudo) {
            if (!(sudo is Administrator)) {
                throw new UnauthorizedAccessException("ACCESS DENIED!");
            }
            
            return _repositories
                   .Users
                   .LegalEntities
                   .List()
                   .Where(e => e.IsVerified)
                   .ToList();
        }

        public List<LegalEntity> GetWaitingForVerification(User sudo) {
            
            if (!(sudo is Administrator)) {
                throw new UnauthorizedAccessException("ACCESS DENIED!");
            }
            
            return _repositories
                   .Users
                   .LegalEntities
                   .List()
                   .Where(e => !e.IsVerified)
                   .ToList();
        }
    }
}