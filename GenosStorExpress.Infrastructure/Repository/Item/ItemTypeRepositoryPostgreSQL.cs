using GenosStorExpress.Domain.Interface.Item;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Item {
    public class ItemTypeRepository: IItemTypeRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public ItemTypeRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<Domain.Entity.Item.ItemType> List() {
            return _context.ItemTypes.ToList();
        }

        public Domain.Entity.Item.ItemType Get(int id) {
            return _context.ItemTypes.Find(id);
        }

        public void Create(Domain.Entity.Item.ItemType itemType) {
            _context.ItemTypes.Add(itemType);
        }

        public void Update(Domain.Entity.Item.ItemType itemType) {
            _context.Entry(itemType).State = EntityState.Modified;
        }

        public void Delete(int id) {
	        Domain.Entity.Item.ItemType itemType = _context.ItemTypes.Find(id);
            if (itemType != null)
                _context.ItemTypes.Remove(itemType);
        }
        
    }
}