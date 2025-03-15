using GenosStorExpress.Application.Service.Implementation.Base;
using GenosStorExpress.Application.Service.Interface.Entity.Items.SimpleComputerComponents;
using GenosStorExpress.Application.Wrappers.Entity.Item.SimpleComputerComponent;
using GenosStorExpress.Domain.Entity.Item.SimpleComputerComponent;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.SimpleComputerComponent;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.SimpleComputerComponents {
    public class AudioChipsetService: AbstractSimpleComputerComponentService, IAudioChipsetService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly IAudioChipsetRepository _audioChipsets;

        public AudioChipsetService(ISimpleComputerComponentTypeService simpleComputerComponentTypeService, IGenosStorExpressRepositories repositories) : base(simpleComputerComponentTypeService) {
            _repositories = repositories;
            _audioChipsets = _repositories.Items.SimpleComputerComponents.AudioChipsets;
        }
        
        public void Create(AudioChipsetWrapper item) {
            var created = new AudioChipset();
            _setEntityPropertiesFromWrapper(created, item);
            _audioChipsets.Create(created);
        }

        public AudioChipsetWrapper? Get(int id) {
            AudioChipset? obj = _audioChipsets.Get(id);
            if (obj == null) {
                return null;
            }
            var wrapped = new AudioChipsetWrapper();
            _setWrapperPropertiesFromEntity(obj, wrapped);
            return wrapped;
        }

        public List<AudioChipsetWrapper> List() {
            return _audioChipsets.List().Select(obj => {
                var wrapped = new AudioChipsetWrapper();
                _setWrapperPropertiesFromEntity(obj, wrapped);
                return wrapped;
            }).ToList();
        }

        public void Update(int id, AudioChipsetWrapper item) {
            var obj = _audioChipsets.Get(id);
            if (obj == null) {
                throw new NullReferenceException($"Аудиочипсета с номером {id} не существует");
            }
            _setEntityPropertiesFromWrapper(obj, item);
            _audioChipsets.Create(obj);
        }

        public void Delete(int id) {
            _audioChipsets.Delete(id);
        }

        public AudioChipset? GetRaw(int id) {
            return _audioChipsets.Get(id);
        }

        public int Save() {
            return _repositories.Save();
        }
    }
}