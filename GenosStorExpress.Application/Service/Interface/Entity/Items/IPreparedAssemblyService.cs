using GenosStorExpress.Application.Service.Interface.Base;
using GenosStorExpress.Application.Wrappers.Entity.Item.PreaparedAssembly;

namespace GenosStorExpress.Application.Service.Interface.Entity.Items {
    /// <summary>
    /// Интерфейс сервиса готовых сборов
    /// </summary>
    public interface IPreparedAssemblyService : IStandardService<PreparedAssemblyWrapper>;
}