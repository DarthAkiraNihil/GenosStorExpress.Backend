using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.Characteristics {
    public class CPUSocketsService: ICPUSocketService {
        private readonly IGenosStorExpressRepositories _repositories;

        public CPUSocketsService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }


        public void Create(string item) {
            var created = new CPUSocket { Name = item };
            _repositories.Items.Characteristics.CPUSockets.Create(created);
        }

        public string Get(int id) {
            return _repositories.Items.Characteristics.CPUSockets.Get(id).Name;
        }

        public List<string> List() {
            return _repositories.Items.Characteristics.CPUSockets.List().Select(c => c.Name).ToList();
        }

        public void Update(int id, string item) {
            CPUSocket obj = _repositories.Items.Characteristics.CPUSockets.Get(id);
            obj.Name = item;
            _repositories.Items.Characteristics.CPUSockets.Update(obj);
        }

        public void Delete(int id) {
            _repositories.Items.Characteristics.CPUSockets.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        public bool BelongsToEnum(string value) {
            return _repositories.Items.Characteristics.CPUSockets.List().Exists(c => c.Name == value);
        }
    }
}