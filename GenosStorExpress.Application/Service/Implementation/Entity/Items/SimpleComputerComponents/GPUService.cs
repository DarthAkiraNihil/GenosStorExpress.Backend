using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Application.Service.Interface.Entity.Items.SimpleComputerComponents;
using GenosStorExpress.Application.Wrappers.Entity.Item.SimpleComputerComponent;
using GenosStorExpress.Domain.Entity.Item.SimpleComputerComponent;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.SimpleComputerComponent;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.SimpleComputerComponents {
    public class GPUService: IGPUService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly IGPURepository _gpus;
        private readonly IVendorService _vendorService;

        public GPUService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
            _gpus = _repositories.Items.SimpleComputerComponents.GPUs;
        }

        public void Create(GPUWrapper item) {
            
            _gpus.Create(new GPU {
                Name = item.Name,
                Model = item.Model,
                Vendor = _vendorService.GetEntityFromString(item.Vendor)
            });
            
        }

        public GPUWrapper Get(int id) {
            GPU obj = _gpus.Get(id);
            return new GPUWrapper {
                Id = obj.Id,
                Name = obj.Name,
                Model = obj.Model,
                Vendor = obj.Vendor.Name,
            };
        }

        public List<GPUWrapper> List() {
            return _gpus.List().Select(obj => new GPUWrapper {
                Id = obj.Id,
                Name = obj.Name,
                Model = obj.Model,
                Vendor = obj.Vendor.Name
            }).ToList();
        }

        public void Update(int id, GPUWrapper item) {
            GPU obj = _gpus.Get(id);
            
            obj.Name = item.Name;
            obj.Model = item.Model;
            obj.Vendor = _vendorService.GetEntityFromString(item.Vendor);
            
            _gpus.Update(obj);
        }

        public void Delete(int id) {
            _gpus.Delete(id);
        }

        public GPU GetRaw(int id) {
            return _gpus.Get(id);
        }

        public int Save() {
            return _repositories.Save();
        }
    }
}