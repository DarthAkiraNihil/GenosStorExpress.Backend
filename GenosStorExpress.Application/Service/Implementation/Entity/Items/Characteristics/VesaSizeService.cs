using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.Characteristics {
    public class VesaSizeService: IVesaSizeService {
        private readonly IGenosStorExpressRepositories _repositories;

        public VesaSizeService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }

        public void Create(string item) {
            var created = new VesaSize { Name = item };
            _repositories.Items.Characteristics.VesaSizes.Create(created);
        }

        public string Get(int id) {
            return _repositories.Items.Characteristics.VesaSizes.Get(id).Name;
        }

        public List<string> List() {
            return _repositories.Items.Characteristics.VesaSizes.List().Select(c => c.Name).ToList();
        }

        public void Update(int id, string item) {
            VesaSize obj = _repositories.Items.Characteristics.VesaSizes.Get(id);
            obj.Name = item;
            _repositories.Items.Characteristics.VesaSizes.Update(obj);
        }

        public void Delete(int id) {
            _repositories.Items.Characteristics.VesaSizes.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        public bool BelongsToEnum(string value) {
            return _repositories.Items.Characteristics.VesaSizes.List().Exists(c => c.Name == value);
        }
    }
}