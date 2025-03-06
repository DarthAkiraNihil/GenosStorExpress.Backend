using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface.Item.Characteristic;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Item.Characteristic {
    public class DefinitionRepository: IDefinitionRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public DefinitionRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<Definition> List() {
            return _context.Definitions.ToList();
        }

        public Definition Get(int id) {
            return _context.Definitions.Find(id);
        }

        public void Create(Definition definition) {
            _context.Definitions.Add(definition);
        }

        public void Update(Definition definition) {
            _context.Entry(definition).State = EntityState.Modified;
        }

        public void Delete(int id) {
            Definition definition = _context.Definitions.Find(id);
            if (definition != null)
                _context.Definitions.Remove(definition);
        }
        
    }
}