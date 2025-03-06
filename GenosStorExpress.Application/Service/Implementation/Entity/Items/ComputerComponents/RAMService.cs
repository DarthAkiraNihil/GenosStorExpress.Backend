using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.ComputerComponents {
    public class RAMService: IRAMService {
        private IGenosStorExpressRepositories _repositories;

        public RAMService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }

        public void Create(RAM item) {
            _repositories.Items.ComputerComponents.RAMs.Create(item);
        }

        public RAM Get(int id) {
            return _repositories.Items.ComputerComponents.RAMs.Get(id);
        }

        public List<RAM> List() {
            return _repositories.Items.ComputerComponents.RAMs.List();
        }

        public void Update(RAM item) {
            _repositories.Items.ComputerComponents.RAMs.Update(item);
        }

        public void Delete(int id) {
            _repositories.Items.ComputerComponents.RAMs.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }
        
        public List<RAM> Filter(List<Func<RAM, bool>> filters) {
            var result = List();
            foreach (var filter in filters) {
                result = result.Where(filter).ToList();
            }
            return result;
        }
    }
}