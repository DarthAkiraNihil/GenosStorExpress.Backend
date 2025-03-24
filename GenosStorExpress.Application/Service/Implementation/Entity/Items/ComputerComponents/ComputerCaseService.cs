using GenosStorExpress.Application.Service.Implementation.Base;
using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;
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
        public List<ComputerCaseWrapper> Filter(List<Func<ComputerCaseWrapper, bool>> filters) {
            var result = List();
            foreach (var filter in filters) {
                result = result.Where(filter).ToList();
            }
            return result;
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