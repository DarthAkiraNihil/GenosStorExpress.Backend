using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Application.Wrappers.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.Characteristics {
    public class DefinitionService: IDefinitionService {

        private readonly IGenosStorExpressRepositories _repositories;

        public DefinitionService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }

        public void Create(DefinitionWrapper item) {
            var obj = new Definition { Width = item.Width, Height = item.Height};
            _repositories.Items.Characteristics.Definitions.Create(obj);
        }

        public DefinitionWrapper? Get(int id) {
            Definition? obj = _repositories.Items.Characteristics.Definitions.Get(id);
            if (obj == null) {
                return null;
            }
            return new DefinitionWrapper { Width = obj.Width, Height = obj.Height };
        }

        public List<DefinitionWrapper> List() {
            return _repositories
                   .Items
                   .Characteristics
                   .Definitions
                   .List()
                   .Select(item => new DefinitionWrapper { Width = item.Width, Height = item.Height })
                   .ToList();
        }

        public Definition? GetRaw(int id) {
            return _repositories.Items.Characteristics.Definitions.Get(id);
        }

        public void Update(int id, DefinitionWrapper item) {
            Definition obj = _repositories.Items.Characteristics.Definitions.Get(id)!;
            
            if (obj.Height != item.Height) {
                obj.Height = item.Height;
            }
            if (obj.Width != item.Width) {
                obj.Width = item.Width;
            }
            
            _repositories.Items.Characteristics.Definitions.Update(obj);
        }

        public void Delete(int id) {
            _repositories.Items.Characteristics.Definitions.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }
    }
}