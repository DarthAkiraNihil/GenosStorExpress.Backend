using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface.Item.Characteristic;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Item.Characteristic {
    public class PCIEVersionRepository: IPCIEVersionRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public PCIEVersionRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<PCIEVersion> List() {
            return _context.PCIEVersions.ToList();
        }

        public PCIEVersion Get(int id) {
            return _context.PCIEVersions.Find(id);
        }

        public void Create(PCIEVersion pcieVersion) {
            _context.PCIEVersions.Add(pcieVersion);
        }

        public void Update(PCIEVersion pcieVersion) {
            _context.Entry(pcieVersion).State = EntityState.Modified;
        }

        public void Delete(int id) {
            PCIEVersion pcieVersion = _context.PCIEVersions.Find(id);
            if (pcieVersion != null)
                _context.PCIEVersions.Remove(pcieVersion);
        }
        
    }
}