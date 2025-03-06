using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.Characteristics {
    public class RAMTypesService: IRAMTypeService {
        private readonly IGenosStorExpressRepositories _repositories;

        public RAMTypesService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }

        public void Create(string item) {
            var created = new RAMType { Name = item };
            _repositories.Items.Characteristics.RAMTypes.Create(created);
        }

        public string Get(int id) {
            return _repositories.Items.Characteristics.RAMTypes.Get(id).Name;
        }

        public List<string> List() {
            return _repositories.Items.Characteristics.RAMTypes.List().Select(c => c.Name).ToList();
        }

        public void Update(int id, string item) {
            RAMType obj = _repositories.Items.Characteristics.RAMTypes.Get(id);
            obj.Name = item;
            _repositories.Items.Characteristics.RAMTypes.Update(obj);
        }

        public void Delete(int id) {
            _repositories.Items.Characteristics.RAMTypes.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        public bool BelongsToEnum(string value) {
            return _repositories.Items.Characteristics.RAMTypes.List().Exists(c => c.Name == value);
        }
    }
}