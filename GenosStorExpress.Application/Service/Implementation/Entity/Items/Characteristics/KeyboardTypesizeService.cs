using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.Characteristic;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.Characteristics {
    public class KeyboardTypesizeService: IKeyboardTypesizeService {

        private readonly IGenosStorExpressRepositories _repositories;
        private readonly IKeyboardTypesizeRepository _keyboardTypesizes;

        public KeyboardTypesizeService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
            _keyboardTypesizes = _repositories.Items.Characteristics.KeyboardTypesizes;
        }

        public void Create(string item) {
            var created = new KeyboardTypesize { Name = item };
            _keyboardTypesizes.Create(created);
        }

        public string Get(int id) {
            return _keyboardTypesizes.Get(id).Name;
        }

        public List<string> List() {
            return _keyboardTypesizes.List().Select(c => c.Name).ToList();
        }

        public void Update(int id, string item) {
            KeyboardTypesize obj = _keyboardTypesizes.Get(id);
            obj.Name = item;
            _keyboardTypesizes.Update(obj);
        }

        public void Delete(int id) {
            _keyboardTypesizes.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        public bool BelongsToEnum(string value) {
            return _keyboardTypesizes.List().Exists(c => c.Name == value);
        }

        public KeyboardTypesize GetEntityFromString(string value) {
            return _keyboardTypesizes.List().FirstOrDefault(c => c.Name == value, null);
        }
    }
}