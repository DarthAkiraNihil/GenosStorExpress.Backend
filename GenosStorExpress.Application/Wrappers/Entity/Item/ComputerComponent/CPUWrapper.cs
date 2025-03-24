using GenosStorExpress.Application.Wrappers.Entity.Item.SimpleComputerComponent;

namespace GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;

/// <summary>
/// Класс-обёртка для центрального процессора
/// </summary>
public class CPUWrapper: ComputerComponentWrapper {
    /// <summary>
    /// Ядро процессора
    /// </summary>
    public CPUCoreWrapper Core { get; set; }
    /// <summary>
    /// Сокет процессора
    /// </summary>
    public string Socket { get; set; }
    
    /// <summary>
    /// Количество ядер
    /// </summary>
    public int CoresCount { get; set; }
    /// <summary>
    /// Количество потоков
    /// </summary>
    public int ThreadsCount { get; set; }
    /// <summary>
    /// Размер кэша L2
    /// </summary>
    public float L2CacheSize { get; set; }
    /// <summary>
    /// Размер кэша L3
    /// </summary>
    public float L3CacheSize { get; set; }
    /// <summary>
    /// Техпроцесс
    /// </summary>
    public float TechnicalProcess {  get; set; }
    /// <summary>
    /// Базовая частота
    /// </summary>
    public float BaseFrequency { get; set; }
    /// <summary>
    /// Поддерживаемые типы ОЗУ
    /// </summary>
    public List<string> SupportedRamTypes { get; set; }
    /// <summary>
    /// Поддерживаемый размер ОЗУ
    /// </summary>
    public int SupportedRAMSize { get; set; }
    /// <summary>
    /// Наличие интегрированной графики
    /// </summary>
    public bool HasIntegratedGraphics { get; set; }

    /// <summary>
    /// Стандартный конструктор
    /// </summary>
    public CPUWrapper() {
        SupportedRamTypes = new List<string>();
        Socket = string.Empty;
        Core = new CPUCoreWrapper();
    }
}