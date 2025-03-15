using GenosStorExpress.Domain.Entity.Item.SimpleComputerComponent;
using GenosStorExpress.Domain.Interface.Item.SimpleComputerComponent;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Item.SimpleComputerComponent {
    public class SimpleComputerComponentTypeRepository: ISimpleComputerComponentTypeRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public SimpleComputerComponentTypeRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<SimpleComputerComponentType> List() {
            return _context.SimpleComputerComponentTypes.ToList();
        }

        public SimpleComputerComponentType? Get(int id) {
            return _context.SimpleComputerComponentTypes.Find(id);
        }

        public void Create(SimpleComputerComponentType simpleComputerComponentType) {
            _context.SimpleComputerComponentTypes.Add(simpleComputerComponentType);
        }

        public void Update(SimpleComputerComponentType simpleComputerComponentType) {
            _context.Entry(simpleComputerComponentType).State = EntityState.Modified;
        }

        public void Delete(int id) {
            SimpleComputerComponentType? simpleComputerComponentType = _context.SimpleComputerComponentTypes.Find(id);
            if (simpleComputerComponentType != null) {
                _context.SimpleComputerComponentTypes.Remove(simpleComputerComponentType);
            }
        }
        
    }
}