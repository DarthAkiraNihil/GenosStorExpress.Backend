using GenosStorExpress.Application.Service.Interface.Base;
using GenosStorExpress.Domain.Entity.Item.Characteristic;

namespace GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics {
    /// <summary>
    /// Интерфейс сервиса форм-факторов материнских плат
    /// </summary>
    public interface IMotherboardFormFactorService : IEnumService<MotherboardFormFactor>;
}