using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Item.ComputerComponent {
    public class HDDRepository: IHDDRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public HDDRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<HDD> List() {
            return _context.HDDs.ToList();
        }

        public HDD Get(int id) {
            return _context.HDDs.Find(id);
        }

        public void Create(HDD hdd) {
            _context.HDDs.Add(hdd);
        }

        public void Update(HDD hdd) {
            _context.Entry(hdd).State = EntityState.Modified;
        }

        public void Delete(int id) {
            HDD hdd = _context.HDDs.Find(id);
            if (hdd != null)
                _context.HDDs.Remove(hdd);
        }
        
    }
}