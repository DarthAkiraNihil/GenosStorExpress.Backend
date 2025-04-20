using System.Collections;
using GenosStorExpress.Application.Wrappers.Entity.Item;
using GenosStorExpress.Application.Wrappers.Enum;
using GenosStorExpress.Application.Wrappers.Filters;

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
    /// <param name="pageNumber">Номер страницы</param>
    /// <param name="pageSize">Размер страницы</param>
    /// <returns>Список товаров указанного типа</returns>
    PaginatedAnonymousItemWrapper List(ItemTypeDescriptor itemType, int pageNumber, int pageSize);
    /// <summary>
    /// Фильтрация списка товаров по критериям
    /// </summary>
    /// <param name="itemType">Дескриптор типа товаров</param>
    /// <param name="filters">Фильтры</param>
    /// <param name="pageNumber">Номер страницы</param>
    /// <param name="pageSize">Размер страницы</param>
    /// <returns>Отфильтрованный список</returns>
    PaginatedAnonymousItemWrapper Filter(ItemTypeDescriptor itemType, IDictionary<string, dynamic> filters, int pageNumber, int pageSize);
    /// <summary>
    /// Получение информации о возможных фильтрах для данного типа товара
    /// </summary>
    /// <param name="itemType">Дескриптор типа товаров</param>
    /// <returns>Список возможных фильтров</returns>
    IList<FilterDescription> FilterData(ItemTypeDescriptor itemType);
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