using GenosStorExpress.Application.Service.Interface.Entity.Items.SimpleComputerComponents;
using GenosStorExpress.Domain.Entity.Item.SimpleComputerComponent;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.SimpleComputerComponents {
    public class AudioChipsetService: IAudioChipsetService {
        private IGenosStorExpressRepositories _repositories;

        public AudioChipsetService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }

        public void Create(AudioChipset item) {
            _repositories.Items.SimpleComputerComponents.AudioChipsets.Create(item);
        }

        public AudioChipset Get(int id) {
            return _repositories.Items.SimpleComputerComponents.AudioChipsets.Get(id);
        }

        public List<AudioChipset> List() {
            return _repositories.Items.SimpleComputerComponents.AudioChipsets.List();
        }

        public void Update(AudioChipset item) {
            _repositories.Items.SimpleComputerComponents.AudioChipsets.Update(item);
        }

        public void Delete(int id) {
            _repositories.Items.SimpleComputerComponents.AudioChipsets.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }
    }
}