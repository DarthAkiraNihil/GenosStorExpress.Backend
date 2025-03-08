using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.Characteristic;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.Characteristics {
    public class CPUSocketsService: ICPUSocketService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly ICPUSocketRepository _cpuSockets;

        public CPUSocketsService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
            _cpuSockets = _repositories.Items.Characteristics.CPUSockets;
        }


        public void Create(string item) {
            var created = new CPUSocket { Name = item };
            _cpuSockets.Create(created);
        }

        public string Get(int id) {
            return _cpuSockets.Get(id).Name;
        }

        public List<string> List() {
            return _cpuSockets.List().Select(c => c.Name).ToList();
        }

        public void Update(int id, string item) {
            CPUSocket obj = _cpuSockets.Get(id);
            obj.Name = item;
            _cpuSockets.Update(obj);
        }

        public void Delete(int id) {
            _cpuSockets.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        public bool BelongsToEnum(string value) {
            return _cpuSockets.List().Exists(c => c.Name == value);
        }

        public CPUSocket GetEntityFromString(string value) {
            return _cpuSockets.List().FirstOrDefault(c => c.Name == value, null);
        }
    }
}