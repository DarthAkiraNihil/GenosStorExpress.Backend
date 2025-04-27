using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Wrappers.Entity.Item;
using GenosStorExpress.Application.Wrappers.Entity.Item.Characteristic;
using GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;
using GenosStorExpress.Application.Wrappers.Entity.Item.SimpleComputerComponent;
using System.Text.Json;
using GenosStorExpress.Application.Wrappers.Entity.Item.PreaparedAssembly;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items;

/// <summary>
/// Реализация сервиса-конвертера (сущность товара -> обёртка и наоборот).
/// Используется для обеспечения корректного вызова всех сервисов товаров, так как извне приходят изначально
/// данные, структура которых заранее неизвестна, с чем сервисы не работают
/// </summary>
public class ItemBuilderService: IItemBuilderService {

    private void _buildBase(ItemWrapper target, AnonymousItemWrapper source) {
        target.Id = source.Id;
        target.Name = source.Name;
        target.Model = source.Model;
        target.Description = source.Description;
        target.ItemType = source.ItemType;
        target.Price = source.Price;
    }

    private void _buildBaseAnonymous(AnonymousItemWrapper target, ItemWrapper source) {
        target.Id = source.Id;
        target.Name = source.Name;
        target.Model = source.Model;
        target.Description = source.Description;
        target.ItemType = source.ItemType;
        target.Price = source.Price;
        target.OverallRating = source.OverallRating;
        target.ActiveDiscount = source.ActiveDiscount;
    }

    private void _buildComputerComponent(ComputerComponentWrapper target, AnonymousItemWrapper source) {
        target.Vendor = source.Characteristics["vendor"].GetString();
        target.TDP = source.Characteristics["tdp"].GetDouble();
    }
    
    private void _buildComputerComponentAnonymous(AnonymousItemWrapper target, ComputerComponentWrapper source) {
        target.Characteristics.Add("vendor", source.Vendor);
        target.Characteristics.Add("tdp", source.TDP);
    }
    
    private void _buildDiskDrive(DiskDriveWrapper target, AnonymousItemWrapper source) {
        target.Capacity = source.Characteristics["capacity"].GetInt64();
        target.ReadSpeed = source.Characteristics["read_speed"].GetInt64();
        target.WriteSpeed = source.Characteristics["write_speed"].GetInt64();
    }
    
    private void _buildDiskDriveAnonymous(AnonymousItemWrapper target, DiskDriveWrapper source) {
        target.Characteristics.Add("capacity", source.Capacity);
        target.Characteristics.Add("read_speed", source.ReadSpeed);
        target.Characteristics.Add("write_speed", source.WriteSpeed);
    }
    
    private void _buildSSD(SSDWrapper target, AnonymousItemWrapper source) {
        target.TBW = source.Characteristics["tbw"].GetInt32();
        target.DWPD = source.Characteristics["dwpd"].GetFloat();
        target.BitsForCell = source.Characteristics["bits_for_cell"].GetByte();

        var controller = source.Characteristics["controller"];

        target.Controller = new SSDControllerWrapper {
            Id = controller.GetProperty("id").GetInt64(),
            Model = controller.GetProperty("model").GetString(),
            Name = controller.GetProperty("name").GetString(),
            Type = controller.GetProperty("type").GetString(),
        };
    }
    
    private void _buildSSDAnonymous(AnonymousItemWrapper target, SSDWrapper source) {
        target.Characteristics.Add("tbw", source.TBW);
        target.Characteristics.Add("dwpd", source.DWPD);
        target.Characteristics.Add("bits_for_cell", source.BitsForCell);
        target.Characteristics.Add("controller", source.Controller);
    }
    
    /// <summary>
    /// Создание обёртки корпуса из анонимной обёртки товара
    /// </summary>
    /// <param name="wrapper">Анонимная обёртка</param>
    /// <returns>Обёртка корпуса</returns>
    public ComputerCaseWrapper BuildComputerCase(AnonymousItemWrapper wrapper) {
        var built = new ComputerCaseWrapper();
        _buildBase(built, wrapper);
        _buildComputerComponent(built, wrapper);
        
        built.Typesize = wrapper.Characteristics["typesize"].GetString();
        built.Length = wrapper.Characteristics["length"].GetInt32();
        built.Width = wrapper.Characteristics["width"].GetInt32();
        built.Height = wrapper.Characteristics["height"].GetInt32();
        built.SupportedMotherboardFormFactors = ((JsonElement) wrapper.Characteristics["supported_motherboard_form_factors"])
                                                .EnumerateArray()
                                                .Select
                                                    (x => x.GetString()!
                                                ).ToList();
        built.HasARGBLighting = wrapper.Characteristics["has_argb_lighting"].GetBoolean();
        built.DrivesSlotsCount = wrapper.Characteristics["drives_slots_count"].GetByte();
        
        return built;
    }

    /// <summary>
    /// Создание обёртки кулера для процессора из анонимной обёртки товара
    /// </summary>
    /// <param name="wrapper">Анонимная обёртка</param>
    /// <returns>Обёртка кулера для процессора</returns>
    public CPUCoolerWrapper BuildCPUCooler(AnonymousItemWrapper wrapper) {
        var built = new CPUCoolerWrapper();
        _buildBase(built, wrapper);
        _buildComputerComponent(built, wrapper);
        
        built.MaxFanRPM = wrapper.Characteristics["max_fan_rpm"].GetInt64();
        built.TubesCount = wrapper.Characteristics["tubes_count"].GetByte();
        built.TubesDiameter = wrapper.Characteristics["tubes_diameter"].GetInt();
        built.FanCount = wrapper.Characteristics["fan_count"].GetByte();
        built.FoundationMaterial = wrapper.Characteristics["foundation_material"].GetString();
        built.RadiatorMaterial = wrapper.Characteristics["radiator_material"].GetString();
        
        return built;
    }

    /// <summary>
    /// Создание обёртки центрального процессора из анонимной обёртки товара
    /// </summary>
    /// <param name="wrapper">Анонимная обёртка</param>
    /// <returns>Обёртка центрального процессора</returns>
    public CPUWrapper BuildCPU(AnonymousItemWrapper wrapper) {
        var built = new CPUWrapper();
        _buildBase(built, wrapper);
        _buildComputerComponent(built, wrapper);
        
        built.Socket = wrapper.Characteristics["socket"].GetString();
        built.CoresCount = wrapper.Characteristics["cores_count"].GetInt32();
        built.ThreadsCount = wrapper.Characteristics["threads_count"].GetInt32();
        built.L2CacheSize = wrapper.Characteristics["l2_cache_size"].GetFloat();
        built.L3CacheSize = wrapper.Characteristics["l3_cache_size"].GetFloat();
        built.TechnicalProcess = wrapper.Characteristics["technical_process"].GetFloat();
        built.BaseFrequency = wrapper.Characteristics["base_frequency"].GetFloat();
        built.SupportedRAMSize = wrapper.Characteristics["supported_ram_size"].GetInt32();
        built.HasIntegratedGraphics = wrapper.Characteristics["has_integrated_graphics"].GetBoolean();
        
        built.SupportedRamTypes = ((JsonElement) wrapper.Characteristics["supported_ram_types"])
                .EnumerateArray().Select(x => x.GetString()!).ToList();

        var core = wrapper.Characteristics["core"];

        built.Core = new CPUCoreWrapper {
            Id = core.GetProperty("id").GetInt32(),
            Name = core.GetProperty("name").GetString()!,
            Vendor = core.GetProperty("vendor").GetString()!
        };
        
        return built;
    }

    /// <summary>
    /// Создание обёртки монитора из анонимной обёртки товара
    /// </summary>
    /// <param name="wrapper">Анонимная обёртка</param>
    /// <returns>Обёртка монитора</returns>
    public DisplayWrapper BuildDisplay(AnonymousItemWrapper wrapper) {
        var built = new DisplayWrapper();
        _buildBase(built, wrapper);
        _buildComputerComponent(built, wrapper);

        built.MaxUpdateFrequency = wrapper.Characteristics["max_ram_frequency"].GetInt32();
        built.ScreenDiagonal = wrapper.Characteristics["screen_diagonal"].GetDouble();
        built.MatrixType = wrapper.Characteristics["matrix_type"].GetString();
        built.Underlight = wrapper.Characteristics["underlight"].GetString();
        built.VesaSize = wrapper.Characteristics["vesa_size"].GetString();
        
        var definition = wrapper.Characteristics["definition"];

        built.Definition = new DefinitionWrapper {
            Id = definition.GetProperty("id").GetInt32(),
            Width = definition.GetProperty("width").GetInt32()!,
            Height = definition.GetProperty("height").GetInt32()!,
        };
        
        return built;
    }

    /// <summary>
    /// Создание обёртки видеокарты из анонимной обёртки товара
    /// </summary>
    /// <param name="wrapper">Анонимная обёртка</param>
    /// <returns>Обёртка видеокарты</returns>
    public GraphicsCardWrapper BuildGraphicsCard(AnonymousItemWrapper wrapper) {
        var built = new GraphicsCardWrapper();
        _buildBase(built, wrapper);
        _buildComputerComponent(built, wrapper);
        
        built.VideoRAM = wrapper.Characteristics["video_ram"].GetInt32();
        built.MaxDisplaysSupported = wrapper.Characteristics["max_displays_supported"].GetByte();
        built.UsedSlots = wrapper.Characteristics["used_slots"].GetByte();
        
        built.VideoPorts = ((JsonElement) wrapper.Characteristics["video_ports"])
            .EnumerateArray().Select(x => x.GetString()!).ToList();
        
        var gpu = wrapper.Characteristics["gpu"];

        built.GPU = new GPUWrapper {
            Id = gpu.GetProperty("id").GetInt32(),
            Name = gpu.GetProperty("name").GetString()!,
            Model = gpu.GetProperty("model").GetString()!,
            Vendor = gpu.GetProperty("vendor").GetString()!,
        };
        
        return built;
    }

    /// <summary>
    /// Создание обёртки жёсткого диска из анонимной обёртки товара
    /// </summary>
    /// <param name="wrapper">Анонимная обёртка</param>
    /// <returns>Обёртка жёсткого диска</returns>
    public HDDWrapper BuildHDD(AnonymousItemWrapper wrapper) {
        var built = new HDDWrapper();
        _buildBase(built, wrapper);
        _buildComputerComponent(built, wrapper);
        _buildDiskDrive(built, wrapper);
        
        built.RPM = wrapper.Characteristics["rpm"].GetInt32();
        
        return built;
    }

    /// <summary>
    /// Создание обёртки клавиатуры из анонимной обёртки товара
    /// </summary>
    /// <param name="wrapper">Анонимная обёртка</param>
    /// <returns>Обёртка клавиатуры</returns>
    public KeyboardWrapper BuildKeyboard(AnonymousItemWrapper wrapper) {
        var built = new KeyboardWrapper();
        _buildBase(built, wrapper);
        _buildComputerComponent(built, wrapper);

        built.HasRGBLighting = wrapper.Characteristics["has_rgb_lighting"].GetBoolean();
        built.IsWireless = wrapper.Characteristics["is_wireless"].GetBoolean();
        built.Typesize = wrapper.Characteristics["typesize"].GetString();
        built.Type = wrapper.Characteristics["type"].GetString();
        
        return built;
    }

    /// <summary>
    /// Создание обёртки материнской платы из анонимной обёртки товара
    /// </summary>
    /// <param name="wrapper">Анонимная обёртка</param>
    /// <returns>Обёртка материнской платы</returns>
    public MotherboardWrapper BuildMotherboard(AnonymousItemWrapper wrapper) {
        
        var built = new MotherboardWrapper();
        _buildBase(built, wrapper);
        _buildComputerComponent(built, wrapper);
        
        built.RAMSlots = wrapper.Characteristics["ram_slots"].GetByte();
        built.RAMChannels = wrapper.Characteristics["ram_channels"].GetByte();
        built.MaxRAMFrequency = wrapper.Characteristics["max_ram_frequency"].GetInt32();
        built.PCIESlotsCount = wrapper.Characteristics["pcie_slots_count"].GetByte();
        built.HasNVMeSupport = wrapper.Characteristics["has_nvme_support"].GetBoolean();
        built.M2SlotsCount = wrapper.Characteristics["m2_slots_count"].GetByte();
        built.SataPortsCount = wrapper.Characteristics["sata_ports_count"].GetByte();
        built.USBPortsCount = wrapper.Characteristics["usb_ports_count"].GetByte();
        built.RJ45PortsCount = wrapper.Characteristics["rj45_ports_count"].GetByte();
        built.DigitalAudioPortsCount = wrapper.Characteristics["digital_audio_ports_count"].GetByte();
        built.NetworkAdapterSpeed = wrapper.Characteristics["network_adapter_speed"].GetFloat();
        built.FormFactor = wrapper.Characteristics["form_factor"].GetString();
        built.CPUSocket = wrapper.Characteristics["cpu_socket"].GetString();
        built.PCIEVersion = wrapper.Characteristics["pcie_version"].GetString();
        
        built.SupportedRAMTypes = ((JsonElement) wrapper.Characteristics["supported_ram_types"])
            .EnumerateArray().Select(x => x.GetString()!).ToList();
        built.VideoPorts = ((JsonElement) wrapper.Characteristics["supported_ram_types"])
            .EnumerateArray().Select(x => x.GetString()!).ToList();
        
        built.SupportedCPUCores = ((JsonElement) wrapper.Characteristics["supported_cpu_cores"])
            .EnumerateArray().Select(x => new CPUCoreWrapper {
                Id = x.GetProperty("id").GetInt32(),
                Name = x.GetProperty("name").GetString()!,
                Vendor = x.GetProperty("vendor").GetString()!
            }).ToList();
        
        var motherboardChipset = wrapper.Characteristics["motherboard_chipset"];
        var audioChipset = wrapper.Characteristics["audio_chipset"];
        var networkAdapter = wrapper.Characteristics["network_adapter"];

        built.MotherboardChipset = new MotherboardChipsetWrapper {
            Id = motherboardChipset.GetProperty("id").GetInt32(),
            Name = motherboardChipset.GetProperty("name").GetString()!,
            Model = motherboardChipset.GetProperty("model").GetString()!,
            Type = motherboardChipset.GetProperty("type").GetString()!,
        };
        
        built.AudioChipset = new AudioChipsetWrapper {
            Id = audioChipset.GetProperty("id").GetInt32(),
            Name = audioChipset.GetProperty("name").GetString()!,
            Model = audioChipset.GetProperty("model").GetString()!,
            Type = audioChipset.GetProperty("type").GetString()!,
        };
        
        built.NetworkAdapter = new NetworkAdapterWrapper {
            Id = networkAdapter.GetProperty("id").GetInt32(),
            Name = networkAdapter.GetProperty("name").GetString()!,
            Model = networkAdapter.GetProperty("model").GetString()!,
            Type = networkAdapter.GetProperty("type").GetString()!,
        };
        
        return built;
        
    }

    /// <summary>
    /// Создание обёртки мыши из анонимной обёртки товара
    /// </summary>
    /// <param name="wrapper">Анонимная обёртка</param>
    /// <returns>Обёртка мыши</returns>
    public MouseWrapper BuildMouse(AnonymousItemWrapper wrapper) {
        var built = new MouseWrapper();
        _buildBase(built, wrapper);
        _buildComputerComponent(built, wrapper);
        
        built.ButtonsCount = wrapper.Characteristics["buttons_count"].GetByte();
        built.HasProgrammableButtons = wrapper.Characteristics["has_programmable_buttons"].GetBoolean();
        built.IsWireless = wrapper.Characteristics["is_wireless"].GetBoolean();
        
        built.DPIModes = ((JsonElement) wrapper.Characteristics["dpi_modes"])
                           .EnumerateArray().Select(x => x.GetInt32()).ToList();
        
        return built;
    }

    /// <summary>
    /// Создание обёртки твердотельного накопителя NVMe из анонимной обёртки товара
    /// </summary>
    /// <param name="wrapper">Анонимная обёртка</param>
    /// <returns>Обёртка твердотельного накопителя NVMe</returns>
    public NVMeSSDWrapper BuildNVMeSSD(AnonymousItemWrapper wrapper) {
        var built = new NVMeSSDWrapper();
        _buildBase(built, wrapper);
        _buildComputerComponent(built, wrapper);
        _buildDiskDrive(built, wrapper);
        _buildSSD(built, wrapper);
        return built;
    }

    /// <summary>
    /// Создание обёртки блока питания из анонимной обёртки товара
    /// </summary>
    /// <param name="wrapper">Анонимная обёртка</param>
    /// <returns>Обёртка блока питания</returns>
    public PowerSupplyWrapper BuildPowerSupply(AnonymousItemWrapper wrapper) {
        var built = new PowerSupplyWrapper();
        _buildBase(built, wrapper);
        _buildComputerComponent(built, wrapper);
        
        built.PowerOutput = wrapper.Characteristics["power_output"].GetInt32();
        built.SataPorts = wrapper.Characteristics["sata_ports"].GetByte();
        built.MolexPorts = wrapper.Characteristics["molex_ports"].GetByte();
        built.Certificate80Plus = wrapper.Characteristics["certificate_80plus"].GetString();
        
        return built;
    }

    /// <summary>
    /// Создание обёртки ОЗУ из анонимной обёртки товара
    /// </summary>
    /// <param name="wrapper">Анонимная обёртка</param>
    /// <returns>Обёртка ОЗУ</returns>
    public RAMWrapper BuildRAM(AnonymousItemWrapper wrapper) {
        var built = new RAMWrapper();
        _buildBase(built, wrapper);
        _buildComputerComponent(built, wrapper);
        
        built.TotalSize = wrapper.Characteristics["total_size"].GetInt32();
        built.ModuleSize = wrapper.Characteristics["module_size"].GetInt32();
        built.ModulesCount = wrapper.Characteristics["modules_count"].GetByte();
        built.Frequency = wrapper.Characteristics["frequency"].GetInt();
        built.CL = wrapper.Characteristics["cl"].GetByte();
        built.tRCD = wrapper.Characteristics["trcd"].GetByte();
        built.tRP = wrapper.Characteristics["trp"].GetByte();
        built.tRAS = wrapper.Characteristics["tras"].GetByte();
        built.Type = wrapper.Characteristics["type"].GetString();
        
        return built;
    }

    /// <summary>
    /// Создание обёртки твердотельного накопителя Sata из анонимной обёртки товара
    /// </summary>
    /// <param name="wrapper">Анонимная обёртка</param>
    /// <returns>Обёртка твердотельного накопителя Sata</returns>
    public SataSSDWrapper BuildSataSSD(AnonymousItemWrapper wrapper) {
        var built = new SataSSDWrapper();
        _buildBase(built, wrapper);
        _buildComputerComponent(built, wrapper);
        _buildDiskDrive(built, wrapper);
        _buildSSD(built, wrapper);
        return built;
    }

    /// <summary>
    /// Создание анонимной обёртки из корпуса 
    /// </summary>
    /// <param name="wrapper">Оборачиваемый корпус</param>
    /// <returns>Анонимная обёртка корпуса</returns>
    public AnonymousItemWrapper BuildWrapper(ComputerCaseWrapper wrapper) {
        var built = new AnonymousItemWrapper();
        _buildBaseAnonymous(built, wrapper);
        
        built.Characteristics = new Dictionary<string, dynamic> {
            { "typesize", wrapper.Typesize },
            { "length", wrapper.Length },
            { "width", wrapper.Width },
            { "height", wrapper.Height },
            { "supported_motherboard_form_factors", wrapper.SupportedMotherboardFormFactors },
            { "has_argb_lighting", wrapper.HasARGBLighting },
            { "drives_slots_count", wrapper.DrivesSlotsCount },
        };
        
        _buildComputerComponentAnonymous(built, wrapper);
        return built;
    }

    /// <summary>
    /// Создание анонимной обёртки из кулера для процессора 
    /// </summary>
    /// <param name="wrapper">Оборачиваемый кулер для процессора</param>
    /// <returns>Анонимная обёртка кулера для процессора</returns>
    public AnonymousItemWrapper BuildWrapper(CPUCoolerWrapper wrapper) {
        var built = new AnonymousItemWrapper();
        _buildBaseAnonymous(built, wrapper);

        built.Characteristics = new Dictionary<string, dynamic> {

            { "max_fan_rpm", wrapper.MaxFanRPM },
            { "tubes_count", wrapper.TubesCount },
            { "tubes_diameter", wrapper.TubesDiameter },
            { "fan_count", wrapper.FanCount },
            { "foundation_material", wrapper.FoundationMaterial },
            { "radiator_material", wrapper.RadiatorMaterial },

        };
        
        _buildComputerComponentAnonymous(built, wrapper);
        return built;
    }

    /// <summary>
    /// Создание анонимной обёртки из центрального процессора 
    /// </summary>
    /// <param name="wrapper">Оборачиваемый центральный процессор</param>
    /// <returns>Анонимная обёртка центрального процессора</returns>
    public AnonymousItemWrapper BuildWrapper(CPUWrapper wrapper) {
        var built = new AnonymousItemWrapper();
        _buildBaseAnonymous(built, wrapper);
        
        built.Characteristics = new Dictionary<string, dynamic> {
            {"socket",wrapper.Socket},
            {"cores_count",wrapper.CoresCount},
            {"threads_count",wrapper.ThreadsCount},
            {"l2_cache_size",wrapper.L2CacheSize},
            {"l3_cache_size",wrapper.L3CacheSize},
            {"technical_process",wrapper.TechnicalProcess},
            {"base_frequency",wrapper.BaseFrequency},
            {"supported_ram_size",wrapper.SupportedRAMSize},
            {"has_integrated_graphics",wrapper.HasIntegratedGraphics},
            {"supported_ram_types", wrapper.SupportedRamTypes},
            {"core", wrapper.Core}
        };

        _buildComputerComponentAnonymous(built, wrapper);
        return built;
    }

    /// <summary>
    /// Создание анонимной обёртки из монитора 
    /// </summary>
    /// <param name="wrapper">Оборачиваемый монитор</param>
    /// <returns>Анонимная обёртка монитора</returns>
    public AnonymousItemWrapper BuildWrapper(DisplayWrapper wrapper) {
        var built = new AnonymousItemWrapper();
        _buildBaseAnonymous(built, wrapper);

        built.Characteristics = new Dictionary<string, dynamic> {
            { "max_update_frequency", wrapper.MaxUpdateFrequency },
            { "screen_diagonal", wrapper.ScreenDiagonal },
            { "matrix_type", wrapper.MatrixType },
            { "underlight", wrapper.Underlight },
            { "vesa_size", wrapper.VesaSize },
            { "definition", wrapper.Definition }
        };
        
        _buildComputerComponentAnonymous(built, wrapper);
        return built;
    }

    /// <summary>
    /// Создание анонимной обёртки из видеокарты 
    /// </summary>
    /// <param name="wrapper">Оборачиваемая видеокарта</param>
    /// <returns>Анонимная обёртка видеокарты</returns>
    public AnonymousItemWrapper BuildWrapper(GraphicsCardWrapper wrapper) {
        var built = new AnonymousItemWrapper();
        _buildBaseAnonymous(built, wrapper);

        built.Characteristics = new Dictionary<string, dynamic> {

            { "video_ram", wrapper.VideoRAM },
            { "max_displays_supported", wrapper.MaxDisplaysSupported },
            { "used_slots", wrapper.UsedSlots },
            { "video_ports", wrapper.VideoPorts },
            { "gpu", wrapper.GPU }
            
        };
        
        _buildComputerComponentAnonymous(built, wrapper);
        return built;
    }

    /// <summary>
    /// Создание анонимной обёртки из жёсткого диска 
    /// </summary>
    /// <param name="wrapper">Оборачиваемый жёсткий диск</param>
    /// <returns>Анонимная обёртка жёсткого диска</returns>
    public AnonymousItemWrapper BuildWrapper(HDDWrapper wrapper) {
        var built = new AnonymousItemWrapper();
        _buildBaseAnonymous(built, wrapper);
        _buildComputerComponentAnonymous(built, wrapper);
        _buildDiskDriveAnonymous(built, wrapper);
        
        built.Characteristics.Add("rpm", wrapper.RPM);
        
        return built;
    }

    /// <summary>
    /// Создание анонимной обёртки из клавиатуры 
    /// </summary>
    /// <param name="wrapper">Оборачиваемая клавиатура</param>
    /// <returns>Анонимная обёртка клавиатуры</returns>
    public AnonymousItemWrapper BuildWrapper(KeyboardWrapper wrapper) {
        var built = new AnonymousItemWrapper();
        _buildBaseAnonymous(built, wrapper);

        built.Characteristics = new Dictionary<string, dynamic> {
            { "has_rgb_lighting", wrapper.HasRGBLighting },
            { "is_wireless", wrapper.IsWireless },
            { "typesize", wrapper.Typesize },
            { "type", wrapper.Type },
        };
        
        _buildComputerComponentAnonymous(built, wrapper);
        return built;
    }

    /// <summary>
    /// Создание анонимной обёртки из материнской платы 
    /// </summary>
    /// <param name="wrapper">Оборачиваемая материнская плата</param>
    /// <returns>Анонимная обёртка материнской платы</returns>
    public AnonymousItemWrapper BuildWrapper(MotherboardWrapper wrapper) {
        var built = new AnonymousItemWrapper();
        _buildBaseAnonymous(built, wrapper);

        built.Characteristics = new Dictionary<string, dynamic> {
            { "ram_slots", wrapper.RAMSlots },
            { "ram_channels", wrapper.RAMChannels },
            { "max_ram_frequency", wrapper.MaxRAMFrequency },
            { "pcie_slots_count", wrapper.PCIESlotsCount },
            { "has_nvme_support", wrapper.HasNVMeSupport },
            { "m2_slots_count", wrapper.M2SlotsCount },
            { "sata_ports_count", wrapper.SataPortsCount },
            { "usb_ports_count", wrapper.USBPortsCount },
            { "rj45_ports_count", wrapper.RJ45PortsCount },
            { "digital_audio_ports_count", wrapper.DigitalAudioPortsCount },
            { "network_adapter_speed", wrapper.NetworkAdapterSpeed },
            { "form_factor", wrapper.FormFactor },
            { "cpu_socket", wrapper.CPUSocket },
            { "pcie_version", wrapper.PCIEVersion },
            { "supported_ram_types", wrapper.SupportedRAMTypes },
            { "video_ports", wrapper.VideoPorts },
            { "supported_cpu_cores", wrapper.SupportedCPUCores },
            { "motherboard_chipset", wrapper.MotherboardChipset },
            { "audio_chipset", wrapper.AudioChipset },
            { "network_adapter", wrapper.NetworkAdapter }
        };
        
        _buildComputerComponentAnonymous(built, wrapper);
        return built;
    }

    /// <summary>
    /// Создание анонимной обёртки из мыши 
    /// </summary>
    /// <param name="wrapper">Оборачиваемая мышь</param>
    /// <returns>Анонимная обёртка мыши</returns>
    public AnonymousItemWrapper BuildWrapper(MouseWrapper wrapper) {
        var built = new AnonymousItemWrapper();
        _buildBaseAnonymous(built, wrapper);

        built.Characteristics = new Dictionary<string, dynamic> {
            { "buttons_count", wrapper.ButtonsCount },
            { "has_programmable_buttons", wrapper.HasProgrammableButtons },
            { "is_wireless", wrapper.IsWireless },
            { "dpi_modes", wrapper.DPIModes },
        };
            
        _buildComputerComponentAnonymous(built, wrapper);
        return built;
    }

    /// <summary>
    /// Создание анонимной обёртки из твердотельного накопителя NVMe 
    /// </summary>
    /// <param name="wrapper">Оборачиваемый накопитель</param>
    /// <returns>Анонимная обёртка твердотельного накопителя NVMe</returns>
    public AnonymousItemWrapper BuildWrapper(NVMeSSDWrapper wrapper) {
        var built = new AnonymousItemWrapper();
        _buildBaseAnonymous(built, wrapper);
        _buildComputerComponentAnonymous(built, wrapper);
        _buildDiskDriveAnonymous(built, wrapper);
        _buildSSDAnonymous(built, wrapper);
        return built;
    }

    /// <summary>
    /// Создание анонимной обёртки из блока питания 
    /// </summary>
    /// <param name="wrapper">Оборачиваемый блок питания</param>
    /// <returns>Анонимная обёртка блока питания</returns>
    public AnonymousItemWrapper BuildWrapper(PowerSupplyWrapper wrapper) {
        var built = new AnonymousItemWrapper();
        _buildBaseAnonymous(built, wrapper);

        built.Characteristics = new Dictionary<string, dynamic> {
            { "power_output", wrapper.PowerOutput },
            { "sata_ports", wrapper.SataPorts },
            { "molex_ports", wrapper.MolexPorts },
            { "certificate_80plus", wrapper.Certificate80Plus }
        };
        
        _buildComputerComponentAnonymous(built, wrapper);
        return built;
    }

    /// <summary>
    /// Создание анонимной обёртки из ОЗУ 
    /// </summary>
    /// <param name="wrapper">Оборачиваемое ОЗУ</param>
    /// <returns>Анонимная обёртка ОЗУ</returns>
    public AnonymousItemWrapper BuildWrapper(RAMWrapper wrapper) {
        var built = new AnonymousItemWrapper();
        _buildBaseAnonymous(built, wrapper);

        built.Characteristics = new Dictionary<string, dynamic> {
            { "total_size", wrapper.TotalSize },
            { "module_size", wrapper.ModuleSize },
            { "modules_count", wrapper.ModulesCount },
            { "frequency", wrapper.Frequency },
            { "cl", wrapper.CL },
            { "trcd", wrapper.tRCD },
            { "trp", wrapper.tRP },
            { "tras", wrapper.tRAS },
            { "type", wrapper.Type },
        };
        
        _buildComputerComponentAnonymous(built, wrapper);
        return built;
    }

    /// <summary>
    /// Создание анонимной обёртки из твердотельного накопителя Sata 
    /// </summary>
    /// <param name="wrapper">Оборачиваемый накопитель</param>
    /// <returns>Анонимная обёртка твердотельного накопителя Sata</returns>
    public AnonymousItemWrapper BuildWrapper(SataSSDWrapper wrapper) {
        var built = new AnonymousItemWrapper();
        _buildBaseAnonymous(built, wrapper);
        _buildComputerComponentAnonymous(built, wrapper);
        _buildDiskDriveAnonymous(built, wrapper);
        _buildSSDAnonymous(built, wrapper);
        return built;
    }

    /// <summary>
    /// Создание анонимной обёртки из готовой сборки 
    /// </summary>
    /// <param name="wrapper">Оборачиваемая готовая сборка</param>
    /// <returns>Анонимная обёртка готовой сборки</returns>
    public PreparedAssemblyWrapper BuildPreparedAssembly(AnonymousItemWrapper wrapper) {
        var built = new PreparedAssemblyWrapper();
        _buildBase(built, wrapper);
        
        var characteristics = wrapper.Characteristics;
        built.CPU = _buildPreparedAssemblyItem(characteristics["cpu"]);
        built.Motherboard = _buildPreparedAssemblyItem(characteristics["motherboard"]);
        built.GraphicsCard = _buildPreparedAssemblyItem(characteristics["graphics_card"]);
        built.PowerSupply = _buildPreparedAssemblyItem(characteristics["power_supply"]);
        built.ComputerCase = _buildPreparedAssemblyItem(characteristics["computer_case"]);
        built.CPUCooler = _buildPreparedAssemblyItem(characteristics["cpu_cooler"]);
        
        built.Display = characteristics["display"] == null ? null : _buildPreparedAssemblyItem(characteristics["display"]);
        built.Keyboard = characteristics["keyboard"] == null ? null : _buildPreparedAssemblyItem(characteristics["keyboard"]);
        built.Mouse = characteristics["mouse"] == null ? null : _buildPreparedAssemblyItem(characteristics["mouse"]);
        
        built.RAMs = ((JsonElement) wrapper.Characteristics["rams"])
                     .EnumerateArray().Select(x => _buildPreparedAssemblyItem(x))
                     .ToList();
        built.DiskDrives = ((JsonElement) wrapper.Characteristics["disk_drives"])
                     .EnumerateArray().Select(x => _buildPreparedAssemblyDiskDrive(x))
                     .ToList();
        
        return built;
    }

    /// <summary>
    /// Создание обёртки готовой сборки из анонимной обёртки товара
    /// </summary>
    /// <param name="wrapper">Анонимная обёртка</param>
    /// <returns>Обёртка готовой сборки</returns>
    public AnonymousItemWrapper BuildWrapper(PreparedAssemblyWrapper wrapper) {
        var built = new AnonymousItemWrapper();
        _buildBaseAnonymous(built, wrapper);

        built.Characteristics = new Dictionary<string, dynamic> {
            {"rams", wrapper.RAMs },
            {"disk_drives", wrapper.DiskDrives },
            {"cpu", wrapper.CPU},
            {"motherboard", wrapper.Motherboard},
            {"graphics_card", wrapper.GraphicsCard},
            {"power_supply", wrapper.PowerSupply},
            {"display", wrapper.Display!},
            {"computer_case", wrapper.ComputerCase},
            {"keyboard", wrapper.Keyboard!},
            {"mouse", wrapper.Mouse!},
            {"cpu_cooler", wrapper.CPUCooler},
        };
        
        return built;
    }

    private PreparedAssemblyItemWrapper _buildPreparedAssemblyItem(JsonElement wrapper) {
        var built = new PreparedAssemblyItemWrapper();
        built.Id = wrapper.GetProperty("id").GetInt32();
        built.Model = wrapper.GetProperty("model").GetString()!;
        return built;
    }

    private PreparedAssemblyDiskDriveWrapper _buildPreparedAssemblyDiskDrive(JsonElement wrapper) {
        var built = new PreparedAssemblyDiskDriveWrapper();
        built.Id = wrapper.GetProperty("id").GetInt32();
        built.Model = wrapper.GetProperty("model").GetString()!;
        built.Type = wrapper.GetProperty("type").GetString()!;
        return built;
    }
}