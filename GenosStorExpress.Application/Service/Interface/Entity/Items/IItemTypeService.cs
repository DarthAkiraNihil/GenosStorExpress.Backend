using GenosStorExpress.Application.Service.Interface.Base;
using GenosStorExpress.Application.Wrappers.Enum;
using GenosStorExpress.Domain.Entity.Item;

namespace GenosStorExpress.Application.Service.Interface.Entity.Items {
    /// <summary>
    /// Интерфейс сервиса типов товаров
    /// </summary>
    public interface IItemTypeService: IEnumService<ItemType> {
        /// <summary>
        /// Получение дескриптора товара по его названию
        /// </summary>
        /// <param name="name">Название типа товара</param>
        /// <returns>Дескриптор типа товара</returns>
        ItemTypeDescriptor GetDescriptor(string name);
    }
}