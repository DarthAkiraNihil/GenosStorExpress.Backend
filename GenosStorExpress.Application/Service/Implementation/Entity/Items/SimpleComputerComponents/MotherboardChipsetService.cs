using GenosStorExpress.Application.Service.Interface.Entity.Items.SimpleComputerComponents;
using GenosStorExpress.Domain.Entity.Item.SimpleComputerComponent;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.SimpleComputerComponents {
    public class MotherboardChipsetService: IMotherboardChipsetService {
        private IGenosStorExpressRepositories _repositories;

        public MotherboardChipsetService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }

        public void Create(MotherboardChipset item) {
            _repositories.Items.SimpleComputerComponents.MotherboardChipsets.Create(item);
        }

        public MotherboardChipset Get(int id) {
            return _repositories.Items.SimpleComputerComponents.MotherboardChipsets.Get(id);
        }

        public List<MotherboardChipset> List() {
            return _repositories.Items.SimpleComputerComponents.MotherboardChipsets.List();
        }

        public void Update(MotherboardChipset item) {
            _repositories.Items.SimpleComputerComponents.MotherboardChipsets.Update(item);
        }

        public void Delete(int id) {
            _repositories.Items.SimpleComputerComponents.MotherboardChipsets.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }
    }
}