using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface.Item.Characteristic;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Item.Characteristic {
    public class UnderlightRepository: IUnderlightRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public UnderlightRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<Underlight> List() {
            return _context.Underlights.ToList();
        }

        public Underlight Get(int id) {
            return _context.Underlights.Find(id);
        }

        public void Create(Underlight underlight) {
            _context.Underlights.Add(underlight);
        }

        public void Update(Underlight underlight) {
            _context.Entry(underlight).State = EntityState.Modified;
        }

        public void Delete(int id) {
            Underlight underlight = _context.Underlights.Find(id);
            if (underlight != null)
                _context.Underlights.Remove(underlight);
        }
        
    }
}