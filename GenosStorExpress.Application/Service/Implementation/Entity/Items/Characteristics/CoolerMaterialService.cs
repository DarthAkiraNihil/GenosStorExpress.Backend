using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.Characteristics {
    public class CoolerMaterialService: ICoolerMaterialService {
        private readonly IGenosStorExpressRepositories _repositories;

        public CoolerMaterialService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }


        public void Create(string item) {
            var created = new CoolerMaterial { Name = item };
            _repositories.Items.Characteristics.CoolerMaterials.Create(created);
        }

        public string Get(int id) {
            return _repositories.Items.Characteristics.CoolerMaterials.Get(id).Name;
        }

        public List<string> List() {
            return _repositories.Items.Characteristics.CoolerMaterials.List().Select(c => c.Name).ToList();
        }

        public void Update(int id, string item) {
            CoolerMaterial obj = _repositories.Items.Characteristics.CoolerMaterials.Get(id);
            obj.Name = item;
            _repositories.Items.Characteristics.CoolerMaterials.Update(obj);
        }

        public void Delete(int id) {
            _repositories.Items.Characteristics.CoolerMaterials.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        public bool BelongsToEnum(string value) {
            return _repositories.Items.Characteristics.CoolerMaterials.List().Exists(c => c.Name == value);
        }
    }
}