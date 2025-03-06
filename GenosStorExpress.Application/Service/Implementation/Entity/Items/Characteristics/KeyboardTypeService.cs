using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.Characteristics {
    public class KeyboardTypeService: IKeyboardTypeService {
        private readonly IGenosStorExpressRepositories _repositories;

        public KeyboardTypeService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }

        public void Create(string item) {
            var created = new KeyboardType { Name = item };
            _repositories.Items.Characteristics.KeyboardTypes.Create(created);
        }

        public string Get(int id) {
            return _repositories.Items.Characteristics.KeyboardTypes.Get(id).Name;
        }

        public List<string> List() {
            return _repositories.Items.Characteristics.KeyboardTypes.List().Select(c => c.Name).ToList();
        }

        public void Update(int id, string item) {
            KeyboardType obj = _repositories.Items.Characteristics.KeyboardTypes.Get(id);
            obj.Name = item;
            _repositories.Items.Characteristics.KeyboardTypes.Update(obj);
        }

        public void Delete(int id) {
            _repositories.Items.Characteristics.KeyboardTypes.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        public bool BelongsToEnum(string value) {
            return _repositories.Items.Characteristics.KeyboardTypes.List().Exists(c => c.Name == value);
        }
    }
}