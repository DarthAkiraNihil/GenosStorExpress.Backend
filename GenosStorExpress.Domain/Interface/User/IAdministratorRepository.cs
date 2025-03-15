using GenosStorExpress.Domain.Entity.User;
using GenosStorExpress.Utils.Operations;

namespace GenosStorExpress.Domain.Interface.User {
    public interface IAdministratorRepository:
        ISupportsCreate<Administrator>,
        ISupportsGetByStringId<Administrator>,
        ISupportsList<Administrator>,
        ISupportsUpdate<Administrator>,
        ISupportsDeleteByStringId {
		
    }
}