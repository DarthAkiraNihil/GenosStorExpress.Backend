using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.ComputerComponents {
    public class DisplayService: IDisplayService {
        private IGenosStorExpressRepositories _repositories;

        public DisplayService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }

        public void Create(Display item) {
            _repositories.Items.ComputerComponents.Displays.Create(item);
        }

        public Display Get(int id) {
            return _repositories.Items.ComputerComponents.Displays.Get(id);
        }

        public List<Display> List() {
            return _repositories.Items.ComputerComponents.Displays.List();
        }

        public void Update(Display item) {
            _repositories.Items.ComputerComponents.Displays.Update(item);
        }

        public void Delete(int id) {
            _repositories.Items.ComputerComponents.Displays.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }
        
        public List<Display> Filter(List<Func<Display, bool>> filters) {
            var result = List();
            foreach (var filter in filters) {
                result = result.Where(filter).ToList();
            }
            return result;
        }
    }
}