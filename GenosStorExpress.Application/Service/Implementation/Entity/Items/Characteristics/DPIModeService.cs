using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.Characteristic;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.Characteristics {
    public class DPIModeService: IDPIModeService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly IDPIModeRepository _dpiModes;

        public DPIModeService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
            _dpiModes = _repositories.Items.Characteristics.DPIModes;
        }

        public List<int> List() {
            return _repositories.Items.Characteristics.DPIModes.List().Select(i => i.DPI).ToList();
        }

        public DPIMode GetByValue(int dpi) {
            return _dpiModes.List().FirstOrDefault(i => i.DPI == dpi);
        }
    }
}