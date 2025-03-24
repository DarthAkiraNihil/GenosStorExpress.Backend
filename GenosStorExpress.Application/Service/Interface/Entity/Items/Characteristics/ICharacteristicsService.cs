namespace GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics {
    
    /// <summary>
    /// Интерфейс для сервиса, объединяющего все сервисы характеристик
    /// </summary>
    public interface ICharacteristicsService {
        /// <summary>
        /// Сервис сертификатов 80Plus
        /// </summary>
        ICertificate80PlusService Certificates80Plus { get; }
        /// <summary>
        /// Сервис типоразмеров компьютерных корпусов
        /// </summary>
        IComputerCaseTypesizeService ComputerCaseTypesizes { get; }
        /// <summary>
        /// Сервис материалов для кулера
        /// </summary>
        ICoolerMaterialService CoolerMaterials { get; }
        /// <summary>
        /// Сервис сокетов процессоров
        /// </summary>
        ICPUSocketService CPUSockets { get; }
        /// <summary>
        /// Сервис разрешений мониторов
        /// </summary>
        IDefinitionService Definitions { get; }
        /// <summary>
        /// Сервис режимов DPI
        /// </summary>
        IDPIModeService DPIModes { get; }
        /// <summary>
        /// Сервис типов клавиатур
        /// </summary>
        IKeyboardTypeService KeyboardTypes { get; }
        /// <summary>
        /// Сервис типоразмеров клавиатур
        /// </summary>
        IKeyboardTypesizeService KeyboardTypesizes { get; }
        /// <summary>
        /// Сервис типов матриц
        /// </summary>
        IMatrixTypeService MatrixTypes { get; }
        /// <summary>
        /// Сервис форм-факторов материнских плат
        /// </summary>
        IMotherboardFormFactorService MotherboardFormFactors { get; }
        /// <summary>
        /// Сервис типов ОЗУ
        /// </summary>
        IRAMTypeService RAMTypes { get; }
        /// <summary>
        /// Сервис подсветок мониторов
        /// </summary>
        IUnderlightService Underlights { get; }
        /// <summary>
        /// Сервис производителей
        /// </summary>
        IVendorService Vendors { get; }
        /// <summary>
        /// Сервис размеров Vesa
        /// </summary>
        IVesaSizeService VesaSizes { get; }
        /// <summary>
        /// Сервис видеопортов
        /// </summary>
        IVideoPortService VideoPorts { get; }
    }
}