using GenosStorExpress.Domain.Entity.Item.SimpleComputerComponent;
using GenosStorExpress.Domain.Interface.Item.SimpleComputerComponent;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Item.SimpleComputerComponent {
    public class NetworkAdapterRepository: INetworkAdapterRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public NetworkAdapterRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<NetworkAdapter> List() {
            return _context.NetworkAdapters.ToList();
        }

        public NetworkAdapter? Get(int id) {
            return _context.NetworkAdapters.Find(id);
        }

        public void Create(NetworkAdapter networkAdapter) {
            _context.NetworkAdapters.Add(networkAdapter);
        }

        public void Update(NetworkAdapter networkAdapter) {
            _context.Entry(networkAdapter).State = EntityState.Modified;
        }

        public void Delete(int id) {
            NetworkAdapter? networkAdapter = _context.NetworkAdapters.Find(id);
            if (networkAdapter != null) {
                _context.NetworkAdapters.Remove(networkAdapter);
            }
        }
        
    }
}