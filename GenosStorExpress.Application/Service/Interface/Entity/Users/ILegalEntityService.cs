using GenosStorExpress.Domain.Entity.User;

namespace GenosStorExpress.Application.Service.Interface.Entity.Users {
    public interface ILegalEntityService { 
        void RevokeVerification(User sudo, LegalEntity legalEntity);
        void Verify(User sudo, LegalEntity legalEntity);
        List<LegalEntity> GetVerified(User sudo);
        List<LegalEntity> GetWaitingForVerification(User sudo);
    }
}