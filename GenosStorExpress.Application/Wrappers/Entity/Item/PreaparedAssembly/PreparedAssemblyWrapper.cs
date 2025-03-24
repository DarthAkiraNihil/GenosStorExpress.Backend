namespace GenosStorExpress.Application.Wrappers.Entity.Item.PreaparedAssembly;

/// <summary>
/// Класс-обёртка для готовой сборки
/// </summary>
public class PreparedAssemblyWrapper: ItemWrapper {
    /// <summary>
    /// Список ОЗУ
    /// </summary>
    
    public IList<PreparedAssemblyItemWrapper> RAMs { get; set; }
    /// <summary>
    /// Список дисков
    /// </summary>
    public IList<PreparedAssemblyDiskDriveWrapper> DiskDrives { get; set; }
    /// <summary>
    /// Центральный процессор
    /// </summary>
    public PreparedAssemblyItemWrapper CPU { get; set; }
    /// <summary>
    /// Материнская плата
    /// </summary>
    public PreparedAssemblyItemWrapper Motherboard { get; set; }
    /// <summary>
    /// Видеокарта
    /// </summary>
    public PreparedAssemblyItemWrapper GraphicsCard { get; set; }
    /// <summary>
    /// Блок питания
    /// </summary>
    public PreparedAssemblyItemWrapper PowerSupply { get; set; }
    /// <summary>
    /// Монитор. Не обязателен
    /// </summary>
    public PreparedAssemblyItemWrapper? Display { get; set; }
    /// <summary>
    /// Корпус
    /// </summary>
    public PreparedAssemblyItemWrapper ComputerCase { get; set; }
    /// <summary>
    /// Клавиатура. Не обязательна
    /// </summary>
    public PreparedAssemblyItemWrapper? Keyboard { get; set; }
    /// <summary>
    /// Мышь. Не обязательна
    /// </summary>
    public PreparedAssemblyItemWrapper? Mouse { get; set; }
    /// <summary>
    /// Кулер для процессора
    /// </summary>
    public PreparedAssemblyItemWrapper CPUCooler { get; set; }
    /// <summary>
    /// Стандартный конструктор
    /// </summary>

    public PreparedAssemblyWrapper() {
        RAMs = new List<PreparedAssemblyItemWrapper>();
        DiskDrives = new List<PreparedAssemblyDiskDriveWrapper>();
        CPU = new PreparedAssemblyItemWrapper();
        Motherboard = new PreparedAssemblyItemWrapper();
        GraphicsCard = new PreparedAssemblyItemWrapper();
        PowerSupply = new PreparedAssemblyItemWrapper();
        ComputerCase = new PreparedAssemblyItemWrapper();
        CPUCooler = new PreparedAssemblyItemWrapper();
    }
}