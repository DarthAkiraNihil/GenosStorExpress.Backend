// using System.Collections.Generic;
// using Microsoft.EntityFrameworkCore;
// using System.Linq;
// using GenosStorExpress.Infrastructure.Context;
// using GenosStore.Model.Entity.Item.ComputerComponent;
// using GenosStore.Model.Repository.Interface.Item.ComputerComponent;
//
// namespace GenosStorExpress.Infrastructure.Repository.Item.ComputerComponent {
//     public class DiskDriveRepository: IDiskDriveRepository {
//
//         private readonly GenosStorExpressDatabaseContext _context;
//         
//         public DiskDriveRepository(GenosStorExpressDatabaseContext context) {
//             _context = context;
//         }
//
//         public List<DiskDrive> List() {
//             return _context.DiskDrives.ToList();
//         }
//
//         public DiskDrive Get(int id) {
//             return _context.DiskDrives.Find(id);
//         }
//
//         public void Create(DiskDrive DiskDrive) {
//             _context.DiskDrives.Add(DiskDrive);
//         }
//
//         public void Update(DiskDrive DiskDrive) {
//             _context.Entry(DiskDrive).State = EntityState.Modified;
//         }
//
//         public void Delete(int id) {
//             DiskDrive DiskDrive = _context.DiskDrives.Find(id);
//             if (DiskDrive != null)
//                 _context.DiskDrives.Remove(DiskDrive);
//         }
//         
//     }
// }