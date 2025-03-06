using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.ComputerComponents {
    public class HDDService: IHDDService {
        private IGenosStorExpressRepositories _repositories;

        public HDDService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }

        public void Create(HDD item) {
            _repositories.Items.ComputerComponents.HDDs.Create(item);
        }

        public HDD Get(int id) {
            return _repositories.Items.ComputerComponents.HDDs.Get(id);
        }

        public List<HDD> List() {
            return _repositories.Items.ComputerComponents.HDDs.List();
        }

        public void Update(HDD item) {
            _repositories.Items.ComputerComponents.HDDs.Update(item);
        }

        public void Delete(int id) {
            _repositories.Items.ComputerComponents.HDDs.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }
        
        public List<HDD> Filter(List<Func<HDD, bool>> filters) {
            var result = List();
            foreach (var filter in filters) {
                result = result.Where(filter).ToList();
            }
            return result;
        }
    }
}