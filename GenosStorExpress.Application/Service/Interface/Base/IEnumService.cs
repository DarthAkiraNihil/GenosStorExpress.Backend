namespace GenosStorExpress.Application.Service.Interface.Base {
    public interface IEnumService<T> : IStandardService<string> where T: class{
        bool BelongsToEnum(string value);
        T? GetEntityFromString(string value);
    }
}