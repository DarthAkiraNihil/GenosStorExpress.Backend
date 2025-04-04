﻿using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Item.ComputerComponent {
    public class DiskDriveRepository: IDiskDriveRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public DiskDriveRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<DiskDrive> List() {
            return _context.DiskDrives.Include(i => i.Reviews).ToList();
        }

        public DiskDrive? Get(int id) {
            return _context.DiskDrives
                           .Include(i => i.Reviews)
                           .FirstOrDefault(i => i.Id == id);
        }

        public void Create(DiskDrive item) {
            _context.DiskDrives.Add(item);
            _context.SaveChanges();
        }

        public void Update(DiskDrive item) {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id) {
            DiskDrive? diskDrive = _context.DiskDrives.Find(id);
            if (diskDrive != null) {
                _context.DiskDrives.Remove(diskDrive);
            }
        }
        
    }
}