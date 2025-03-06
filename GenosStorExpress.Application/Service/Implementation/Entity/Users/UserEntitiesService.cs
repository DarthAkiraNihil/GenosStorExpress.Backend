using GenosStorExpress.Application.Service.Interface.Entity.Users;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Users {
    public class UserEntitiesService: IUserEntitiesService {
        private readonly ILegalEntityService _legalEntityService;

        public UserEntitiesService(ILegalEntityService legalEntityService) {
            _legalEntityService = legalEntityService;
        }

        public ILegalEntityService LegalEntities => _legalEntityService;
    }
}