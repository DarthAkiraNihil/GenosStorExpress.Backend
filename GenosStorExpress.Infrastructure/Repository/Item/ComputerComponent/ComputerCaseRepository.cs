using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Item.ComputerComponent {
    public class ComputerCaseRepository: IComputerCaseRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public ComputerCaseRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<ComputerCase> List() {
            return _context.ComputerCases
                           .Include(i => i.Reviews)
                           //.Include(i => i.ItemType)
                           //.Include(i => i.Vendor)
                           //.Include(i => i.SupportedMotherboardFormFactors)
                           //.Include(i => i.ComputerCaseTypesize)
                           .ToList();
        }

        public ComputerCase Get(int id) {
            return _context.ComputerCases.Include(i => i.ItemType)
	            .Include(i => i.Vendor)
	            .Include(i => i.SupportedMotherboardFormFactors)
	            .Include(i => i.Typesize).First(i => i.Id == id);
        }

        public void Create(ComputerCase computerCase) {
            _context.ComputerCases.Add(computerCase);
        }

        public void Update(ComputerCase computerCase) {
            _context.Entry(computerCase).State = EntityState.Modified;
        }

        public void Delete(int id) {
            ComputerCase computerCase = _context.ComputerCases.Find(id);
            if (computerCase != null)
                _context.ComputerCases.Remove(computerCase);
        }
        
    }
}