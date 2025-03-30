using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Application.Service.Interface.Entity.Items.SimpleComputerComponents;

namespace GenosStorExpress.Application.Service.Interface.Entity.Items {
    /// <summary>
    /// Интерфейс сервиса, объединяющего все сервисы товаров 
    /// </summary>
    public interface IItemServices {
        /// <summary>
        /// Сервис характеристик товаров
        /// </summary>
		ICharacteristicsService Characteristics { get; }
        /// <summary>
        /// Сервис компьютерных комплектующих
        /// </summary>
        IComputerComponentServices ComputerComponents { get; }
        /// <summary>
        /// Сервис простых компьютерных компонентов
        /// </summary>
        ISimpleComputerComponentService SimpleComputerComponents { get; }
        /// <summary>
        /// Сервис типов товаров
        /// </summary>
        IItemTypeService ItemTypes { get; }
        /// <summary>
        /// Сервис готовых сборов
        /// </summary>
        IPreparedAssemblyService PreparedAssemblies { get; }
        /// <summary>
        /// Общий сервис товаров
        /// </summary>
        IAllItemsService All { get; }
    }
}