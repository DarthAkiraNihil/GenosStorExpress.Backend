using GenosStorExpress.Application.Service.Interface.Base;
using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.Characteristics {
    
    public class Certificate80PlusService: ICertificate80PlusService {
        private readonly IGenosStorExpressRepositories _repositories;

        public Certificate80PlusService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }

        public void Create(string item) {
            var created = new Certificate80Plus { Name = item };
            _repositories.Items.Characteristics.Certificates80Plus.Create(created);
        }

        public string Get(int id) {
            return _repositories
                   .Items
                   .Characteristics
                   .Certificates80Plus.Get(id).Name;
        }

        public List<string> List() {
            return _repositories
                   .Items
                   .Characteristics
                   .Certificates80Plus
                   .List().Select(c => c.Name).ToList();
        }

        public void Update(int id, string item) {
            Certificate80Plus obj = _repositories.Items.Characteristics.Certificates80Plus.Get(id);
            obj.Name = item;
            _repositories.Items.Characteristics.Certificates80Plus.Update(obj);
        }

        public void Delete(int id) {
            _repositories.Items.Characteristics.Certificates80Plus.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        public bool BelongsToEnum(string value) {
            return _repositories.Items.Characteristics.Certificates80Plus.List().Exists(c => c.Name == value);
        }
    }
}