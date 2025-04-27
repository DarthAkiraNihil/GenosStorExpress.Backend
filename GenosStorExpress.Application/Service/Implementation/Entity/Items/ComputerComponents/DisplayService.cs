using GenosStorExpress.Application.Service.Implementation.Base;
using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Application.Service.Interface.Entity.Orders;
using GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;
using GenosStorExpress.Application.Wrappers.Filters;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.ComputerComponents {
    /// <summary>
    /// Реализация сервиса мониторов
    /// </summary>
    public class DisplayService: AbstractComputerComponentService, IDisplayService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly IDisplayRepository _displays;
        
        private readonly IDefinitionService _definitionService;
        private readonly IMatrixTypeService _matrixTypeService;
        private readonly IUnderlightService _underlightService;
        private readonly IVesaSizeService _vesaSizeService;
        
        /// <summary>
        /// Стандартный конструктор
        /// </summary>
        /// <param name="itemTypeService">Сервис типов предметов</param>
        /// <param name="activeDiscountService">Сервис скидок</param>
        /// <param name="vendorService">Сервис производителей</param>
        /// <param name="repositories">Репозитории проекта</param>
        /// <param name="definitionService">Сервис разрешений мониторов</param>
        /// <param name="matrixTypeService">Сервис типов матриц</param>
        /// <param name="underlightService">Сервис подсветок мониторов</param>
        /// <param name="vesaSizeService">Сервис размеров Vesa</param>
        public DisplayService(IItemTypeService itemTypeService, IActiveDiscountService activeDiscountService, IVendorService vendorService, IGenosStorExpressRepositories repositories, IDefinitionService definitionService, IMatrixTypeService matrixTypeService, IUnderlightService underlightService, IVesaSizeService vesaSizeService) : base(itemTypeService, activeDiscountService, vendorService) {
            _repositories = repositories;
            _definitionService = definitionService;
            _matrixTypeService = matrixTypeService;
            _underlightService = underlightService;
            _vesaSizeService = vesaSizeService;
            _displays = _repositories.Items.ComputerComponents.Displays;
        }
        
        /// <summary>
        /// Создание сущности монитора из обёртки
        /// </summary>
        /// <param name="item">Обёрнутый монитор</param>
        /// <exception cref="NullReferenceException">Если указанного производителя, разрешения, типа матрицы, типа подсветки или размера Vesa не существует</exception>
        public void Create(DisplayWrapper item) {
            var created = new Display();
            _setEntityPropertiesFromWrapper(created, item);
            
            created.MaxUpdateFrequency = item.MaxUpdateFrequency;
            created.ScreenDiagonal = item.ScreenDiagonal;
            
            var definition = _definitionService.GetRaw(item.Definition.Id);
            if (definition == null) {
                throw new NullReferenceException($"Разрешения экрана с номером {item.Definition.Id} ({item.Definition.Width}x{item.Definition.Height}) не существует");
            }
            created.Definition = definition;
            
            var matrixType = _matrixTypeService.GetEntityFromString(item.MatrixType);
            if (matrixType == null) {
                throw new NullReferenceException($"Типа матрицы экрана {item.MatrixType} не существует");
            }
            created.MatrixType = matrixType;
            
            var underlight = _underlightService.GetEntityFromString(item.Underlight);
            if (underlight == null) {
                throw new NullReferenceException($"Типа подсветки экрана {item.Underlight} не существует");
            }
            created.Underlight = underlight;
            
            var vesaSize = _vesaSizeService.GetEntityFromString(item.VesaSize);
            if (vesaSize == null) {
                throw new NullReferenceException($"Размера Vesa {item.VesaSize} не существует");
            }
            created.VesaSize = vesaSize;
            
            _displays.Create(created);
            item.Id = created.Id;
        }

        /// <summary>
        /// Получение объекта-обёртки сущности монитора по его номеру
        /// </summary>
        /// <param name="id">Номер монитора</param>
        /// <returns>Обёрнутый монитор или null в случае его отсутствия</returns>
        public DisplayWrapper? Get(int id) {
            Display? obj = _displays.Get(id);
            if (obj == null) {
                return null;
            }
            var wrapped = new DisplayWrapper();
            
            _setWrapperPropertiesFromEntity(obj, wrapped);
            wrapped.MaxUpdateFrequency = obj.MaxUpdateFrequency;
            wrapped.ScreenDiagonal = obj.ScreenDiagonal;
            wrapped.Definition = _definitionService.Get(obj.Definition.Id)!;
            wrapped.MatrixType = obj.MatrixType.Name;
            wrapped.Underlight = obj.Underlight.Name;
            wrapped.VesaSize = obj.VesaSize.Name;
            
            return wrapped;
        }

        /// <summary>
        /// Получение списка всех мониторов в виде обёрток
        /// </summary>
        /// <returns>Список обёрнутых мониторов</returns>
        public List<DisplayWrapper> List() {
            return _displays.List().Select(obj => {
                var wrapped = new DisplayWrapper();
            
                _setWrapperPropertiesFromEntity(obj, wrapped);
                wrapped.MaxUpdateFrequency = obj.MaxUpdateFrequency;
                wrapped.ScreenDiagonal = obj.ScreenDiagonal;
                wrapped.Definition = _definitionService.Get(obj.Definition.Id)!;
                wrapped.MatrixType = obj.MatrixType.Name;
                wrapped.Underlight = obj.Underlight.Name;
                wrapped.VesaSize = obj.VesaSize.Name;
            
                return wrapped;
            }).ToList();
        }

        /// <summary>
        /// Обновление сущности монитора
        /// </summary>
        /// <param name="id">Номер монитора</param>
        /// <param name="item">Изменённые данные</param>
        /// <exception cref="NullReferenceException">Если указанного производителя, разрешения, типа матрицы, типа подсветки или размера Vesa не существует</exception>
        public void Update(int id, DisplayWrapper item) {
            var obj = _displays.Get(id);
            if (obj == null) {
                throw new NullReferenceException($"Монитора с номером {id} не существует");
            }
            _setEntityPropertiesFromWrapper(obj, item);
            
            obj.MaxUpdateFrequency = item.MaxUpdateFrequency;
            obj.ScreenDiagonal = item.ScreenDiagonal;
            
            var definition = _definitionService.GetRaw(item.Definition.Id);
            if (definition == null) {
                throw new NullReferenceException($"Разрешения экрана с номером {item.Definition.Id} ({item.Definition.Width}x{item.Definition.Height}) не существует");
            }
            obj.Definition = definition;
            
            var matrixType = _matrixTypeService.GetEntityFromString(item.MatrixType);
            if (matrixType == null) {
                throw new NullReferenceException($"Типа матрицы экрана {item.MatrixType} не существует");
            }
            obj.MatrixType = matrixType;
            
            var underlight = _underlightService.GetEntityFromString(item.Underlight);
            if (underlight == null) {
                throw new NullReferenceException($"Типа подсветки экрана {item.Underlight} не существует");
            }
            obj.Underlight = underlight;
            
            var vesaSize = _vesaSizeService.GetEntityFromString(item.VesaSize);
            if (vesaSize == null) {
                throw new NullReferenceException($"Размера Vesa {item.VesaSize} не существует");
            }
            obj.VesaSize = vesaSize;
            
            _displays.Update(obj);
        }

        /// <summary>
        /// Удаление сущности монитора
        /// </summary>
        /// <param name="id">Номер монитора</param>
        public void Delete(int id) {
            _repositories.Items.ComputerComponents.Displays.Delete(id);
        }

        /// <summary>
        /// Сохранение базы данных
        /// </summary>
        /// <returns>Количество изменений</returns>
        public int Save() {
            return _repositories.Save();
        }
        
        /// <summary>
        /// Фильтрация списка сущностей мониторов
        /// </summary>
        /// <param name="filters">Список фильтров</param>
        /// <returns>Отфильтрованный список обёрток мониторов</returns>
        public List<DisplayWrapper> Filter(List<Func<DisplayWrapper, bool>> filters) {
            var result = List();
            foreach (var filter in filters) {
                result = result.Where(filter).ToList();
            }
            return result;
        }

        public IList<DisplayWrapper> Filter(FilterContainerWrapper filters) {
            var filters_ = new List<Func<DisplayWrapper, bool>>();

            IDictionary<string, RangeFilterWrapper> ranges = filters.Ranges;
            IDictionary<string, ChoiceFilterWrapper> choices = filters.Choices;
            IDictionary<string, bool> havings = filters.Havings;

            if (ranges.ContainsKey("max_update_frequency")) {
                if (ranges["max_update_frequency"].IsValid()) {
                    filters_.Add(
                        i => ranges["max_update_frequency"].From <= i.MaxUpdateFrequency && i.MaxUpdateFrequency <= ranges["max_update_frequency"].To
                    );
                }
            }

            if (ranges.ContainsKey("screen_diagonal")) {
                if (ranges["screen_diagonal"].IsValid()) {
                    filters_.Add(
                        i => ranges["screen_diagonal"].From <= i.ScreenDiagonal && i.ScreenDiagonal <= ranges["screen_diagonal"].To
                    );
                }
            }

            if (choices.ContainsKey("matrix_type")) {
                filters_.Add(
                    i => choices["matrix_type"].CreateFilterClosure(n => n.Contains(i.MatrixType))
                );
            }

            if (choices.ContainsKey("underlight")) {
                filters_.Add(
                    i => choices["underlight"].CreateFilterClosure(n => n.Contains(i.Underlight))
                );
            }

            if (choices.ContainsKey("vesa_size")) {
                filters_.Add(
                    i => choices["vesa_size"].CreateFilterClosure(n => n.Contains(i.VesaSize))
                );
            }
            
            if (ranges.ContainsKey("price")) {
                if (ranges["price"].IsValid()) {
                    filters_.Add(
                        i => ranges["price"].From <= i.Price && i.Price <= ranges["price"].To
                    );
                }
            }

            if (ranges.ContainsKey("tdp")) {
                if (ranges["tdp"].IsValid()) {
                    filters_.Add(
                        i => ranges["tdp"].From <= i.TDP && i.TDP <= ranges["tdp"].To
                    );
                }
            }

            if (choices.ContainsKey("vendors")) {
                filters_.Add(
                    i => choices["vendors"].CreateFilterClosure(n => n.Contains(i.Vendor))
                );
            }
            
            if (filters.Name.Length != 0) {
                filters_.Add(
                    i => i.Name.Contains(filters.Name)
                );
            }
            
            var result = List();
            foreach (var filter in filters_) {
                result = result.Where(filter).ToList();
            }
            return result;
        }
        
        /// <summary>
        /// Получение данных о возможных фильтрах товара
        /// </summary>
        /// <returns>Список возможных фильтров</returns>
        public IList<FilterDescription> FilterData() {
            return new List<FilterDescription> {
                new FilterDescription {
                    Type = FilterType.Range,
                    Name = "price",
                    VerboseName = "Цена"
                },
                new FilterDescription {
                    Type = FilterType.Choice,
                    Name = "vendors",
                    VerboseName = "Производители",
                    Choices = _repositories.Items.Characteristics.Vendors.List().Select(i => i.Name).ToList()
                },
                new FilterDescription {
                    Type = FilterType.Range,
                    Name = "tdp",
                    VerboseName = "Потребляемая мощность"
                },
                new FilterDescription {
                    Type = FilterType.Choice,
                    Name = "matrix_type",
                    VerboseName = "Тип матрицы",
                    Choices = _repositories.Items.Characteristics.MatrixTypes.List().Select(i => i.Name).ToList()
                },
                new FilterDescription {
                    Type = FilterType.Choice,
                    Name = "underlight",
                    VerboseName = "Тип подсветки",
                    Choices = _repositories.Items.Characteristics.Underlights.List().Select(i => i.Name).ToList()
                },
                new FilterDescription {
                    Type = FilterType.Choice,
                    Name = "vesa_size",
                    VerboseName = "Размер Vesa",
                    Choices = _repositories.Items.Characteristics.VesaSizes.List().Select(i => i.Name).ToList()
                },
                new FilterDescription {
                    Type = FilterType.Range,
                    Name = "max_update_frequency",
                    VerboseName = "Макс. частота обновления"
                },
                new FilterDescription {
                    Type = FilterType.Range,
                    Name = "screen_diagonal",
                    VerboseName = "Размер диагонали"
                },
            };
        }
    }
}