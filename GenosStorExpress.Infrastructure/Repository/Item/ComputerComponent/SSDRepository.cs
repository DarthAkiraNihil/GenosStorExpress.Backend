// using System.Collections.Generic;
// using Microsoft.EntityFrameworkCore;
// using System.Linq;
// using GenosStorExpress.Infrastructure.Context;
// using GenosStore.Model.Entity.Item.ComputerComponent;
// using GenosStore.Model.Repository.Interface.Item.ComputerComponent;
//
// namespace GenosStorExpress.Infrastructure.Repository.Item.ComputerComponent {
//     public class ComputerCaseRepository: IComputerCaseRepository {
//
//         private readonly GenosStorExpressDatabaseContext _context;
//         
//         public ComputerCaseRepository(GenosStorExpressDatabaseContext context) {
//             _context = context;
//         }
//
//         public List<ComputerCase> List() {
//             return _context.ComputerCases.ToList();
//         }
//
//         public ComputerCase Get(int id) {
//             return _context.ComputerCases.Find(id);
//         }
//
//         public void Create(ComputerCase computerCase) {
//             _context.ComputerCases.Add(computerCase);
//         }
//
//         public void Update(ComputerCase computerCase) {
//             _context.Entry(computerCase).State = EntityState.Modified;
//         }
//
//         public void Delete(int id) {
//             ComputerCase computerCase = _context.ComputerCases.Find(id);
//             if (computerCase != null)
//                 _context.ComputerCases.Remove(computerCase);
//         }
//         
//     }
// }