using GenosStorExpress.Application.Service.Implementation.Base;
using GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;
using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.ComputerComponents {
    public class ComputerCaseService: AbstractComputerComponentService, IComputerCaseService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly IComputerCaseRepository _computerCases;
        private readonly IComputerCaseTypesizeService _computerCaseTypesizeService;

        public ComputerCaseService(IItemTypeService itemTypeService, IVendorService vendorService, IGenosStorExpressRepositories repositories, IComputerCaseTypesizeService computerCaseTypesizeService) : base(itemTypeService, vendorService) {
            _repositories = repositories;
            _computerCases = _repositories.Items.ComputerComponents.ComputerCases;
            _computerCaseTypesizeService = computerCaseTypesizeService;
        }
        
        public void Create(ComputerCaseWrapper item) {
            
            var created = new ComputerCase();
            
            _setEntityPropertiesFromWrapper(created, item);
            
            created.Length = item.Length;
            created.Width = item.Width;
            created.Height = item.Height;
            created.HasARGBLighting = item.HasARGBLighting;
            created.DrivesSlotsCount = item.DrivesSlotsCount;
            created.Typesize = _computerCaseTypesizeService.GetEntityFromString(item.Typesize);
            
            _computerCases.Create(created);
            
        }

        public ComputerCaseWrapper Get(int id) {
            ComputerCase obj =  _computerCases.Get(id);
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

        public void Update(int id, ComputerCaseWrapper item) {
            ComputerCase obj = _computerCases.Get(id);
            
            _setEntityPropertiesFromWrapper(obj, item);
            
            obj.Length = item.Length;
            obj.Width = item.Width;
            obj.Height = item.Height;
            obj.HasARGBLighting = item.HasARGBLighting;
            obj.DrivesSlotsCount = item.DrivesSlotsCount;
            obj.Typesize = _computerCaseTypesizeService.GetEntityFromString(item.Typesize);
            
            _computerCases.Update(obj);
        }

        public void Delete(int id) {
            _repositories.Items.ComputerComponents.ComputerCases.Delete(id);
        }

        public List<ComputerCaseWrapper> Filter(List<Func<ComputerCaseWrapper, bool>> filters) {
            var result = List();
            foreach (var filter in filters) {
                result = result.Where(filter).ToList();
            }
            return result;
        }

        public int Save() {
            return _repositories.Save();
        }
    }
}