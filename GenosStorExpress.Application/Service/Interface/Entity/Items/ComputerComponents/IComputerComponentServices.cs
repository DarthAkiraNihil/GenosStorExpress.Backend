namespace GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents {
    /// <summary>
    /// Интерфейс для сервиса, объединяющего все сервисы компьютерных комплектующих
    /// </summary>
    public interface IComputerComponentServices {
        /// <summary>
        /// Сервис компьютерных корпусов
        /// </summary>
        IComputerCaseService ComputerCases { get; }
        /// <summary>
        /// Сервис кулеров для процессора
        /// </summary>
        ICPUCoolerService CPUCoolers { get; }
        /// <summary>
        /// Сервис центральных процессоров
        /// </summary>
        ICPUService CPUs { get; }
        //IDiskDriveService DiskDrives { get; }
        /// <summary>
        /// Сервис мониторов
        /// </summary>
        IDisplayService Displays { get; }
        /// <summary>
        /// Сервис видеокарт
        /// </summary>
        IGraphicsCardService GraphicsCards { get; }
        /// <summary>
        /// Сервис жёстких дисков
        /// </summary>
        IHDDService HDDs { get; }
        /// <summary>
        /// Сервис клавиатур
        /// </summary>
        IKeyboardService Keyboards { get; }
        /// <summary>
        /// Сервис материнских плат
        /// </summary>
        IMotherboardService Motherboards { get; }
        /// <summary>
        /// Сервис мышей
        /// </summary>
        IMouseService Mouses { get; }
        /// <summary>
        /// Сервис твердотельных накопителей NVMe
        /// </summary>
        INVMeSSDService NVMeSSDs { get; }
        /// <summary>
        /// Сервис блоков питания
        /// </summary>
        IPowerSupplyService PowerSupplies { get; }
        /// <summary>
        /// Сервис ОЗУ
        /// </summary>
        IRAMService RAMs { get; }
        /// <summary>
        /// Сервис твердотельных накопителей Sata
        /// </summary>
        ISataSSDService SataSSDs { get; }
    }
}