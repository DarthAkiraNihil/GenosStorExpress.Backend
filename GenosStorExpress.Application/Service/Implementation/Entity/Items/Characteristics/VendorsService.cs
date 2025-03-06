using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.Characteristics {
    public class VendorsService: IVendorService {

        private readonly IGenosStorExpressRepositories _repositories;

        public VendorsService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }

        public void Create(string item) {
            var created = new Vendor { Name = item };
            _repositories.Items.Characteristics.Vendors.Create(created);
        }

        public string Get(int id) {
            return _repositories.Items.Characteristics.Vendors.Get(id).Name;
        }

        public List<string> List() {
            return _repositories.Items.Characteristics.Vendors.List().Select(c => c.Name).ToList();
        }

        public void Update(int id, string item) {
            Vendor obj = _repositories.Items.Characteristics.Vendors.Get(id);
            obj.Name = item;
            _repositories.Items.Characteristics.Vendors.Update(obj);
        }

        public void Delete(int id) {
            _repositories.Items.Characteristics.Vendors.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        public bool BelongsToEnum(string value) {
            return _repositories.Items.Characteristics.Vendors.List().Exists(c => c.Name == value);
        }
    }
}