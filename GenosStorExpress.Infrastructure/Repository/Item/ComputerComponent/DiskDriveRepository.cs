using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using GenosStorExpress.Infrastructure.Context;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;

namespace GenosStorExpress.Infrastructure.Repository.Item.ComputerComponent {
    public class DiskDriveRepository: IDiskDriveRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public DiskDriveRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<DiskDrive> List() {
            return _context.DiskDrives.ToList();
        }

        public DiskDrive Get(int id) {
            return _context.DiskDrives.Find(id);
        }

        public void Create(DiskDrive item) {
            _context.DiskDrives.Add(item);
        }

        public void Update(DiskDrive item) {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id) {
            DiskDrive DiskDrive = _context.DiskDrives.Find(id);
            if (DiskDrive != null)
                _context.DiskDrives.Remove(DiskDrive);
        }
        
    }
}