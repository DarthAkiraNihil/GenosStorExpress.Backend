using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Domain.Entity.Item;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items {
    public class AllItemsService: IAllItemsService {
        private IGenosStorExpressRepositories _repositories;

        public AllItemsService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }

        public void Create(Item item) {
            _repositories.Items.All.Create(item);
        }

        public Item Get(int id) {
            return _repositories.Items.All.Get(id);
        }

        public List<Item> List() {
            return _repositories.Items.All.List();
        }

        public void Update(Item item) {
            _repositories.Items.All.Update(item);
        }

        public void Delete(int id) {
            _repositories.Items.All.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }
    }
}