using GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;
using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.ComputerComponents {
    public class ComputerCaseService: IComputerCaseService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly IMotherboardFormFactorService _motherboardFormFactorService;
        private readonly IComputerCaseTypesizeService _caseTypesizeService;
        private readonly IItemTypeService _itemTypeService;
        

        public ComputerCaseService(
            IGenosStorExpressRepositories repositories,
            IMotherboardFormFactorService motherboardFormFactorService,
            IComputerCaseTypesizeService computerCaseTypesizeService,
            IItemTypeService itemTypesService
        ) {
            _repositories = repositories;
            _motherboardFormFactorService = motherboardFormFactorService;
            _caseTypesizeService = computerCaseTypesizeService;
            _itemTypeService = itemTypesService;
        }

        public void Create(ComputerCaseWrapper item) {
            
            List<MotherboardFormFactor> formFactors = _repositories.Items.Characteristics.MotherboardFormFactors.List();
            Vendor vendor = _repositories.Items.Characteristics.Vendors.List().FirstOrDefault(x => x.Name == item.Vendor);
            
            _repositories.Items.ComputerComponents.ComputerCases.Create(new ComputerCase {
                Name = item.Name,
                Model = item.Model,
                ImageBase64 = "",
                Price = item.Price,
                Description = item.Description,
                ActiveDiscount = null,
                ItemType = _repositories.Items.ItemTypes.List().FirstOrDefault(x => x.Name == item.ItemType),
                TDP = item.TDP,
                Vendor = vendor,
                Length = item.Length,
                Width = item.Width,
                Height = item.Height,
                HasARGBLighting = item.HasARGBLighting,
                DrivesSlotsCount = item.DrivesSlotsCount,
                Typesize = _repositories.Items.Characteristics.ComputerCaseTypesizes.List().FirstOrDefault(x => x.Name == item.Typesize),
            });
        }

        public ComputerCaseWrapper Get(int id) {
            ComputerCase obj =  _repositories.Items.ComputerComponents.ComputerCases.Get(id);
            return new ComputerCaseWrapper {
                Id = obj.Id,
                ActiveDiscount = null,
                Description = obj.Description,
                DrivesSlotsCount = obj.DrivesSlotsCount,
                HasARGBLighting = obj.HasARGBLighting,
                Height = obj.Height,
                ItemType = obj.ItemType.Name,
                Length = obj.Length,
                Model = obj.Model,
                Name = obj.Name,
                TDP = obj.TDP,
                Price = obj.Price,
            };
        }

        public List<ComputerCaseWrapper> List() {
            var list = _repositories.Items.ComputerComponents.ComputerCases.List();
            return _repositories.Items.ComputerComponents.ComputerCases.List().Select(
                obj => new ComputerCaseWrapper {
                Id = obj.Id,
                ActiveDiscount = null,
                Description = obj.Description,
                DrivesSlotsCount = obj.DrivesSlotsCount,
                HasARGBLighting = obj.HasARGBLighting,
                Height = obj.Height,
                ItemType = obj.ItemType.Name,
                Length = obj.Length,
                Model = obj.Model,
                Name = obj.Name,
                TDP = obj.TDP,
                Price = obj.Price,
                Vendor = obj.Vendor.Name,
                Typesize = obj.Typesize.Name
            }).ToList();
        }

        public void Update(int id, ComputerCaseWrapper item) {
            ComputerCase obj = _repositories.Items.ComputerComponents.ComputerCases.Get(id);
            obj.Description = item.Description;
            obj.DrivesSlotsCount = item.DrivesSlotsCount;
            obj.HasARGBLighting = item.HasARGBLighting;
            obj.Height = item.Height;
            obj.Name = item.Name;
            obj.Price = item.Price;
            obj.Length = item.Length;
            obj.Width = item.Width;
            obj.Model = item.Model;
            obj.TDP = item.TDP;
            obj.Price = item.Price;
            _repositories.Items.ComputerComponents.ComputerCases.Update(obj);
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