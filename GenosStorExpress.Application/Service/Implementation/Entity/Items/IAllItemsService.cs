using GenosStorExpress.Application.Service.Implementation.Base;
using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Wrappers.Entity.Item;
using GenosStorExpress.Domain.Entity.Item;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items {
    public class AllItemsService: AbstractItemService, IAllItemsService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly IAllItemsRepository _items;

        public AllItemsService(IItemTypeService itemTypeService, IGenosStorExpressRepositories repositories) : base(itemTypeService) {
            _repositories = repositories;
            _items = _repositories.Items.All;
        }
        
        public ItemWrapper? Get(int id) {
            Item? obj =  _items.Get(id);

            if (obj == null) {
                return null;
            }
            
            var wrapped = new ItemWrapper();
            _setWrapperPropertiesFromEntity(obj, wrapped);
            return wrapped;
        }

        public List<ItemWrapper> List() {
            return _items.List().Select(
                obj => {
                    var wrapped = new ItemWrapper();
                    _setWrapperPropertiesFromEntity(obj, wrapped);
                    return wrapped;
                }
            ).ToList();
        }

        public void Delete(int id) {
            _repositories.Items.All.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }
    }
}