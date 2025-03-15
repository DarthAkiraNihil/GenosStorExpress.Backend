using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.Characteristic;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.Characteristics {
    public class VideoPortService: IVideoPortService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly IVideoPortRepository _videoPorts;

        public VideoPortService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
            _videoPorts = _repositories.Items.Characteristics.VideoPorts;
        }

        public void Create(string item) {
            var created = new VideoPort { Name = item };
            _videoPorts.Create(created);
        }

        public string? Get(int id) {
            return _videoPorts.Get(id)?.Name;
        }

        public List<string> List() {
            return _videoPorts.List().Select(c => c.Name).ToList();
        }

        public void Update(int id, string item) {
            VideoPort obj = _videoPorts.Get(id)!;
            obj.Name = item;
            _videoPorts.Update(obj);
        }

        public void Delete(int id) {
            _videoPorts.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        public bool BelongsToEnum(string value) {
            return _repositories.Items.Characteristics.VideoPorts.List().Exists(c => c.Name == value);
        }

        public VideoPort? GetEntityFromString(string value) {
            return _videoPorts.List().FirstOrDefault(c => c.Name == value);
        }
    }
}