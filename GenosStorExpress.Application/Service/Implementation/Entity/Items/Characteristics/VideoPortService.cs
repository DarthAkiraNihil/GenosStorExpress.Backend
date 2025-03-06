using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.Characteristics {
    public class VideoPortService: IVideoPortService {
        private readonly IGenosStorExpressRepositories _repositories;

        public VideoPortService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }

        public void Create(string item) {
            var created = new VideoPort { Name = item };
            _repositories.Items.Characteristics.VideoPorts.Create(created);
        }

        public string Get(int id) {
            return _repositories.Items.Characteristics.VideoPorts.Get(id).Name;
        }

        public List<string> List() {
            return _repositories.Items.Characteristics.VideoPorts.List().Select(c => c.Name).ToList();
        }

        public void Update(int id, string item) {
            VideoPort obj = _repositories.Items.Characteristics.VideoPorts.Get(id);
            obj.Name = item;
            _repositories.Items.Characteristics.VideoPorts.Update(obj);
        }

        public void Delete(int id) {
            _repositories.Items.Characteristics.VideoPorts.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        public bool BelongsToEnum(string value) {
            return _repositories.Items.Characteristics.VideoPorts.List().Exists(c => c.Name == value);
        }
    }
}