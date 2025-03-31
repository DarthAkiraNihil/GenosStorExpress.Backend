using GenosStorExpress.Application.Service.Interface.Entity.Items.SimpleComputerComponents;
using GenosStorExpress.Application.Wrappers.Entity.Item.SimpleComputerComponent;
using GenosStorExpress.Domain.Entity.Item.SimpleComputerComponent;

namespace GenosStorExpress.Application.Service.Implementation.Base;

/// <summary>
/// Абстрактный класс, предоставляющий методы, общие для всех сервисов простых компьютерных компонентов
/// </summary>
public abstract class AbstractSimpleComputerComponentService {
    private readonly ISimpleComputerComponentTypeService _simpleComputerComponentTypeService;

    /// <summary>
    /// Стандартный конструктор
    /// </summary>
    /// <param name="simpleComputerComponentTypeService">Сервис типов компьютерных компонентов</param>
    protected AbstractSimpleComputerComponentService(ISimpleComputerComponentTypeService simpleComputerComponentTypeService) {
        _simpleComputerComponentTypeService = simpleComputerComponentTypeService;
    }
    
    /// <summary>
    /// Установка свойств сущности, общих с обёрткой
    /// </summary>
    /// <param name="entity">Обёрнутая сущность, свойства которой устанавливаются</param>
    /// <param name="wrapper">Обёртка сущности</param>
    /// <exception cref="NullReferenceException">Если в обёртке указан несуществующий тип компонента</exception>
    protected void _setEntityPropertiesFromWrapper(SimpleComputerComponent entity, SimpleComputerComponentWrapper wrapper) {
        entity.Name = wrapper.Name;
        entity.Model = wrapper.Model;
        
        var type = _simpleComputerComponentTypeService.GetEntityFromString(wrapper.Type);
        if (type == null) {
            throw new NullReferenceException($"Типа компонента {wrapper.Type} не существует");
        }

        entity.Type = type;
    }

    /// <summary>
    /// Установка свойств обёртки, общих с сущностью
    /// </summary>
    /// <param name="entity">Оборачиваемая сущность</param>
    /// <param name="wrapper">Обёртка, свойства которой устанавливаются</param>
    protected void _setWrapperPropertiesFromEntity(SimpleComputerComponent entity, SimpleComputerComponentWrapper wrapper) {
        wrapper.Id = entity.Id;
        wrapper.Name = entity.Name;
        wrapper.Model = entity.Model;
        wrapper.Type = entity.Type.Name;
    }
}