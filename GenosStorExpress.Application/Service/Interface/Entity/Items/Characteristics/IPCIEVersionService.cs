using GenosStorExpress.Application.Service.Interface.Base;
using GenosStorExpress.Domain.Entity.Item.Characteristic;

namespace GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics {
    /// <summary>
    /// Интерфейс сервиса версий PCI-e
    /// </summary>
    public interface IPCIEVersionService : IEnumService<PCIEVersion>;
}