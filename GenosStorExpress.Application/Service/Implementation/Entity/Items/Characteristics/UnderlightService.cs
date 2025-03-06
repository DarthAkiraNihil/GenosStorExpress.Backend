using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.Characteristics {
    public class UnderlightService: IUnderlightService {
        private readonly IGenosStorExpressRepositories _repositories;

        public UnderlightService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }

        public void Create(string item) {
            var created = new Underlight { Name = item };
            _repositories.Items.Characteristics.Underlights.Create(created);
        }

        public string Get(int id) {
            return _repositories.Items.Characteristics.Underlights.Get(id).Name;
        }

        public List<string> List() {
            return _repositories.Items.Characteristics.Underlights.List().Select(c => c.Name).ToList();
        }

        public void Update(int id, string item) {
            Underlight obj = _repositories.Items.Characteristics.Underlights.Get(id);
            obj.Name = item;
            _repositories.Items.Characteristics.Underlights.Update(obj);
        }

        public void Delete(int id) {
            _repositories.Items.Characteristics.Underlights.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        public bool BelongsToEnum(string value) {
            return _repositories.Items.Characteristics.Underlights.List().Exists(c => c.Name == value);
        }
    }
}