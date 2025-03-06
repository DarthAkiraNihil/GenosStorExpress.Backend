using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Domain.Entity.Item;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items {
    public class ItemTypesService: IItemTypeService {
        
        private readonly IGenosStorExpressRepositories _repositories;

        public ItemTypesService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }

        public void Create(string item) {
            var created = new ItemType { Name = item };
            _repositories.Items.ItemTypes.Create(created);
        }

        public string Get(int id) {
            return _repositories.Items.ItemTypes.Get(id).Name;
        }

        public List<string> List() {
            return _repositories.Items.ItemTypes.List().Select(c => c.Name).ToList();
        }

        public void Update(int id, string item) {
            ItemType obj = _repositories.Items.ItemTypes.Get(id);
            obj.Name = item;
            _repositories.Items.ItemTypes.Update(obj);
        }

        public void Delete(int id) {
            _repositories.Items.ItemTypes.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        public bool BelongsToEnum(string value) {
            return _repositories.Items.ItemTypes.List().Exists(c => c.Name == value);
        }
    }
}