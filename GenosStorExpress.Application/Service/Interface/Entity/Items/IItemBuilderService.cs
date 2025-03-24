using GenosStorExpress.Application.Wrappers.Entity.Item;
using GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;
using GenosStorExpress.Application.Wrappers.Entity.Item.PreaparedAssembly;

namespace GenosStorExpress.Application.Service.Interface.Entity.Items;


/// <summary>
/// Интерфейс для сервиса-конвертера (сущность товара -> обёртка и наоборот).
/// Используется для обеспечения корректного вызова всех сервисов товаров, так как извне приходят изначально
/// данные, структура которых заранее неизвестна, с чем сервисы не работают
/// </summary>
public interface IItemBuilderService {
    /// <summary>
    /// Создание обёртки корпуса из анонимной обёртки товара
    /// </summary>
    /// <param name="wrapper">Анонимная обёртка</param>
    /// <returns>Обёртка корпуса</returns>
    
    ComputerCaseWrapper BuildComputerCase(AnonymousItemWrapper wrapper);
    /// <summary>
    /// Создание обёртки кулера для процессора из анонимной обёртки товара
    /// </summary>
    /// <param name="wrapper">Анонимная обёртка</param>
    /// <returns>Обёртка кулера для процессора</returns>
    CPUCoolerWrapper BuildCPUCooler(AnonymousItemWrapper wrapper);
    /// <summary>
    /// Создание обёртки центрального процессора из анонимной обёртки товара
    /// </summary>
    /// <param name="wrapper">Анонимная обёртка</param>
    /// <returns>Обёртка центрального процессора</returns>
    CPUWrapper BuildCPU(AnonymousItemWrapper wrapper);
    /// <summary>
    /// Создание обёртки монитора из анонимной обёртки товара
    /// </summary>
    /// <param name="wrapper">Анонимная обёртка</param>
    /// <returns>Обёртка монитора</returns>
    DisplayWrapper BuildDisplay(AnonymousItemWrapper wrapper);
    /// <summary>
    /// Создание обёртки видеокарты из анонимной обёртки товара
    /// </summary>
    /// <param name="wrapper">Анонимная обёртка</param>
    /// <returns>Обёртка видеокарты</returns>
    GraphicsCardWrapper BuildGraphicsCard(AnonymousItemWrapper wrapper);
    /// <summary>
    /// Создание обёртки жёсткого диска из анонимной обёртки товара
    /// </summary>
    /// <param name="wrapper">Анонимная обёртка</param>
    /// <returns>Обёртка жёсткого диска</returns>
    HDDWrapper BuildHDD(AnonymousItemWrapper wrapper);
    /// <summary>
    /// Создание обёртки клавиатуры из анонимной обёртки товара
    /// </summary>
    /// <param name="wrapper">Анонимная обёртка</param>
    /// <returns>Обёртка клавиатуры</returns>
    KeyboardWrapper BuildKeyboard(AnonymousItemWrapper wrapper);
    /// <summary>
    /// Создание обёртки материнской платы из анонимной обёртки товара
    /// </summary>
    /// <param name="wrapper">Анонимная обёртка</param>
    /// <returns>Обёртка материнской платы</returns>
    MotherboardWrapper BuildMotherboard(AnonymousItemWrapper wrapper);
    /// <summary>
    /// Создание обёртки мыши из анонимной обёртки товара
    /// </summary>
    /// <param name="wrapper">Анонимная обёртка</param>
    /// <returns>Обёртка мыши</returns>
    MouseWrapper BuildMouse(AnonymousItemWrapper wrapper);
    /// <summary>
    /// Создание обёртки твердотельного накопителя NVMe из анонимной обёртки товара
    /// </summary>
    /// <param name="wrapper">Анонимная обёртка</param>
    /// <returns>Обёртка твердотельного накопителя NVMe</returns>
    NVMeSSDWrapper BuildNVMeSSD(AnonymousItemWrapper wrapper);
    /// <summary>
    /// Создание обёртки блока питания из анонимной обёртки товара
    /// </summary>
    /// <param name="wrapper">Анонимная обёртка</param>
    /// <returns>Обёртка блока питания</returns>
    PowerSupplyWrapper BuildPowerSupply(AnonymousItemWrapper wrapper);
    /// <summary>
    /// Создание обёртки ОЗУ из анонимной обёртки товара
    /// </summary>
    /// <param name="wrapper">Анонимная обёртка</param>
    /// <returns>Обёртка ОЗУ</returns>
    RAMWrapper BuildRAM(AnonymousItemWrapper wrapper);
    /// <summary>
    /// Создание обёртки твердотельного накопителя Sata из анонимной обёртки товара
    /// </summary>
    /// <param name="wrapper">Анонимная обёртка</param>
    /// <returns>Обёртка твердотельного накопителя Sata</returns>
    SataSSDWrapper BuildSataSSD(AnonymousItemWrapper wrapper);
    
    /// <summary>
    /// Создание анонимной обёртки из корпуса 
    /// </summary>
    /// <param name="wrapper">Оборачиваемый корпус</param>
    /// <returns>Анонимная обёртка корпуса</returns>
    AnonymousItemWrapper BuildWrapper(ComputerCaseWrapper wrapper);
    /// <summary>
    /// Создание анонимной обёртки из кулера для процессора 
    /// </summary>
    /// <param name="wrapper">Оборачиваемый кулер для процессора</param>
    /// <returns>Анонимная обёртка кулера для процессора</returns>
    AnonymousItemWrapper BuildWrapper(CPUCoolerWrapper wrapper);
    /// <summary>
    /// Создание анонимной обёртки из центрального процессора 
    /// </summary>
    /// <param name="wrapper">Оборачиваемый центральный процессор</param>
    /// <returns>Анонимная обёртка центрального процессора</returns>
    AnonymousItemWrapper BuildWrapper(CPUWrapper wrapper);
    /// <summary>
    /// Создание анонимной обёртки из монитора 
    /// </summary>
    /// <param name="wrapper">Оборачиваемый монитор</param>
    /// <returns>Анонимная обёртка монитора</returns>
    AnonymousItemWrapper BuildWrapper(DisplayWrapper wrapper);
    /// <summary>
    /// Создание анонимной обёртки из видеокарты 
    /// </summary>
    /// <param name="wrapper">Оборачиваемая видеокарта</param>
    /// <returns>Анонимная обёртка видеокарты</returns>
    AnonymousItemWrapper BuildWrapper(GraphicsCardWrapper wrapper);
    /// <summary>
    /// Создание анонимной обёртки из жёсткого диска 
    /// </summary>
    /// <param name="wrapper">Оборачиваемый жёсткий диск</param>
    /// <returns>Анонимная обёртка жёсткого диска</returns>
    AnonymousItemWrapper BuildWrapper(HDDWrapper wrapper);
    /// <summary>
    /// Создание анонимной обёртки из клавиатуры 
    /// </summary>
    /// <param name="wrapper">Оборачиваемая клавиатура</param>
    /// <returns>Анонимная обёртка клавиатуры</returns>
    AnonymousItemWrapper BuildWrapper(KeyboardWrapper wrapper);
    /// <summary>
    /// Создание анонимной обёртки из материнской платы 
    /// </summary>
    /// <param name="wrapper">Оборачиваемая материнская плата</param>
    /// <returns>Анонимная обёртка материнской платы</returns>
    AnonymousItemWrapper BuildWrapper(MotherboardWrapper wrapper);
    /// <summary>
    /// Создание анонимной обёртки из мыши 
    /// </summary>
    /// <param name="wrapper">Оборачиваемая мышь</param>
    /// <returns>Анонимная обёртка мыши</returns>
    AnonymousItemWrapper BuildWrapper(MouseWrapper wrapper);
    /// <summary>
    /// Создание анонимной обёртки из твердотельного накопителя NVMe 
    /// </summary>
    /// <param name="wrapper">Оборачиваемый накопитель</param>
    /// <returns>Анонимная обёртка твердотельного накопителя NVMe</returns>
    AnonymousItemWrapper BuildWrapper(NVMeSSDWrapper wrapper);
    /// <summary>
    /// Создание анонимной обёртки из блока питания 
    /// </summary>
    /// <param name="wrapper">Оборачиваемый блок питания</param>
    /// <returns>Анонимная обёртка блока питания</returns>
    AnonymousItemWrapper BuildWrapper(PowerSupplyWrapper wrapper);
    /// <summary>
    /// Создание анонимной обёртки из ОЗУ 
    /// </summary>
    /// <param name="wrapper">Оборачиваемое ОЗУ</param>
    /// <returns>Анонимная обёртка ОЗУ</returns>
    AnonymousItemWrapper BuildWrapper(RAMWrapper wrapper);
    /// <summary>
    /// Создание анонимной обёртки из твердотельного накопителя Sata 
    /// </summary>
    /// <param name="wrapper">Оборачиваемый накопитель</param>
    /// <returns>Анонимная обёртка твердотельного накопителя Sata</returns>
    AnonymousItemWrapper BuildWrapper(SataSSDWrapper wrapper);
    
    
    /// <summary>
    /// Создание анонимной обёртки из готовой сборки 
    /// </summary>
    /// <param name="wrapper">Оборачиваемая готовая сборка</param>
    /// <returns>Анонимная обёртка готовой сборки</returns>
    PreparedAssemblyWrapper BuildPreparedAssembly(AnonymousItemWrapper wrapper);
    /// <summary>
    /// Создание обёртки готовой сборки из анонимной обёртки товара
    /// </summary>
    /// <param name="wrapper">Анонимная обёртка</param>
    /// <returns>Обёртка готовой сборки</returns>
    AnonymousItemWrapper BuildWrapper(PreparedAssemblyWrapper wrapper);
    
}