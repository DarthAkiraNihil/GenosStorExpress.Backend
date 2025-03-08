using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.Characteristic;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.Characteristics {
    public class VendorsService: IVendorService {

        private readonly IGenosStorExpressRepositories _repositories;
        private readonly IVendorRepository _vendors;

        public VendorsService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
            _vendors = _repositories.Items.Characteristics.Vendors;
        }

        public void Create(string item) {
            var created = new Vendor { Name = item };
            _vendors.Create(created);
        }

        public string Get(int id) {
            return _vendors.Get(id).Name;
        }

        public List<string> List() {
            return _vendors.List().Select(c => c.Name).ToList();
        }

        public void Update(int id, string item) {
            Vendor obj = _vendors.Get(id);
            obj.Name = item;
            _vendors.Update(obj);
        }

        public void Delete(int id) {
            _vendors.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        public bool BelongsToEnum(string value) {
            return _vendors.List().Exists(c => c.Name == value);
        }

        public Vendor GetEntityFromString(string value) {
            return _vendors.List().FirstOrDefault(c => c.Name == value, null);
        }
    }
}