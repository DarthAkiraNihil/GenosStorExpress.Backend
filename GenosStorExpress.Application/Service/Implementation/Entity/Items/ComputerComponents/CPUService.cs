using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.ComputerComponents {
    public class CPUService: ICPUService {
        private IGenosStorExpressRepositories _repositories;

        public CPUService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }

        public void Create(CPU item) {
            _repositories.Items.ComputerComponents.CPUs.Create(item);
        }

        public CPU Get(int id) {
            return _repositories.Items.ComputerComponents.CPUs.Get(id);
        }

        public List<CPU> List() {
            return _repositories.Items.ComputerComponents.CPUs.List();
        }

        public void Update(CPU item) {
            _repositories.Items.ComputerComponents.CPUs.Update(item);
        }

        public void Delete(int id) {
            _repositories.Items.ComputerComponents.CPUs.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }
        
        public List<CPU> Filter(List<Func<CPU, bool>> filters) {
            var result = List();
            foreach (var filter in filters) {
                result = result.Where(filter).ToList();
            }
            return result;
        }
    }
}