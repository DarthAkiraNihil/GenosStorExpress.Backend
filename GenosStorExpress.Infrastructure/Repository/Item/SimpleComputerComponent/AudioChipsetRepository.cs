using GenosStorExpress.Domain.Entity.Item.SimpleComputerComponent;
using GenosStorExpress.Domain.Interface.Item.SimpleComputerComponent;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Item.SimpleComputerComponent {
    public class AudioChipsetRepository: IAudioChipsetRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public AudioChipsetRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<AudioChipset> List() {
            return _context.AudioChipsets.ToList();
        }

        public AudioChipset Get(int id) {
            return _context.AudioChipsets.Find(id);
        }

        public void Create(AudioChipset audioChipset) {
            _context.AudioChipsets.Add(audioChipset);
        }

        public void Update(AudioChipset audioChipset) {
            _context.Entry(audioChipset).State = EntityState.Modified;
        }

        public void Delete(int id) {
            AudioChipset audioChipset = _context.AudioChipsets.Find(id);
            if (audioChipset != null)
                _context.AudioChipsets.Remove(audioChipset);
        }
        
    }
}