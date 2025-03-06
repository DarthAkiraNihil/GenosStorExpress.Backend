using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.ComputerComponents {
    public class KeyboardService: IKeyboardService {
        private IGenosStorExpressRepositories _repositories;

        public KeyboardService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }

        public void Create(Keyboard item) {
            _repositories.Items.ComputerComponents.Keyboards.Create(item);
        }

        public Keyboard Get(int id) {
            return _repositories.Items.ComputerComponents.Keyboards.Get(id);
        }

        public List<Keyboard> List() {
            return _repositories.Items.ComputerComponents.Keyboards.List();
        }

        public void Update(Keyboard item) {
            _repositories.Items.ComputerComponents.Keyboards.Update(item);
        }

        public void Delete(int id) {
            _repositories.Items.ComputerComponents.Keyboards.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }
        
        public List<Keyboard> Filter(List<Func<Keyboard, bool>> filters) {
            var result = List();
            foreach (var filter in filters) {
                result = result.Where(filter).ToList();
            }
            return result;
        }
    }
}