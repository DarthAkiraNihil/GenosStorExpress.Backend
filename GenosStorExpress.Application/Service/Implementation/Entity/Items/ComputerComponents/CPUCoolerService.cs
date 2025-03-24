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
    /// Реализация сервиса кулеров для процессора
    /// </summary>
    public class CPUCoolerService: AbstractComputerComponentService, ICPUCoolerService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly ICPUCoolerRepository _cpuCoolers;
        private readonly ICoolerMaterialService _coolerMaterials;
        
        /// <summary>
        /// Стандартный конструктор
        /// </summary>
        /// <param name="itemTypeService">Сервис типов предметов</param>
        /// <param name="vendorService">Сервис производителей</param>
        /// <param name="repositories">Репозитории проекта</param>
        /// <param name="coolerMaterials">Сервис материалов кулеров</param>

        public CPUCoolerService(IItemTypeService itemTypeService, IVendorService vendorService, IGenosStorExpressRepositories repositories, ICoolerMaterialService coolerMaterials) : base(itemTypeService, vendorService) {
            _repositories = repositories;
            _coolerMaterials = coolerMaterials;
            _cpuCoolers = _repositories.Items.ComputerComponents.CPUCoolers;
        }
        
        /// <summary>
        /// Создание сущности кулера для процессора из обёртки
        /// </summary>
        /// <param name="item">Обёрнутый кулер для процессора</param>
        /// <exception cref="NullReferenceException">Если указанного производителя или материала кулера не существует</exception>
        public void Create(CPUCoolerWrapper item) {
            
            var created = new CPUCooler();
            _setEntityPropertiesFromWrapper(created, item);
            
            created.MaxFanRPM = item.MaxFanRPM;
            created.TubesCount = item.TubesCount;
            created.TubesDiameter = item.TubesDiameter;
            created.FanCount = item.FanCount;
            
            var foundationMaterial = _coolerMaterials.GetEntityFromString(item.FoundationMaterial);
            if (foundationMaterial == null) {
                throw new NullReferenceException($"Материала основания {item.FoundationMaterial} не существует");
            }
            created.FoundationMaterial = foundationMaterial;
            
            var radiatorMaterial = _coolerMaterials.GetEntityFromString(item.RadiatorMaterial);
            if (radiatorMaterial == null) {
                throw new NullReferenceException($"Материала радиатора {item.RadiatorMaterial} не существует");
            }
            created.RadiatorMaterial = radiatorMaterial;
            
            _cpuCoolers.Create(created);
        }
        
        /// <summary>
        /// Получение объекта-обёртки сущности кулера для процессора по его номеру
        /// </summary>
        /// <param name="id">Номер кулера</param>
        /// <returns>Обёрнутый кулер или null в случае его отсутствия</returns>
        public CPUCoolerWrapper? Get(int id) {
            
            CPUCooler? obj = _cpuCoolers.Get(id);
            
            if (obj == null) {
                return null;
            }
            
            var wrapped = new CPUCoolerWrapper();
            _setWrapperPropertiesFromEntity(obj, wrapped);
            
            wrapped.MaxFanRPM = obj.MaxFanRPM;
            wrapped.TubesCount = obj.TubesCount;
            wrapped.TubesDiameter = obj.TubesDiameter;
            wrapped.FanCount = obj.FanCount;
            wrapped.RadiatorMaterial = obj.RadiatorMaterial.Name;
            wrapped.FoundationMaterial = obj.FoundationMaterial.Name;
            
            return wrapped;
        }

        /// <summary>
        /// Получение списка всех кулеров для процессора в виде обёрток
        /// </summary>
        /// <returns>Список обёрнутых кулеров для процессора</returns>
        public List<CPUCoolerWrapper> List() {
            return _cpuCoolers.List().Select(obj => {
                var wrapped = new CPUCoolerWrapper();
                _setWrapperPropertiesFromEntity(obj, wrapped);
            
                wrapped.MaxFanRPM = obj.MaxFanRPM;
                wrapped.TubesCount = obj.TubesCount;
                wrapped.TubesDiameter = obj.TubesDiameter;
                wrapped.FanCount = obj.FanCount;
                wrapped.RadiatorMaterial = obj.RadiatorMaterial.Name;
                wrapped.FoundationMaterial = obj.FoundationMaterial.Name;
            
                return wrapped;
            }).ToList();
        }

        /// <summary>
        /// Обновление сущности кулера для процессора
        /// </summary>
        /// <param name="id">Номер кулера</param>
        /// <param name="item">Изменённые данные</param>
        /// <exception cref="NullReferenceException">Если указанного производителя или материала кулера не существует</exception>
        public void Update(int id, CPUCoolerWrapper item) {
            var obj = _cpuCoolers.Get(id);
            
            if (obj == null) {
                throw new NullReferenceException($"Кулера для процессора с номером {id} не существует");
            }
            _setEntityPropertiesFromWrapper(obj, item);
            
            obj.MaxFanRPM = item.MaxFanRPM;
            obj.TubesCount = item.TubesCount;
            obj.TubesDiameter = item.TubesDiameter;
            obj.FanCount = item.FanCount;
            var foundationMaterial = _coolerMaterials.GetEntityFromString(item.FoundationMaterial);
            if (foundationMaterial == null) {
                throw new NullReferenceException($"Материала основания {item.FoundationMaterial} не существует");
            }
            obj.FoundationMaterial = foundationMaterial;
            
            var radiatorMaterial = _coolerMaterials.GetEntityFromString(item.RadiatorMaterial);
            if (radiatorMaterial == null) {
                throw new NullReferenceException($"Материала радиатора {item.RadiatorMaterial} не существует");
            }
            obj.RadiatorMaterial = radiatorMaterial;
            
            _cpuCoolers.Update(obj);
        }

        /// <summary>
        /// Удаление сущности кулера для процессора
        /// </summary>
        /// <param name="id">Номер кулера для процессора</param>
        public void Delete(int id) {
            _cpuCoolers.Delete(id);
        }

        /// <summary>
        /// Сохранение базы данных
        /// </summary>
        /// <returns>Количество изменений</returns>
        public int Save() {
            return _repositories.Save();
        }
        
        /// <summary>
        /// Фильтрация списка сущностей кулеров для процессора
        /// </summary>
        /// <param name="filters">Список фильтров</param>
        /// <returns>Отфильтрованный список обёрток кулеров для процессора</returns>
        public List<CPUCoolerWrapper> Filter(List<Func<CPUCoolerWrapper, bool>> filters) {
            var result = List();
            foreach (var filter in filters) {
                result = result.Where(filter).ToList();
            }
            return result;
        }
    }
}