using GenosStorExpress.Application.Service.Interface.Entity.Items.SimpleComputerComponents;
using GenosStorExpress.Domain.Entity.Item.SimpleComputerComponent;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.SimpleComputerComponents {
    public class SimpleComputerComponentTypeService: ISimpleComputerComponentTypeService {
        private IGenosStorExpressRepositories _repositories;

        public SimpleComputerComponentTypeService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }

        public void Create(SimpleComputerComponentType item) {
            _repositories.Items.SimpleComputerComponents.SimpleComputerComponentTypes.Create(item);
        }

        public SimpleComputerComponentType Get(int id) {
            return _repositories.Items.SimpleComputerComponents.SimpleComputerComponentTypes.Get(id);
        }

        public List<SimpleComputerComponentType> List() {
            return _repositories.Items.SimpleComputerComponents.SimpleComputerComponentTypes.List();
        }

        public void Update(SimpleComputerComponentType item) {
            _repositories.Items.SimpleComputerComponents.SimpleComputerComponentTypes.Update(item);
        }

        public void Delete(int id) {
            _repositories.Items.SimpleComputerComponents.SimpleComputerComponentTypes.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        public SimpleComputerComponentType GetFromString(string value) {
            return _repositories.Items.SimpleComputerComponents.SimpleComputerComponentTypes.List().FirstOrDefault(i => i.Name == value);
        }
    }
}