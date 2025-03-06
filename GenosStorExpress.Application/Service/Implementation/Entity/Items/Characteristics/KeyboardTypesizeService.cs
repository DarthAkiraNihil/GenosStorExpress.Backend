using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.Characteristics {
    public class KeyboardTypesizeService: IKeyboardTypesizeService {

        private readonly IGenosStorExpressRepositories _repositories;

        public KeyboardTypesizeService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }

        public void Create(string item) {
            var created = new KeyboardTypesize { Name = item };
            _repositories.Items.Characteristics.KeyboardTypesizes.Create(created);
        }

        public string Get(int id) {
            return _repositories.Items.Characteristics.KeyboardTypesizes.Get(id).Name;
        }

        public List<string> List() {
            return _repositories.Items.Characteristics.KeyboardTypesizes.List().Select(c => c.Name).ToList();
        }

        public void Update(int id, string item) {
            KeyboardTypesize obj = _repositories.Items.Characteristics.KeyboardTypesizes.Get(id);
            obj.Name = item;
            _repositories.Items.Characteristics.KeyboardTypesizes.Update(obj);
        }

        public void Delete(int id) {
            _repositories.Items.Characteristics.KeyboardTypesizes.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        public bool BelongsToEnum(string value) {
            return _repositories.Items.Characteristics.KeyboardTypesizes.List().Exists(c => c.Name == value);
        }
        
    }
}