using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.Characteristic;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.Characteristics {
    public class KeyboardTypeService: IKeyboardTypeService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly IKeyboardTypeRepository _keyboardTypes;

        public KeyboardTypeService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
            _keyboardTypes = _repositories.Items.Characteristics.KeyboardTypes;
        }

        public void Create(string item) {
            var created = new KeyboardType { Name = item };
            _keyboardTypes.Create(created);
        }

        public string Get(int id) {
            return _keyboardTypes.Get(id).Name;
        }

        public List<string> List() {
            return _keyboardTypes.List().Select(c => c.Name).ToList();
        }

        public void Update(int id, string item) {
            KeyboardType obj = _keyboardTypes.Get(id);
            obj.Name = item;
            _keyboardTypes.Update(obj);
        }

        public void Delete(int id) {
            _keyboardTypes.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        public bool BelongsToEnum(string value) {
            return _keyboardTypes.List().Exists(c => c.Name == value);
        }

        public KeyboardType GetEntityFromString(string value) {
            return _keyboardTypes.List().FirstOrDefault(c => c.Name == value, null);
        }
    }
}