using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.Characteristic;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.Characteristics {
    public class CoolerMaterialService: ICoolerMaterialService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly ICoolerMaterialRepository _coolerMaterials;

        public CoolerMaterialService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
            _coolerMaterials = _repositories.Items.Characteristics.CoolerMaterials;
        }


        public void Create(string item) {
            var created = new CoolerMaterial { Name = item };
            _coolerMaterials.Create(created);
        }

        public string Get(int id) {
            return _coolerMaterials.Get(id).Name;
        }

        public List<string> List() {
            return _coolerMaterials.List().Select(c => c.Name).ToList();
        }

        public void Update(int id, string item) {
            CoolerMaterial obj = _coolerMaterials.Get(id);
            obj.Name = item;
            _coolerMaterials.Update(obj);
        }

        public void Delete(int id) {
            _coolerMaterials.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        public bool BelongsToEnum(string value) {
            return _coolerMaterials.List().Exists(c => c.Name == value);
        }

        public CoolerMaterial GetEntityFromString(string value) {
            return _coolerMaterials.List().FirstOrDefault(c => c.Name == value, null);
        }
    }
}