using GenosStorExpress.Domain.Entity.User;
using GenosStorExpress.Utils.Operations;


namespace GenosStorExpress.Application.Service.Interface.Entity.Users {
    public interface IUserService:
        ISupportsCreate<User>,
        ISupportsList<User> {
        bool Exists(string email);
        User? FindByEmail(string email);
    }
}