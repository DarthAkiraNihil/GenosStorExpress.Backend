using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.Characteristics {
    public class DPIModeService: IDPIModeService {
        private readonly IGenosStorExpressRepositories _repositories;

        public DPIModeService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }

        public List<DPIMode> List() {
            return _repositories.Items.Characteristics.DPIModes.List();
        }
        
    }
}