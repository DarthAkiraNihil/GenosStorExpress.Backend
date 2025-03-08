using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item;
using GenosStorExpress.Domain.Interface.Item.Characteristic;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.Characteristics {
    public class RAMTypesService: IRAMTypeService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly IRAMTypeRepository _ramTypes;

        public RAMTypesService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
            _ramTypes = _repositories.Items.Characteristics.RAMTypes;
        }

        public void Create(string item) {
            var created = new RAMType { Name = item };
            _ramTypes.Create(created);
        }

        public string Get(int id) {
            return _ramTypes.Get(id).Name;
        }

        public List<string> List() {
            return _ramTypes.List().Select(c => c.Name).ToList();
        }

        public void Update(int id, string item) {
            RAMType obj = _ramTypes.Get(id);
            obj.Name = item;
            _ramTypes.Update(obj);
        }

        public void Delete(int id) {
            _ramTypes.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        public bool BelongsToEnum(string value) {
            return _ramTypes.List().Exists(c => c.Name == value);
        }

        public RAMType GetEntityFromString(string value) {
            return _ramTypes.List().FirstOrDefault(c => c.Name == value, null);
        }
    }
}