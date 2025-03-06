using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface.Item.Characteristic;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Item.Characteristic {
    public class ComputerCaseTypesizeRepository: IComputerCaseTypesizeRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public ComputerCaseTypesizeRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<ComputerCaseTypesize> List() {
            return _context.ComputerCaseTypesizes.ToList();
        }

        public ComputerCaseTypesize Get(int id) {
            return _context.ComputerCaseTypesizes.Find(id);
        }

        public void Create(ComputerCaseTypesize computerCaseTypesize) {
            _context.ComputerCaseTypesizes.Add(computerCaseTypesize);
        }

        public void Update(ComputerCaseTypesize computerCaseTypesize) {
            _context.Entry(computerCaseTypesize).State = EntityState.Modified;
        }

        public void Delete(int id) {
            ComputerCaseTypesize computerCaseTypesize = _context.ComputerCaseTypesizes.Find(id);
            if (computerCaseTypesize != null)
                _context.ComputerCaseTypesizes.Remove(computerCaseTypesize);
        }
        
    }
}