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

        public GPUService(IGenosStorExpressRepositories repositories, IGPURepository gpus, IVendorService vendorService) {
            _repositories = repositories;
            _gpus = gpus;
            _vendorService = vendorService;
        }
        
        public void Create(GPUWrapper item) {
            
            var vendor = _vendorService.GetEntityFromString(item.Vendor);
            if (vendor == null) {
                throw new NullReferenceException($"Производителя {item.Vendor} не существует");
            }
            
            _gpus.Create(new GPU {
                Name = item.Name,
                Model = item.Model,
                Vendor = vendor
            });
            
        }

        public GPUWrapper? Get(int id) {
            GPU? obj = _gpus.Get(id);
            if (obj == null) {
                return null;
            }
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
            GPU? obj = _gpus.Get(id);

            if (obj == null) {
                throw new NullReferenceException($"Графического процессора с номером {id} не существует");
            }
            
            obj.Name = item.Name;
            obj.Model = item.Model;
            
            var vendor = _vendorService.GetEntityFromString(item.Vendor);
            if (vendor == null) {
                throw new NullReferenceException($"Производителя {item.Vendor} не существует");
            }

            obj.Vendor = vendor;
            
            _gpus.Update(obj);
        }

        public void Delete(int id) {
            _gpus.Delete(id);
        }

        public GPU? GetRaw(int id) {
            return _gpus.Get(id);
        }

        public int Save() {
            return _repositories.Save();
        }
    }
}