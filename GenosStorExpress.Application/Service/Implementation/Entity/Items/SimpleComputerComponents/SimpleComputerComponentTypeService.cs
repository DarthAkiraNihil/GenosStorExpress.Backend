using GenosStorExpress.Application.Service.Interface.Entity.Items.SimpleComputerComponents;
using GenosStorExpress.Domain.Entity.Item.SimpleComputerComponent;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.SimpleComputerComponent;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.SimpleComputerComponents {
    public class SimpleComputerComponentTypeService: ISimpleComputerComponentTypeService {

        private readonly IGenosStorExpressRepositories _repositories;
        private readonly ISimpleComputerComponentTypeRepository _simpleComputerComponentTypes;

        public SimpleComputerComponentTypeService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
            _simpleComputerComponentTypes = _repositories.Items.SimpleComputerComponents.SimpleComputerComponentTypes;
        }

        public void Create(string item) {
            var created = new SimpleComputerComponentType { Name = item };
            _simpleComputerComponentTypes.Create(created);
        }

        public string Get(int id) {
            return _simpleComputerComponentTypes.Get(id).Name;
        }

        public List<string> List() {
            return _simpleComputerComponentTypes.List().Select(c => c.Name).ToList();
        }

        public void Update(int id, string item) {
            SimpleComputerComponentType obj = _simpleComputerComponentTypes.Get(id);
            obj.Name = item;
            _simpleComputerComponentTypes.Update(obj);
        }

        public void Delete(int id) {
            _simpleComputerComponentTypes.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        public bool BelongsToEnum(string value) {
            return _simpleComputerComponentTypes.List().Exists(c => c.Name == value);
        }

        public SimpleComputerComponentType GetEntityFromString(string value) {
            return _simpleComputerComponentTypes.List().FirstOrDefault(c => c.Name == value, null);
        }
    }
}