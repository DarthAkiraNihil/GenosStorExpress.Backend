using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Domain.Entity.Item;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items {
    public class PreparedAssemblyService: IPreparedAssemblyService {
        private IGenosStorExpressRepositories _repositories;

        public PreparedAssemblyService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }

        public void Create(PreparedAssembly item) {
            _repositories.Items.PreparedAssemblies.Create(item);
        }

        public PreparedAssembly Get(int id) {
            return _repositories.Items.PreparedAssemblies.Get(id);
        }

        public List<PreparedAssembly> List() {
            return _repositories.Items.PreparedAssemblies.List();
        }

        public void Update(PreparedAssembly item) {
            _repositories.Items.PreparedAssemblies.Update(item);
        }

        public void Delete(int id) {
            _repositories.Items.PreparedAssemblies.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }
    }
}