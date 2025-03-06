// using GenosStore.Model.Repository.Interface;
// using GenosStorExpress.Application.Service.Interface.Entity.Users;
//
// namespace GenosStorExpress.Application.Service.Implementation.Entity.Users {
//     public class CustomerService: ICustomerService {
//         private IGenosStoreRepositories _repositories;
//
//         public void Create(Customer item) {
//             _repositories.Users.Customers.Create(item);
//         }
//
//         public Customer Get(int id) {
//             return _repositories.Users.Customers.Get(id);
//         }
//
//         public List<Customer> List() {
//             return _repositories.Users.Customers.List();
//         }
//
//         public void Update(Customer item) {
//             _repositories.Users.Customers.Update(item);
//         }
//
//         public void Delete(int id) {
//             _repositories.Users.Customers.Delete(id);
//         }
//
//         public int Save() {
//             return _repositories.Save();
//         }
//     }
// }