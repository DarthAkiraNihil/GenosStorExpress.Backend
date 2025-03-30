using GenosStorExpress.Application.Service.Interface.Base;
using GenosStorExpress.Application.Wrappers.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Utils.Operations;

namespace GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics {

    /// <summary>
    /// Интерфейс сервиса разрешений мониторов
    /// </summary>
    public interface IDefinitionService :
        IStandardService<DefinitionWrapper>,
        ISupportsGetRaw<Definition>;
}