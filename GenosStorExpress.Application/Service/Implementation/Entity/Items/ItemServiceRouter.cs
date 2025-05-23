﻿using GenosStorExpress.Application.Service.Interface.Common;
using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Application.Wrappers.Entity.Item;
using GenosStorExpress.Application.Wrappers.Enum;
using GenosStorExpress.Application.Wrappers.Filters;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items;

/// <summary>
/// Реализация сервиса-маршрутизатора сервисов товаров
/// </summary>
public class ItemServiceRouter: IItemServiceRouter {
    
    private readonly IComputerComponentServices _computerComponents;
    private readonly IPreparedAssemblyService _preparedAssemblyService;
    private readonly IItemBuilderService _itemBuilderService;
    private readonly IFilterTransformerService _filterTransformerService;

    /// <summary>
    /// Стандартный конструктор
    /// </summary>
    /// <param name="computerComponents">Сервисы компьютерных комплектующих</param>
    /// <param name="preparedAssemblyService">Сервис готовых сборок</param>
    /// <param name="itemBuilderService">Сервис сборщика товаров</param>
    /// <param name="filterTransformerService">Сервис трансформера фильтров</param>
    public ItemServiceRouter(IComputerComponentServices computerComponents, IPreparedAssemblyService preparedAssemblyService, IItemBuilderService itemBuilderService, IFilterTransformerService filterTransformerService) {
        _computerComponents = computerComponents;
        _preparedAssemblyService = preparedAssemblyService;
        _itemBuilderService = itemBuilderService;
        _filterTransformerService = filterTransformerService;
    }

    /// <summary>
    /// Получение информации о товаре по его типу по номеру
    /// </summary>
    /// <param name="itemType">Дескриптор типа товара</param>
    /// <param name="id">Номер товара</param>
    /// <returns>Анонимную обёртку товара</returns>
    /// <exception cref="NullReferenceException">Если такого товара не существует</exception>
    /// <exception cref="ArgumentException">Если был передан неизвестный дескриптор товара</exception>
    public AnonymousItemWrapper Get(ItemTypeDescriptor itemType, int id) {
        
        switch (itemType) {
            case ItemTypeDescriptor.ComputerCase: {
                var obj = _computerComponents.ComputerCases.Get(id);
                if (obj == null) {
                    throw new NullReferenceException($"Компьютерного корпуса с номером {id} не существует");
                }

                return _itemBuilderService.BuildWrapper(obj);
            }
            case ItemTypeDescriptor.CPUCooler: {
                var obj = _computerComponents.CPUCoolers.Get(id);
                if (obj == null) {
                    throw new NullReferenceException($"Кулера для процессора с номером {id} не существует");
                }
                return _itemBuilderService.BuildWrapper(obj);
            }
            case ItemTypeDescriptor.CPU: {
                var obj = _computerComponents.CPUs.Get(id);
                if (obj == null) {
                    throw new NullReferenceException($"Процессора с номером {id} не существует");
                }
                return _itemBuilderService.BuildWrapper(obj);
            }
            case ItemTypeDescriptor.Display: {
                var obj = _computerComponents.Displays.Get(id);
                if (obj == null) {
                    throw new NullReferenceException($"Монитора с номером {id} не существует");
                }
                return _itemBuilderService.BuildWrapper(obj);
            }
            case ItemTypeDescriptor.GraphicsCard: {
                var obj = _computerComponents.GraphicsCards.Get(id);
                if (obj == null) {
                    throw new NullReferenceException($"Видеокарты с номером {id} не существует");
                }
                return _itemBuilderService.BuildWrapper(obj);
            }
            case ItemTypeDescriptor.HDD: {
                var obj = _computerComponents.HDDs.Get(id);
                if (obj == null) {
                    throw new NullReferenceException($"Жёсткого диска с номером {id} не существует");
                }
                return _itemBuilderService.BuildWrapper(obj);
            }
            case ItemTypeDescriptor.Keyboard: {
                var obj = _computerComponents.Keyboards.Get(id);
                if (obj == null) {
                    throw new NullReferenceException($"Клавиатуры с номером {id} не существует");
                }
                return _itemBuilderService.BuildWrapper(obj);
            }
            case ItemTypeDescriptor.Motherboard: {
                var obj = _computerComponents.Motherboards.Get(id);
                if (obj == null) {
                    throw new NullReferenceException($"Материнской платы с номером {id} не существует");
                }
                return _itemBuilderService.BuildWrapper(obj);
            }
            case ItemTypeDescriptor.Mouse: {
                var obj = _computerComponents.Mouses.Get(id);
                if (obj == null) {
                    throw new NullReferenceException($"Мыши с номером {id} не существует");
                }
                return _itemBuilderService.BuildWrapper(obj);
            }
            case ItemTypeDescriptor.NVMeSSD: {
                var obj = _computerComponents.NVMeSSDs.Get(id);
                if (obj == null) {
                    throw new NullReferenceException($"Твердотельного накопителя NVMe с номером {id} не существует");
                }
                return _itemBuilderService.BuildWrapper(obj);
            }
            case ItemTypeDescriptor.PowerSupply: {
                var obj = _computerComponents.PowerSupplies.Get(id);
                if (obj == null) {
                    throw new NullReferenceException("Блока питания с номером {id} не существует");
                }
                return _itemBuilderService.BuildWrapper(obj);
            }
            case ItemTypeDescriptor.RAM: {
                var obj = _computerComponents.RAMs.Get(id);
                if (obj == null) {
                    throw new NullReferenceException($"Оперативной памяти с номером {id} не существует");
                }
                return _itemBuilderService.BuildWrapper(obj);
            }
            case ItemTypeDescriptor.SataSSD: {
                var obj = _computerComponents.SataSSDs.Get(id);
                if (obj == null) {
                    throw new NullReferenceException($"Твердотельного накопителя Sata с номером {id} не существует");
                }
                return _itemBuilderService.BuildWrapper(obj);
            }
            case ItemTypeDescriptor.PreparedAssembly: {
                var obj = _preparedAssemblyService.Get(id);
                if (obj == null) {
                    throw new NullReferenceException($"Готовой сборки с номером {id} не существует");
                }
                return _itemBuilderService.BuildWrapper(obj);
            }
            case ItemTypeDescriptor.Unknown: {
                throw new ArgumentException("Unknown descriptor");
            }
        }
        throw new ArgumentException("Unknown descriptor");
    }

    /// <summary>
    /// Получение списка товаров по дескриптору
    /// </summary>
    /// <param name="itemType">Дескриптор типа товара</param>
    /// /// <param name="pageNumber">Номер страницы</param>
    /// <param name="pageSize">Размер страницы</param>
    /// <returns>Список товаров указанного типа</returns>
    /// <exception cref="ArgumentException">Если был передан неизвестный дескриптор товара</exception>
    public PaginatedAnonymousItemWrapper List(ItemTypeDescriptor itemType, int pageNumber, int pageSize) {
        switch (itemType) {
            case ItemTypeDescriptor.ComputerCase: {
                var list = _computerComponents.ComputerCases.List();
                return new PaginatedAnonymousItemWrapper {
                    Count = list.Count,
                    Previous = pageNumber == 1 ? null : (pageNumber - 1).ToString(),
                    Next = (pageNumber + 1) * pageSize >= list.Count ? null : (pageNumber + 1).ToString(),
                    Items = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(
                        i => _itemBuilderService.BuildWrapper(i)
                    ).ToList()
                };
            }
            case ItemTypeDescriptor.CPUCooler: {
                var list = _computerComponents.CPUCoolers.List();
                return new PaginatedAnonymousItemWrapper {
                    Count = list.Count,
                    Previous = pageNumber == 1 ? null : (pageNumber - 1).ToString(),
                    Next = (pageNumber + 1) * pageSize >= list.Count ? null : (pageNumber + 1).ToString(),
                    Items = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(
                        i => _itemBuilderService.BuildWrapper(i)
                    ).ToList()
                };
            }
            case ItemTypeDescriptor.CPU: {
                var list = _computerComponents.CPUs.List();
                return new PaginatedAnonymousItemWrapper {
                    Count = list.Count,
                    Previous = pageNumber == 1 ? null : (pageNumber - 1).ToString(),
                    Next = (pageNumber + 1) * pageSize >= list.Count ? null : (pageNumber + 1).ToString(),
                    Items = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(
                        i => _itemBuilderService.BuildWrapper(i)
                    ).ToList()
                };
            }
            case ItemTypeDescriptor.Display: {
                var list = _computerComponents.Displays.List();
                return new PaginatedAnonymousItemWrapper {
                    Count = list.Count,
                    Previous = pageNumber == 1 ? null : (pageNumber - 1).ToString(),
                    Next = (pageNumber + 1) * pageSize >= list.Count ? null : (pageNumber + 1).ToString(),
                    Items = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(
                        i => _itemBuilderService.BuildWrapper(i)
                    ).ToList()
                };
            }
            case ItemTypeDescriptor.GraphicsCard: {
                var list = _computerComponents.GraphicsCards.List();
                return new PaginatedAnonymousItemWrapper {
                    Count = list.Count,
                    Previous = pageNumber == 1 ? null : (pageNumber - 1).ToString(),
                    Next = (pageNumber + 1) * pageSize >= list.Count ? null : (pageNumber + 1).ToString(),
                    Items = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(
                        i => _itemBuilderService.BuildWrapper(i)
                    ).ToList()
                };
            }
            case ItemTypeDescriptor.HDD: {
                var list = _computerComponents.HDDs.List();
                return new PaginatedAnonymousItemWrapper {
                    Count = list.Count,
                    Previous = pageNumber == 1 ? null : (pageNumber - 1).ToString(),
                    Next = (pageNumber + 1) * pageSize >= list.Count ? null : (pageNumber + 1).ToString(),
                    Items = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(
                        i => _itemBuilderService.BuildWrapper(i)
                    ).ToList()
                };
            }
            case ItemTypeDescriptor.Keyboard: {
                var list = _computerComponents.Keyboards.List();
                return new PaginatedAnonymousItemWrapper {
                    Count = list.Count,
                    Previous = pageNumber == 1 ? null : (pageNumber - 1).ToString(),
                    Next = (pageNumber + 1) * pageSize >= list.Count ? null : (pageNumber + 1).ToString(),
                    Items = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(
                        i => _itemBuilderService.BuildWrapper(i)
                    ).ToList()
                };
            }
            case ItemTypeDescriptor.Motherboard: {
                var list = _computerComponents.Motherboards.List();
                return new PaginatedAnonymousItemWrapper {
                    Count = list.Count,
                    Previous = pageNumber == 1 ? null : (pageNumber - 1).ToString(),
                    Next = (pageNumber + 1) * pageSize >= list.Count ? null : (pageNumber + 1).ToString(),
                    Items = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(
                        i => _itemBuilderService.BuildWrapper(i)
                    ).ToList()
                };
            }
            case ItemTypeDescriptor.Mouse: {
                var list = _computerComponents.Mouses.List();
                return new PaginatedAnonymousItemWrapper {
                    Count = list.Count,
                    Previous = pageNumber == 1 ? null : (pageNumber - 1).ToString(),
                    Next = (pageNumber + 1) * pageSize >= list.Count ? null : (pageNumber + 1).ToString(),
                    Items = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(
                        i => _itemBuilderService.BuildWrapper(i)
                    ).ToList()
                };
            }
            case ItemTypeDescriptor.NVMeSSD: {
                var list = _computerComponents.NVMeSSDs.List();
                return new PaginatedAnonymousItemWrapper {
                    Count = list.Count,
                    Previous = pageNumber == 1 ? null : (pageNumber - 1).ToString(),
                    Next = (pageNumber + 1) * pageSize >= list.Count ? null : (pageNumber + 1).ToString(),
                    Items = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(
                        i => _itemBuilderService.BuildWrapper(i)
                    ).ToList()
                };
            }
            case ItemTypeDescriptor.PowerSupply: {
                var list = _computerComponents.PowerSupplies.List();
                return new PaginatedAnonymousItemWrapper {
                    Count = list.Count,
                    Previous = pageNumber == 1 ? null : (pageNumber - 1).ToString(),
                    Next = (pageNumber + 1) * pageSize >= list.Count ? null : (pageNumber + 1).ToString(),
                    Items = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(
                        i => _itemBuilderService.BuildWrapper(i)
                    ).ToList()
                };
            }
            case ItemTypeDescriptor.RAM: {
                var list = _computerComponents.RAMs.List();
                return new PaginatedAnonymousItemWrapper {
                    Count = list.Count,
                    Previous = pageNumber == 1 ? null : (pageNumber - 1).ToString(),
                    Next = (pageNumber + 1) * pageSize >= list.Count ? null : (pageNumber + 1).ToString(),
                    Items = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(
                        i => _itemBuilderService.BuildWrapper(i)
                    ).ToList()
                };
            }
            case ItemTypeDescriptor.SataSSD: {
                var list = _computerComponents.SataSSDs.List();
                return new PaginatedAnonymousItemWrapper {
                    Count = list.Count,
                    Previous = pageNumber == 1 ? null : (pageNumber - 1).ToString(),
                    Next = (pageNumber + 1) * pageSize >= list.Count ? null : (pageNumber + 1).ToString(),
                    Items = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(
                        i => _itemBuilderService.BuildWrapper(i)
                    ).ToList()
                };
            }
            case ItemTypeDescriptor.PreparedAssembly: {
                var list = _preparedAssemblyService.List();
                return new PaginatedAnonymousItemWrapper {
                    Count = list.Count,
                    Previous = pageNumber == 0 ? null : (pageNumber - 1).ToString(),
                    Next = (pageNumber + 1) * pageSize >= list.Count ? null : (pageNumber + 1).ToString(),
                    Items = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(
                        i => _itemBuilderService.BuildWrapper(i)
                    ).ToList()
                };
            }
            case ItemTypeDescriptor.Unknown: {
                throw new ArgumentException("Unknown descriptor");
            }
        }
        throw new ArgumentException("Unknown descriptor");
    }

    /// <summary>
    /// Создание товара по его анонимной обёртке
    /// </summary>
    /// <param name="itemType">Дескриптор типа товара</param>
    /// <param name="item">Анонимная обёртка создаваемого товара</param>
    /// <exception cref="ArgumentException">Если был передан неизвестный дескриптор товара</exception>
    public void Create(ItemTypeDescriptor itemType, AnonymousItemWrapper item) {
        switch (itemType) {
            case ItemTypeDescriptor.ComputerCase: {
                var created = _itemBuilderService.BuildComputerCase(item);
                _computerComponents.ComputerCases.Create(created);
                item.Id = created.Id;
                return;
            }
            case ItemTypeDescriptor.CPUCooler: {
                var created = _itemBuilderService.BuildCPUCooler(item);
                _computerComponents.CPUCoolers.Create(created);
                item.Id = created.Id;
                return;
            }
            case ItemTypeDescriptor.CPU: {
                var created = _itemBuilderService.BuildCPU(item);
                _computerComponents.CPUs.Create(created);
                item.Id = created.Id;
                return;
            }
            case ItemTypeDescriptor.Display: {
                var created = _itemBuilderService.BuildDisplay(item);
                _computerComponents.Displays.Create(created);
                item.Id = created.Id;
                return;
            }
            case ItemTypeDescriptor.GraphicsCard: {
                var created = _itemBuilderService.BuildGraphicsCard(item);
                _computerComponents.GraphicsCards.Create(created);
                item.Id = created.Id;
                return;
            }
            case ItemTypeDescriptor.HDD: {
                var created = _itemBuilderService.BuildHDD(item);
                _computerComponents.HDDs.Create(created);
                item.Id = created.Id;
                return;
            }
            case ItemTypeDescriptor.Keyboard: {
                var created = _itemBuilderService.BuildKeyboard(item);
                _computerComponents.Keyboards.Create(created);
                item.Id = created.Id;
                return;
            }
            case ItemTypeDescriptor.Motherboard: {
                var created = _itemBuilderService.BuildMotherboard(item);
                _computerComponents.Motherboards.Create(created);
                item.Id = created.Id;
                return;
            }
            case ItemTypeDescriptor.Mouse: {
                var created = _itemBuilderService.BuildMouse(item);
                _computerComponents.Mouses.Create(created);
                item.Id = created.Id;
                return;
            }
            case ItemTypeDescriptor.NVMeSSD: {
                var created = _itemBuilderService.BuildNVMeSSD(item);
                _computerComponents.NVMeSSDs.Create(created);
                item.Id = created.Id;
                return;
            }
            case ItemTypeDescriptor.PowerSupply: {
                var created = _itemBuilderService.BuildPowerSupply(item);
                _computerComponents.PowerSupplies.Create(created);
                item.Id = created.Id;
                return;
            }
            case ItemTypeDescriptor.RAM: {
                var created = _itemBuilderService.BuildRAM(item);
                _computerComponents.RAMs.Create(created);
                item.Id = created.Id;
                return;
            }
            case ItemTypeDescriptor.SataSSD: {
                var created = _itemBuilderService.BuildSataSSD(item);
                _computerComponents.SataSSDs.Create(created);
                item.Id = created.Id;
                return;
            }
            case ItemTypeDescriptor.PreparedAssembly: {
                var created = _itemBuilderService.BuildPreparedAssembly(item);
                _preparedAssemblyService.Create(created);
                item.Id = created.Id;
                return;
            }
            case ItemTypeDescriptor.Unknown: {
                throw new ArgumentException("Unknown descriptor");
            }
        }
        throw new ArgumentException("Unknown descriptor");
    }
    
    /// <summary>
    /// Обновление информации о товаре
    /// </summary>
    /// <param name="itemType">Дескриптор типа товара</param>
    /// <param name="id">Номер обновляемого товара</param>
    /// <param name="wrapped">Анонимная обёртка с обновлёнными данными</param>
    /// <exception cref="ArgumentException">Если был передан неизвестный дескриптор товара</exception>
    public void Update(ItemTypeDescriptor itemType, int id, AnonymousItemWrapper wrapped) {
        
        switch (itemType) {
            case ItemTypeDescriptor.ComputerCase: {
                _computerComponents.ComputerCases.Update(id, _itemBuilderService.BuildComputerCase(wrapped));
                return;
            }
            case ItemTypeDescriptor.CPUCooler: {
                _computerComponents.CPUCoolers.Update(id, _itemBuilderService.BuildCPUCooler(wrapped));
                return;
            }
            case ItemTypeDescriptor.CPU: {
                _computerComponents.CPUs.Update(id, _itemBuilderService.BuildCPU(wrapped));
                return;
            }
            case ItemTypeDescriptor.Display: {
                _computerComponents.Displays.Update(id, _itemBuilderService.BuildDisplay(wrapped));
                return;
            }
            case ItemTypeDescriptor.GraphicsCard: {
                _computerComponents.GraphicsCards.Update(id, _itemBuilderService.BuildGraphicsCard(wrapped));
                return;
            }
            case ItemTypeDescriptor.HDD: {
                _computerComponents.HDDs.Update(id, _itemBuilderService.BuildHDD(wrapped));
                return;
            }
            case ItemTypeDescriptor.Keyboard: {
                _computerComponents.Keyboards.Update(id, _itemBuilderService.BuildKeyboard(wrapped));
                return;
            }
            case ItemTypeDescriptor.Motherboard: {
                _computerComponents.Motherboards.Update(id, _itemBuilderService.BuildMotherboard(wrapped));
                return;
            }
            case ItemTypeDescriptor.Mouse: {
                _computerComponents.Mouses.Update(id, _itemBuilderService.BuildMouse(wrapped));
                return;
            }
            case ItemTypeDescriptor.NVMeSSD: {
                _computerComponents.NVMeSSDs.Update(id, _itemBuilderService.BuildNVMeSSD(wrapped));
                return;
            }
            case ItemTypeDescriptor.PowerSupply: {
                _computerComponents.PowerSupplies.Update(id, _itemBuilderService.BuildPowerSupply(wrapped));
                return;
            }
            case ItemTypeDescriptor.RAM: {
                _computerComponents.RAMs.Update(id, _itemBuilderService.BuildRAM(wrapped));
                return;
            }
            case ItemTypeDescriptor.SataSSD: {
                _computerComponents.SataSSDs.Update(id, _itemBuilderService.BuildSataSSD(wrapped));
                return;
            }
            case ItemTypeDescriptor.PreparedAssembly: {
                _preparedAssemblyService.Update(id, _itemBuilderService.BuildPreparedAssembly(wrapped));
                return;
            }
            case ItemTypeDescriptor.Unknown: {
                throw new ArgumentException("Unknown descriptor");
            }
        }
        throw new ArgumentException("Unknown descriptor");
    }

    /// <summary>
    /// Удаление товара
    /// </summary>
    /// <param name="itemType">Дескриптор типа товара</param>
    /// <param name="id">Номер удаляемого товара</param>
    /// <exception cref="ArgumentException">Если был передан неизвестный дескриптор товара</exception>
    public void Delete(ItemTypeDescriptor itemType, int id) {
        
        switch (itemType) {
            case ItemTypeDescriptor.ComputerCase: {
                _computerComponents.ComputerCases.Delete(id);
                return;
            }
            case ItemTypeDescriptor.CPUCooler: {
                _computerComponents.CPUCoolers.Delete(id);
                return;
            }
            case ItemTypeDescriptor.CPU: {
                _computerComponents.CPUs.Delete(id);
                return;
            }
            case ItemTypeDescriptor.Display: {
                _computerComponents.Displays.Delete(id);
                return;
            }
            case ItemTypeDescriptor.GraphicsCard: {
                _computerComponents.GraphicsCards.Delete(id);
                return;
            }
            case ItemTypeDescriptor.HDD: {
                _computerComponents.HDDs.Delete(id);
                return;
            }
            case ItemTypeDescriptor.Keyboard: {
                _computerComponents.Keyboards.Delete(id);
                return;
            }
            case ItemTypeDescriptor.Motherboard: {
                _computerComponents.Motherboards.Delete(id);
                return;
            }
            case ItemTypeDescriptor.Mouse: {
                _computerComponents.Mouses.Delete(id);
                return;
            }
            case ItemTypeDescriptor.NVMeSSD: {
                _computerComponents.NVMeSSDs.Delete(id);
                return;
            }
            case ItemTypeDescriptor.PowerSupply: {
                _computerComponents.PowerSupplies.Delete(id);
                return;
            }
            case ItemTypeDescriptor.RAM: {
                _computerComponents.RAMs.Delete(id);
                return;
            }
            case ItemTypeDescriptor.SataSSD: {
                _computerComponents.SataSSDs.Delete(id);
                return;
            }
            case ItemTypeDescriptor.PreparedAssembly: {
                _preparedAssemblyService.Delete(id);
                return;
            }
            case ItemTypeDescriptor.Unknown: {
                throw new ArgumentException("Unknown descriptor");
            }
        }
        throw new ArgumentException("Unknown descriptor");
    }

    /// <summary>
    /// Сохранение контекста
    /// </summary>
    /// <param name="itemType">Дескриптор типа товара</param>
    /// <exception cref="ArgumentException">Если был передан неизвестный дескриптор товара</exception>
    public void Save(ItemTypeDescriptor itemType) {
        switch (itemType) {
            case ItemTypeDescriptor.ComputerCase: {
                _computerComponents.ComputerCases.Save();
                return;
            }
            case ItemTypeDescriptor.CPUCooler: {
                _computerComponents.CPUCoolers.Save();
                return;
            }
            case ItemTypeDescriptor.CPU: {
                _computerComponents.CPUs.Save();
                return;
            }
            case ItemTypeDescriptor.Display: {
                _computerComponents.Displays.Save();
                return;
            }
            case ItemTypeDescriptor.GraphicsCard: {
                _computerComponents.GraphicsCards.Save();
                return;
            }
            case ItemTypeDescriptor.HDD: {
                _computerComponents.HDDs.Save();
                return;
            }
            case ItemTypeDescriptor.Keyboard: {
                _computerComponents.Keyboards.Save();
                return;
            }
            case ItemTypeDescriptor.Motherboard: {
                _computerComponents.Motherboards.Save();
                return;
            }
            case ItemTypeDescriptor.Mouse: {
                _computerComponents.Mouses.Save();
                return;
            }
            case ItemTypeDescriptor.NVMeSSD: {
                _computerComponents.NVMeSSDs.Save();
                return;
            }
            case ItemTypeDescriptor.PowerSupply: {
                _computerComponents.PowerSupplies.Save();
                return;
            }
            case ItemTypeDescriptor.RAM: {
                _computerComponents.RAMs.Save();
                return;
            }
            case ItemTypeDescriptor.SataSSD: {
                _computerComponents.SataSSDs.Save();
                return;
            }
            case ItemTypeDescriptor.PreparedAssembly: {
                _preparedAssemblyService.Save();
                return;
            }
            case ItemTypeDescriptor.Unknown: {
                throw new ArgumentException("Unknown descriptor");
            }
        }
        throw new ArgumentException("Unknown descriptor");
    }

    /// <summary>
    /// Фильтрация списка товаров по критериям
    /// </summary>
    /// <param name="itemType">Дескриптор типа товаров</param>
    /// <param name="filters">Фильтры</param>
    /// <param name="pageNumber">Номер страницы</param>
    /// <param name="pageSize">Размер страницы</param>
    /// <returns>Отфильтрованный список</returns>
    /// <exception cref="ArgumentException">Если был передан неизвестный дескриптор товара</exception>
    public PaginatedAnonymousItemWrapper Filter(ItemTypeDescriptor itemType, IDictionary<string, dynamic> filters, int pageNumber, int pageSize) {
        FilterContainerWrapper container = _filterTransformerService.TransformFilters(filters);
        switch (itemType) {
            case ItemTypeDescriptor.ComputerCase: {
                var list = _computerComponents.ComputerCases.Filter(container);
                return new PaginatedAnonymousItemWrapper {
                    Count = list.Count,
                    Previous = pageNumber == 1 ? null : (pageNumber - 1).ToString(),
                    Next = (pageNumber + 1) * pageSize >= list.Count ? null : (pageNumber + 1).ToString(),
                    Items = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(
                        i => _itemBuilderService.BuildWrapper(i)
                    ).ToList()
                };
            }
            case ItemTypeDescriptor.CPUCooler: {
                var list = _computerComponents.CPUCoolers.Filter(container);
                return new PaginatedAnonymousItemWrapper {
                    Count = list.Count,
                    Previous = pageNumber == 1 ? null : (pageNumber - 1).ToString(),
                    Next = (pageNumber + 1) * pageSize >= list.Count ? null : (pageNumber + 1).ToString(),
                    Items = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(
                        i => _itemBuilderService.BuildWrapper(i)
                    ).ToList()
                };
            }
            case ItemTypeDescriptor.CPU: {
                var list = _computerComponents.CPUs.Filter(container);
                return new PaginatedAnonymousItemWrapper {
                    Count = list.Count,
                    Previous = pageNumber == 1 ? null : (pageNumber - 1).ToString(),
                    Next = (pageNumber + 1) * pageSize >= list.Count ? null : (pageNumber + 1).ToString(),
                    Items = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(
                        i => _itemBuilderService.BuildWrapper(i)
                    ).ToList()
                };
            }
            case ItemTypeDescriptor.Display: {
                var list = _computerComponents.Displays.Filter(container);
                return new PaginatedAnonymousItemWrapper {
                    Count = list.Count,
                    Previous = pageNumber == 1 ? null : (pageNumber - 1).ToString(),
                    Next = (pageNumber + 1) * pageSize >= list.Count ? null : (pageNumber + 1).ToString(),
                    Items = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(
                        i => _itemBuilderService.BuildWrapper(i)
                    ).ToList()
                };
            }
            case ItemTypeDescriptor.GraphicsCard: {
                var list = _computerComponents.GraphicsCards.Filter(container);
                return new PaginatedAnonymousItemWrapper {
                    Count = list.Count,
                    Previous = pageNumber == 1 ? null : (pageNumber - 1).ToString(),
                    Next = (pageNumber + 1) * pageSize >= list.Count ? null : (pageNumber + 1).ToString(),
                    Items = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(
                        i => _itemBuilderService.BuildWrapper(i)
                    ).ToList()
                };
            }
            case ItemTypeDescriptor.HDD: {
                var list = _computerComponents.HDDs.Filter(container);
                return new PaginatedAnonymousItemWrapper {
                    Count = list.Count,
                    Previous = pageNumber == 1 ? null : (pageNumber - 1).ToString(),
                    Next = (pageNumber + 1) * pageSize >= list.Count ? null : (pageNumber + 1).ToString(),
                    Items = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(
                        i => _itemBuilderService.BuildWrapper(i)
                    ).ToList()
                };
            }
            case ItemTypeDescriptor.Keyboard: {
                var list = _computerComponents.Keyboards.Filter(container);
                return new PaginatedAnonymousItemWrapper {
                    Count = list.Count,
                    Previous = pageNumber == 1 ? null : (pageNumber - 1).ToString(),
                    Next = (pageNumber + 1) * pageSize >= list.Count ? null : (pageNumber + 1).ToString(),
                    Items = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(
                        i => _itemBuilderService.BuildWrapper(i)
                    ).ToList()
                };
            }
            case ItemTypeDescriptor.Motherboard: {
                var list = _computerComponents.Motherboards.Filter(container);
                return new PaginatedAnonymousItemWrapper {
                    Count = list.Count,
                    Previous = pageNumber == 1 ? null : (pageNumber - 1).ToString(),
                    Next = (pageNumber + 1) * pageSize >= list.Count ? null : (pageNumber + 1).ToString(),
                    Items = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(
                        i => _itemBuilderService.BuildWrapper(i)
                    ).ToList()
                };
            }
            case ItemTypeDescriptor.Mouse: {
                var list = _computerComponents.Mouses.Filter(container);
                return new PaginatedAnonymousItemWrapper {
                    Count = list.Count,
                    Previous = pageNumber == 1 ? null : (pageNumber - 1).ToString(),
                    Next = (pageNumber + 1) * pageSize >= list.Count ? null : (pageNumber + 1).ToString(),
                    Items = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(
                        i => _itemBuilderService.BuildWrapper(i)
                    ).ToList()
                };
            }
            case ItemTypeDescriptor.NVMeSSD: {
                var list = _computerComponents.NVMeSSDs.Filter(container);
                return new PaginatedAnonymousItemWrapper {
                    Count = list.Count,
                    Previous = pageNumber == 1 ? null : (pageNumber - 1).ToString(),
                    Next = (pageNumber + 1) * pageSize >= list.Count ? null : (pageNumber + 1).ToString(),
                    Items = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(
                        i => _itemBuilderService.BuildWrapper(i)
                    ).ToList()
                };
            }
            case ItemTypeDescriptor.PowerSupply: {
                var list = _computerComponents.PowerSupplies.Filter(container);
                return new PaginatedAnonymousItemWrapper {
                    Count = list.Count,
                    Previous = pageNumber == 1 ? null : (pageNumber - 1).ToString(),
                    Next = (pageNumber + 1) * pageSize >= list.Count ? null : (pageNumber + 1).ToString(),
                    Items = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(
                        i => _itemBuilderService.BuildWrapper(i)
                    ).ToList()
                };
            }
            case ItemTypeDescriptor.RAM: {
                var list = _computerComponents.RAMs.Filter(container);
                return new PaginatedAnonymousItemWrapper {
                    Count = list.Count,
                    Previous = pageNumber == 1 ? null : (pageNumber - 1).ToString(),
                    Next = (pageNumber + 1) * pageSize >= list.Count ? null : (pageNumber + 1).ToString(),
                    Items = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(
                        i => _itemBuilderService.BuildWrapper(i)
                    ).ToList()
                };
            }
            case ItemTypeDescriptor.SataSSD: {
                var list = _computerComponents.SataSSDs.Filter(container);
                return new PaginatedAnonymousItemWrapper {
                    Count = list.Count,
                    Previous = pageNumber == 1 ? null : (pageNumber - 1).ToString(),
                    Next = (pageNumber + 1) * pageSize >= list.Count ? null : (pageNumber + 1).ToString(),
                    Items = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(
                        i => _itemBuilderService.BuildWrapper(i)
                    ).ToList()
                };
            }
            case ItemTypeDescriptor.PreparedAssembly: {
                var list = _preparedAssemblyService.Filter(container);
                return new PaginatedAnonymousItemWrapper {
                    Count = list.Count,
                    Previous = pageNumber == 1 ? null : (pageNumber - 1).ToString(),
                    Next = (pageNumber + 1) * pageSize >= list.Count ? null : (pageNumber + 1).ToString(),
                    Items = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(
                        i => _itemBuilderService.BuildWrapper(i)
                    ).ToList()
                };
            }
            case ItemTypeDescriptor.Unknown: {
                throw new ArgumentException("Unknown descriptor");
            }
        }
        throw new ArgumentException("Unknown descriptor");
    }

    /// <summary>
    /// Получение информации о возможных фильтрах для данного типа товара
    /// </summary>
    /// <param name="itemType">Дескриптор типа товаров</param>
    /// <returns>Список возможных фильтров</returns>
    /// <exception cref="ArgumentException">Если был передан неизвестный дескриптор товара</exception>
    public IList<FilterDescription> FilterData(ItemTypeDescriptor itemType) {
        switch (itemType) {
            case ItemTypeDescriptor.ComputerCase: {
                return _computerComponents.ComputerCases.FilterData();
            }
            case ItemTypeDescriptor.CPUCooler: {
                return _computerComponents.CPUCoolers.FilterData();
            }
            case ItemTypeDescriptor.CPU: {
                return _computerComponents.CPUs.FilterData();
            }
            case ItemTypeDescriptor.Display: {
                return _computerComponents.Displays.FilterData();
            }
            case ItemTypeDescriptor.GraphicsCard: {
                return _computerComponents.GraphicsCards.FilterData();
            }
            case ItemTypeDescriptor.HDD: {
                return _computerComponents.HDDs.FilterData();
            }
            case ItemTypeDescriptor.Keyboard: {
                return _computerComponents.Keyboards.FilterData();
            }
            case ItemTypeDescriptor.Motherboard: {
                return _computerComponents.Motherboards.FilterData();
            }
            case ItemTypeDescriptor.Mouse: {
                return _computerComponents.Mouses.FilterData();
            }
            case ItemTypeDescriptor.NVMeSSD: {
                return _computerComponents.NVMeSSDs.FilterData();
            }
            case ItemTypeDescriptor.PowerSupply: {
                return _computerComponents.PowerSupplies.FilterData();
            }
            case ItemTypeDescriptor.RAM: {
                return _computerComponents.RAMs.FilterData();
            }
            case ItemTypeDescriptor.SataSSD: {
                return _computerComponents.SataSSDs.FilterData();
            }
            case ItemTypeDescriptor.PreparedAssembly:
                return _preparedAssemblyService.FilterData();
            case ItemTypeDescriptor.Unknown: {
                throw new ArgumentException("Unknown descriptor");
            }
        }
        throw new ArgumentException("Unknown descriptor");
    }
}