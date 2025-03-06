using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.ComputerComponents {
    public class SataSSDService: ISataSSDService {
        private IGenosStorExpressRepositories _repositories;

        public void Create(SataSSD item) {
            _repositories.Items.ComputerComponents.SataSSDs.Create(item);
        }

        public SataSSD Get(int id) {
            return _repositories.Items.ComputerComponents.SataSSDs.Get(id);
        }

        public List<SataSSD> List() {
            return _repositories.Items.ComputerComponents.SataSSDs.List();
        }

        public void Update(SataSSD item) {
            _repositories.Items.ComputerComponents.SataSSDs.Update(item);
        }

        public void Delete(int id) {
            _repositories.Items.ComputerComponents.SataSSDs.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }
        
        public List<SataSSD> Filter(List<Func<SataSSD, bool>> filters) {
            var result = List();
            foreach (var filter in filters) {
                result = result.Where(filter).ToList();
            }
            return result;
        }

        public SataSSDService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }
    }
}