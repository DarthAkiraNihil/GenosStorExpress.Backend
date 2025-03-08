using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Application.Service.Interface.Entity.Items.SimpleComputerComponents;
using GenosStorExpress.Application.Wrappers.Entity.Item.SimpleComputerComponent;
using GenosStorExpress.Domain.Entity.Item.SimpleComputerComponent;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface.Item.SimpleComputerComponent;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.SimpleComputerComponents {
    public class CPUCoresService: ICPUCoreService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly ICPUCoreRepository _cpuCores;
        private readonly IVendorService _vendorService;

        public CPUCoresService(IGenosStorExpressRepositories repositories, IVendorService vendorService) {
            _repositories = repositories;
            _vendorService = vendorService;
            _cpuCores = _repositories.Items.SimpleComputerComponents.CPUCores;
        }

        public void Create(CPUCoreWrapper item) {
            var created = new CPUCore {
                Name = item.Name,
                Vendor = _vendorService.GetEntityFromString(item.Vendor),
            };
            _cpuCores.Create(created);
        }

        public CPUCoreWrapper Get(int id) {
            CPUCore obj = _cpuCores.Get(id);
            return new CPUCoreWrapper {
                Id = obj.Id,
                Name = obj.Name,
                Vendor = obj.Vendor.Name
            };
        }

        public List<CPUCoreWrapper> List() {
            return _cpuCores.List().Select(obj => new CPUCoreWrapper {
                    Id = obj.Id,
                    Name = obj.Name,
                    Vendor = obj.Vendor.Name
                }
            ).ToList();
        }

        public void Update(int id, CPUCoreWrapper item) {
            var obj = _cpuCores.Get(id);
            obj.Name = item.Name;
            obj.Vendor = _vendorService.GetEntityFromString(item.Vendor);
            _cpuCores.Update(obj);
        }

        public void Delete(int id) {
            _repositories.Items.SimpleComputerComponents.CPUCores.Delete(id);
        }

        public CPUCore GetRaw(int id) {
            throw new NotImplementedException();
        }

        public int Save() {
            return _repositories.Save();
        }
    }
}