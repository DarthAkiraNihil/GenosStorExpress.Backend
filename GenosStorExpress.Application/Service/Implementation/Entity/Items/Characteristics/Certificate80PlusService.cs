using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.Characteristic;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.Characteristics {
    
    public class Certificate80PlusService: ICertificate80PlusService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly ICertificate80PlusRepository _certificates;

        public Certificate80PlusService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
            _certificates = _repositories.Items.Characteristics.Certificates80Plus;
        }

        public void Create(string item) {
            var created = new Certificate80Plus { Name = item };
            _certificates.Create(created);
        }

        public string? Get(int id) {
            return _certificates.Get(id)?.Name;
        }

        public List<string> List() {
            return _certificates.List().Select(c => c.Name).ToList();
        }

        public void Update(int id, string item) {
            Certificate80Plus obj = _certificates.Get(id)!;
            obj.Name = item;
            _certificates.Update(obj);
        }

        public void Delete(int id) {
            _certificates.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        public bool BelongsToEnum(string value) {
            return _certificates.List().Exists(c => c.Name == value);
        }

        public Certificate80Plus? GetEntityFromString(string value) {
            return _certificates.List().FirstOrDefault(c => c.Name == value);
        }
    }
}