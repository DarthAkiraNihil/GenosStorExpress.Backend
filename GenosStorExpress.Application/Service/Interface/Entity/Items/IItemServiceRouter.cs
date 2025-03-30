using GenosStorExpress.Application.Wrappers.Entity.Item;
using GenosStorExpress.Application.Wrappers.Enum;

namespace GenosStorExpress.Application.Service.Interface.Entity.Items;

/// <summary>
/// Интерфейс сервиса-маршрутизатора сервисов товаров
/// </summary>
public interface IItemServiceRouter {
    /// <summary>
    /// Получение информации о товаре по его типу по номеру
    /// </summary>
    /// <param name="itemType">Дескриптор типа товара</param>
    /// <param name="id">Номер товара</param>
    /// <returns>Анонимную обёртку товара</returns>
    AnonymousItemWrapper Get(ItemTypeDescriptor itemType, int id);
    /// <summary>
    /// Получение списка товаров по дескриптору
    /// </summary>
    /// <param name="itemType">Дескриптор типа товара</param>
    /// <returns>Список товаров указанного типа</returns>
    IList<AnonymousItemWrapper> List(ItemTypeDescriptor itemType);
    /// <summary>
    /// Создание товара по его анонимной обёртке
    /// </summary>
    /// <param name="itemType">Дескриптор типа товара</param>
    /// <param name="item">Анонимная обёртка создаваемого товара</param>
    void Create(ItemTypeDescriptor itemType, AnonymousItemWrapper item);
    /// <summary>
    /// Обновление информации о товаре
    /// </summary>
    /// <param name="itemType">Дескриптор типа товара</param>
    /// <param name="id">Номер обновляемого товара</param>
    /// <param name="item">Анонимная обёртка с обновлёнными данными</param>
    void Update(ItemTypeDescriptor itemType, int id, AnonymousItemWrapper item);
    /// <summary>
    /// Удаление товара
    /// </summary>
    /// <param name="itemType">Дескриптор типа товара</param>
    /// <param name="id">Номер удаляемого товара</param>
    void Delete(ItemTypeDescriptor itemType, int id);
    /// <summary>
    /// Сохранение контекста
    /// </summary>
    /// <param name="itemType">Дескриптор типа товара</param>
    void Save(ItemTypeDescriptor itemType);
}