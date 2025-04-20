using GenosStorExpress.Application.Service.Implementation.Base;
using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;
using GenosStorExpress.Application.Wrappers.Filters;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.ComputerComponents {
    
    /// <summary>
    /// Реализация сервиса компьютерных корпусов
    /// </summary>
    public class ComputerCaseService: AbstractComputerComponentService, IComputerCaseService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly IComputerCaseRepository _computerCases;
        private readonly IComputerCaseTypesizeService _computerCaseTypesizeService;
        private readonly IMotherboardFormFactorService _motherboardFormFactorService;
        
        /// <summary>
        /// Стандартный конструктор
        /// </summary>
        /// <param name="itemTypeService">Сервис типов предметов</param>
        /// <param name="vendorService">Сервис производителей</param>
        /// <param name="repositories">Репозитории проекта</param>
        /// <param name="computerCaseTypesizeService">Сервис типоразмеров корпусов</param>
        /// <param name="motherboardFormFactorService">Сервис форм-факторов материнских плат</param>

        public ComputerCaseService(IItemTypeService itemTypeService, IVendorService vendorService, IGenosStorExpressRepositories repositories, IComputerCaseTypesizeService computerCaseTypesizeService, IMotherboardFormFactorService motherboardFormFactorService) : base(itemTypeService, vendorService) {
            _repositories = repositories;
            _computerCases = _repositories.Items.ComputerComponents.ComputerCases;
            _computerCaseTypesizeService = computerCaseTypesizeService;
            _motherboardFormFactorService = motherboardFormFactorService;
        }
        
        /// <summary>
        /// Создание сущности компьютерного корпуса из обёртки
        /// </summary>
        /// <param name="item">Обёрнутый компьютерный корпус</param>
        /// <exception cref="NullReferenceException">Если указанного производителя, типоразмера или форм-фактора мат. платы не существует</exception>
        public void Create(ComputerCaseWrapper item) {
            
            var created = new ComputerCase();
            
            _setEntityPropertiesFromWrapper(created, item);
            
            created.Length = item.Length;
            created.Width = item.Width;
            created.Height = item.Height;
            created.HasARGBLighting = item.HasARGBLighting;
            created.DrivesSlotsCount = item.DrivesSlotsCount;
            
            var typesize = _computerCaseTypesizeService.GetEntityFromString(item.Typesize);
            if (typesize == null) {
                throw new NullReferenceException($"Типоразмера корпуса {item.Typesize} существует");
            }
            created.Typesize = typesize;
            created.SupportedMotherboardFormFactors = item.SupportedMotherboardFormFactors
                                                        .Select(f => {
                                                            var formFactor = _motherboardFormFactorService.GetEntityFromString(f);
                                                            if (formFactor == null) {
                                                                throw new NullReferenceException($"Форм-фактора материнской платы {f} не существует");
                                                            }
                                                            return formFactor;
                                                        }).ToList();
            
            _computerCases.Create(created);
            item.Id = created.Id;
        }

        /// <summary>
        /// Получение объекта-обёртки сущности кулера для процессора по его номеру
        /// </summary>
        /// <param name="id">Номер корпуса</param>
        /// <returns>Обёрнутый корпус или null в случае его отсутствия</returns>
        public ComputerCaseWrapper? Get(int id) {
            ComputerCase? obj =  _computerCases.Get(id);

            if (obj == null) {
                return null;
            }
            
            var wrapped = new ComputerCaseWrapper();
            
            _setWrapperPropertiesFromEntity(obj, wrapped);
            
            wrapped.Length = obj.Length;
            wrapped.Width = obj.Width;
            wrapped.Height = obj.Height;
            wrapped.HasARGBLighting = obj.HasARGBLighting;
            wrapped.DrivesSlotsCount = obj.DrivesSlotsCount;
            wrapped.Typesize = obj.Typesize.Name;
            wrapped.SupportedMotherboardFormFactors = obj.SupportedMotherboardFormFactors
                                                         .Select(i => i.Name)
                                                         .ToList();

            return wrapped;
        }

        /// <summary>
        /// Получение списка всех компьютерных корпусов в виде обёрток
        /// </summary>
        /// <returns>Список обёрнутых компьютерных корпусов</returns>
        public List<ComputerCaseWrapper> List() {
            return _computerCases.List().Select(
                obj => {
                    var wrapped = new ComputerCaseWrapper();
            
                    _setWrapperPropertiesFromEntity(obj, wrapped);
            
                    wrapped.Length = obj.Length;
                    wrapped.Width = obj.Width;
                    wrapped.Height = obj.Height;
                    wrapped.HasARGBLighting = obj.HasARGBLighting;
                    wrapped.DrivesSlotsCount = obj.DrivesSlotsCount;
                    wrapped.Typesize = obj.Typesize.Name;

                    return wrapped;
                }
            ).ToList();
        }

        /// <summary>
        /// Обновление сущности компьютерного корпуса
        /// </summary>
        /// <param name="id">Номер кулера</param>
        /// <param name="item">Изменённые данные</param>
        /// <exception cref="NullReferenceException">Если указанного производителя, типоразмера или форм-фактора мат. платы не существует</exception>
        public void Update(int id, ComputerCaseWrapper item) {
            ComputerCase? obj = _computerCases.Get(id);
            
            if (obj == null) {
                throw new NullReferenceException($"Компьютерного корпуса с номером {id} не существует");
            }
            
            _setEntityPropertiesFromWrapper(obj, item);
            
            obj.Length = item.Length;
            obj.Width = item.Width;
            obj.Height = item.Height;
            obj.HasARGBLighting = item.HasARGBLighting;
            obj.DrivesSlotsCount = item.DrivesSlotsCount;
            
            var typesize = _computerCaseTypesizeService.GetEntityFromString(item.Typesize);
            if (typesize == null) {
                throw new NullReferenceException($"Типоразмера корпуса {item.Typesize} существует");
            }
            obj.Typesize = typesize;
            
            _computerCases.Update(obj);
        }

        /// <summary>
        /// Удаление сущности компьютерного корпуса
        /// </summary>
        /// <param name="id">Номер компьютерного корпуса</param>
        public void Delete(int id) {
            _repositories.Items.ComputerComponents.ComputerCases.Delete(id);
        }

        /// <summary>
        /// Фильтрация списка сущностей компьютерных корпусов
        /// </summary>
        /// <param name="filters">Список фильтров</param>
        /// <returns>Отфильтрованный список обёрток компьютерных корпусов</returns>
        
        public IList<ComputerCaseWrapper> Filter(FilterContainerWrapper filters) {
            
            var filters_ = new List<Func<ComputerCaseWrapper, bool>>();

            IDictionary<string, RangeFilterWrapper> ranges = filters.Ranges;
            IDictionary<string, ChoiceFilterWrapper> choices = filters.Choices;
            IDictionary<string, bool> havings = filters.Havings;
            
            
            if (ranges.ContainsKey("length")) {
                if (ranges["length"].IsValid()) {
                    filters_.Add(
                        i => ranges["length"].From <= i.Length && i.Length <= ranges["length"].To
                    );
                }
            }

            if (ranges.ContainsKey("width")) {
                if (ranges["width"].IsValid()) {
                    filters_.Add(
                        i => ranges["width"].From <= i.Width && i.Width <= ranges["width"].To
                    );
                }
            }

            if (ranges.ContainsKey("height")) {
                if (ranges["height"].IsValid()) {
                    filters_.Add(
                        i => ranges["height"].From <= i.Height && i.Height <= ranges["height"].To
                    );
                }
            }

            if (ranges.ContainsKey("drives_slots_count")) {
                if (ranges["drive_slots_count"].IsValid()) {
                    filters_.Add(
                        i => ranges["drive_slots_count"].From <= i.DrivesSlotsCount && i.DrivesSlotsCount <= ranges["drive_slots_count"].To
                    );
                }
            }

            if (choices.ContainsKey("typesize")) {
                filters_.Add(
                    i => choices["typesize"].CreateFilterClosure(n => n.Contains(i.Typesize))
                );
            }

            if (choices.ContainsKey("supported_motherboard_form_factors")) {
                filters_.Add(
                    i => choices["supported_motherboard_form_factors"].CreateFilterClosure(n => {
                        foreach (var type in i.SupportedMotherboardFormFactors) {
                            if (type == n) {
                                return true;
                            }
                        }

                        return false;
                    })
                );
            }

            if (havings.ContainsKey("has_argb_lighting")) {
                filters_.Add(
                    i => i.HasARGBLighting == havings["has_argb_lighting"]
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
                    Name = "length",
                    VerboseName = "Длина"
                },
                new FilterDescription {
                    Type = FilterType.Range,
                    Name = "width",
                    VerboseName = "width"
                },
                new FilterDescription {
                    Type = FilterType.Range,
                    Name = "height",
                    VerboseName = "height"
                },
                new FilterDescription {
                    Type = FilterType.Range,
                    Name = "drive_slots_count",
                    VerboseName = "drive_slots_count"
                },
                new FilterDescription {
                    Type = FilterType.Choice,
                    Name = "typesize",
                    VerboseName = "typesize",
                    Choices = _repositories.Items.Characteristics.ComputerCaseTypesizes.List().Select(i => i.Name).ToList()
                },
                new FilterDescription {
                    Type = FilterType.Choice,
                    Name = "supported_motherboard_form_factors",
                    VerboseName = "supported_motherboard_form_factors",
                    Choices = _repositories.Items.Characteristics.MotherboardFormFactors.List().Select(i => i.Name).ToList()
                },
                new FilterDescription {
                    Type = FilterType.Having,
                    Name = "has_argb_lighting",
                    VerboseName = "has_argb_lighting",
                },
            };
        }

        /// <summary>
        /// Сохранение базы данных
        /// </summary>
        /// <returns>Количество изменений</returns>
        public int Save() {
            return _repositories.Save();
        }
    }
}