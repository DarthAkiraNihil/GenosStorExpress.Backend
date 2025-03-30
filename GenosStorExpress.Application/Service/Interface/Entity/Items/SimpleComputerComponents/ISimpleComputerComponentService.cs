namespace GenosStorExpress.Application.Service.Interface.Entity.Items.SimpleComputerComponents {
    /// <summary>
    /// Интерфейс для сервиса, объединяющего все сервисы простых компьютерных компонентов
    /// </summary>
    public interface ISimpleComputerComponentService {
        /// <summary>
        /// Сервис контроллеров твердотельных накопителей
        /// </summary>
        ISSDControllerService SSDControllers { get; }
        /// <summary>
        /// Сервис типов простых компьютерных компонентов
        /// </summary>
        ISimpleComputerComponentTypeService Types { get; }
        /// <summary>
        /// Сервис сетевых адаптеров
        /// </summary>
        INetworkAdapterService NetworkAdapters { get; }
        /// <summary>
        /// Сервис чипсетов материнских плат
        /// </summary>
        IMotherboardChipsetService MotherboardChipsets { get; }
        /// <summary>
        /// Сервис графических процессоров
        /// </summary>
        IGPUService GPUs { get; }
        /// <summary>
        /// Сервис ядер процессоров
        /// </summary>
        ICPUCoreService CPUCores { get; }
        /// <summary>
        /// Сервис аудио чиспетов
        /// </summary>
        IAudioChipsetService AudioChipsets { get; }
    }
}