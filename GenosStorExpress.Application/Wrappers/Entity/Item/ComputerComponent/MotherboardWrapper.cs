using GenosStorExpress.Application.Wrappers.Entity.Item.SimpleComputerComponent;

namespace GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;

/// <summary>
/// Класс-обёртка для материнской платы
/// </summary>
public class MotherboardWrapper: ComputerComponentWrapper {
	/// <summary>
	/// Поддерживаемые ядра процессоров
	/// </summary>
    public IList<CPUCoreWrapper> SupportedCPUCores { get; set; }
	/// <summary>
	/// Поддерживаемые типы ОЗУ
	/// </summary>
    public IList<string> SupportedRAMTypes { get; set; }
	/// <summary>
	/// Количество слотов ОЗУ
	/// </summary>
    
    public byte RAMSlots { get; set; }
	/// <summary>
	/// Количество каналов ОЗУ
	/// </summary>
    public byte RAMChannels { get; set; }
	/// <summary>
	/// Максимальная частота ОЗУ
	/// </summary>
    public int MaxRAMFrequency { get; set; }
	/// <summary>
	/// Количество слотов PCI-e
	/// </summary>
    public byte PCIESlotsCount { get; set; }
	/// <summary>
	/// Наличие поддержки NVMe
	/// </summary>
    
    public bool HasNVMeSupport { get; set; }
	/// <summary>
	/// Количество слотов M2
	/// </summary>
    public byte M2SlotsCount { get; set; }
    /// <summary>
    /// Количество портов Sata
    /// </summary>
    public byte SataPortsCount { get; set; }
    /// <summary>
    /// Количество портов USB
    /// </summary>
    public byte USBPortsCount { get; set; }
    /// <summary>
    /// Количество видеопортов
    /// </summary>
    public List<string> VideoPorts { get; set; }
    /// <summary>
    /// Количество портов RJ-45
    /// </summary>
    public byte RJ45PortsCount { get; set; }
    /// <summary>
    /// Количество цифровых аудиопортор
    /// </summary>
    public byte DigitalAudioPortsCount { get; set; }
    /// <summary>
    /// Скорость сетевого адаптера
    /// </summary>

    public float NetworkAdapterSpeed { get; set; }
    /// <summary>
    /// Форм-фактор
    /// </summary>
		
    public string FormFactor { get; set; }
    /// <summary>
    /// Сокет процессора
    /// </summary>
    public string CPUSocket { get; set; }
    /// <summary>
    /// Версия PCI-e
    /// </summary>
    public string PCIEVersion { get; set; }
    /// <summary>
    /// Чипсет материнской платы
    /// </summary>
		
    public MotherboardChipsetWrapper MotherboardChipset { get; set; }
    /// <summary>
    /// Аудиочипсет
    /// </summary>
    public AudioChipsetWrapper AudioChipset { get; set; }
    /// <summary>
    /// Сетевой адаптер
    /// </summary>
    public NetworkAdapterWrapper NetworkAdapter { get; set; }
    /// <summary>
    /// Стандартный конструктор
    /// </summary>

    public MotherboardWrapper() {
	    SupportedCPUCores = new List<CPUCoreWrapper>();
	    SupportedRAMTypes = new List<string>();
	    VideoPorts = new List<string>();
	    
	    FormFactor = string.Empty;
	    CPUSocket = string.Empty;
	    PCIEVersion = string.Empty;
	    
	    MotherboardChipset = new MotherboardChipsetWrapper();
	    AudioChipset = new AudioChipsetWrapper();
	    NetworkAdapter = new NetworkAdapterWrapper();
	    
    }
}