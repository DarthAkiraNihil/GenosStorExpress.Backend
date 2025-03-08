using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.Characteristic;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.Characteristics {
    public class VesaSizeService: IVesaSizeService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly IVesaSizeRepository _vesaSizes;

        public VesaSizeService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
            _vesaSizes = _repositories.Items.Characteristics.VesaSizes;
        }

        public void Create(string item) {
            var created = new VesaSize { Name = item };
            _vesaSizes.Create(created);
        }

        public string Get(int id) {
            return _vesaSizes.Get(id).Name;
        }

        public List<string> List() {
            return _vesaSizes.List().Select(c => c.Name).ToList();
        }

        public void Update(int id, string item) {
            VesaSize obj = _vesaSizes.Get(id);
            obj.Name = item;
            _vesaSizes.Update(obj);
        }

        public void Delete(int id) {
            _vesaSizes.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        public bool BelongsToEnum(string value) {
            return _vesaSizes.List().Exists(c => c.Name == value);
        }

        public VesaSize GetEntityFromString(string value) {
            return _vesaSizes.List().FirstOrDefault(c => c.Name == value, null);
        }
    }
}