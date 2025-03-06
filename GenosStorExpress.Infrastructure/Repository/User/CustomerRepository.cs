// using System.Collections.Generic;
// using Microsoft.EntityFrameworkCore;
// using System.Linq;
// using GenosStorExpress.Infrastructure.Context;
// using GenosStore.Model.Entity.User;
// using GenosStore.Model.Repository.Interface.User;
//
// namespace GenosStorExpress.Infrastructure.Repository.User {
//     public class CustomerRepository: ICustomerRepository {
//
//         private readonly GenosStorExpressDatabaseContext _context;
//         
//         public CustomerRepository(GenosStorExpressDatabaseContext context) {
//             _context = context;
//         }
//
//         public List<Customer> List() {
//             return _context.Customers.ToList();
//         }
//
//         public Customer Get(int id) {
//             return _context.Customers.Find(id);
//         }
//
//         public void Create(Customer customer) {
//             _context.Customers.Add(customer);
//         }
//
//         public void Update(Customer customer) {
//             _context.Entry(customer).State = EntityState.Modified;
//         }
//
//         public void Delete(int id) {
//             Customer customer = _context.Customers.Find(id);
//             if (customer != null)
//                 _context.Customers.Remove(customer);
//         }
//         
//     }
// }