using GenosStorExpress.Domain.Interface.Item;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Item {
    public class AllItemsRepository: IAllItemsRepository {
        
        private readonly GenosStorExpressDatabaseContext _context;
        
        public AllItemsRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<Domain.Entity.Item.Item> List() {
            return _context.Items
                           .Include(i => i.Reviews)
                           .ToList();
        }

        public Domain.Entity.Item.Item? Get(int id) {
            return _context.Items
                           .Include(i => i.Reviews)
                           .FirstOrDefault(i => i.Id == id);
        }

        public void Create(Domain.Entity.Item.Item itemType) {
            _context.Items.Add(itemType);
        }

        public void Update(Domain.Entity.Item.Item item) {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id) {
	        Domain.Entity.Item.Item? item = _context.Items.Find(id);
            if (item != null) {
                _context.Items.Remove(item);
            }
        }
        
    }
}