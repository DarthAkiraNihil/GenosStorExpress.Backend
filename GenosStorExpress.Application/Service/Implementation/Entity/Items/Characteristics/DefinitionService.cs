using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.Characteristics {
    public class DefinitionService: IDefinitionService {
        private IGenosStorExpressRepositories _repositories;

        public void Create(string item) {
            var created = new Definition { Name = item };
            _repositories.Items.Characteristics.Definitions.Create(created);
        }

        public string Get(int id) {
            return _repositories.Items.Characteristics.Definitions.Get(id).Name;
        }

        public List<string> List() {
            return _repositories.Items.Characteristics.Definitions.List().Select(c => c.Name).ToList();
        }

        public void Update(int id, string item) {
            Definition obj = _repositories.Items.Characteristics.Definitions.Get(id);
            obj.Name = item;
            _repositories.Items.Characteristics.Definitions.Update(obj);
        }

        public void Delete(int id) {
            _repositories.Items.Characteristics.Definitions.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        public bool BelongsToEnum(string value) {
            return _repositories.Items.Characteristics.Definitions.List().Exists(c => c.Name == value);
        }
    }
}