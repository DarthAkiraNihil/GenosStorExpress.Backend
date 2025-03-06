using GenosStorExpress.Application.Service.Interface;
using GenosStorExpress.Application.Service.Interface.Common;
using GenosStorExpress.Application.Service.Interface.Entity;

namespace GenosStorExpress.Application.Service.Implementation {
    public class Services: IServices {
        
        private ICommonServices _commonServices;
        private IEntityServices _entityServices;

        public ICommonServices Common {
            get {
                return _commonServices;
            }
        }

        public IEntityServices Entity {
            get {
                return _entityServices;
            }
        }

        public Services(ICommonServices commonServices, IEntityServices entityServices) {
            _commonServices = commonServices;
            _entityServices = entityServices;
        }
    }
}