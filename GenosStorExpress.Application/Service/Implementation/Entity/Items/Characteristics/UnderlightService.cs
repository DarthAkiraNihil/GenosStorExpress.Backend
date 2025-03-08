using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.Characteristic;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.Characteristics {
    public class UnderlightService: IUnderlightService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly IUnderlightRepository _underlights;

        public UnderlightService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
            _underlights = _repositories.Items.Characteristics.Underlights;
        }

        public void Create(string item) {
            var created = new Underlight { Name = item };
            _underlights.Create(created);
        }

        public string Get(int id) {
            return _underlights.Get(id).Name;
        }

        public List<string> List() {
            return _underlights.List().Select(c => c.Name).ToList();
        }

        public void Update(int id, string item) {
            Underlight obj = _underlights.Get(id);
            obj.Name = item;
            _underlights.Update(obj);
        }

        public void Delete(int id) {
            _underlights.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        public bool BelongsToEnum(string value) {
            return _underlights.List().Exists(c => c.Name == value);
        }

        public Underlight GetEntityFromString(string value) {
            return _underlights.List().FirstOrDefault(c => c.Name == value, null);
        }
    }
}