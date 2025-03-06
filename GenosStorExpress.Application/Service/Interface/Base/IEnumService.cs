using GenosStorExpress.Utils.Operations;

namespace GenosStorExpress.Application.Service.Interface.Base {
    public interface IEnumService : IStandardService<string> {
        bool BelongsToEnum(string value);
    }
}