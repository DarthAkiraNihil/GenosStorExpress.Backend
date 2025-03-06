using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface.Item.Characteristic;
using GenosStorExpress.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenosStorExpress.Infrastructure.Repository.Item.Characteristic {
    public class VideoPortRepository: IVideoPortRepository {

        private readonly GenosStorExpressDatabaseContext _context;
        
        public VideoPortRepository(GenosStorExpressDatabaseContext context) {
            _context = context;
        }

        public List<VideoPort> List() {
            return _context.VideoPorts.ToList();
        }

        public VideoPort Get(int id) {
            return _context.VideoPorts.Find(id);
        }

        public void Create(VideoPort videoPort) {
            _context.VideoPorts.Add(videoPort);
        }

        public void Update(VideoPort videoPort) {
            _context.Entry(videoPort).State = EntityState.Modified;
        }

        public void Delete(int id) {
            VideoPort videoPort = _context.VideoPorts.Find(id);
            if (videoPort != null)
                _context.VideoPorts.Remove(videoPort);
        }
        
    }
}