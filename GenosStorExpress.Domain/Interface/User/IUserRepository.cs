using GenosStorExpress.Domain.Repository.Base;
using GenosStorExpress.Utils.Operations;

namespace GenosStorExpress.Domain.Interface.User {
    public interface IUserRepository:
        ISupportsCreate<Entity.User.User>,
        ISupportsGetByStringId<Entity.User.User>,
        ISupportsList<Entity.User.User>,
        ISupportsUpdate<Entity.User.User>,
        ISupportsDeleteByStringId {
		
    }
}