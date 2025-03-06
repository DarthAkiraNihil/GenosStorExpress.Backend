using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.Characteristics {
    public class ComputerCaseTypesizesService: IComputerCaseTypesizeService {

        private readonly IGenosStorExpressRepositories _repositories;

        public ComputerCaseTypesizesService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }


        public void Create(string item) {
            var created = new ComputerCaseTypesize { Name = item };
            _repositories.Items.Characteristics.ComputerCaseTypesizes.Create(created);
        }

        public string Get(int id) {
            return _repositories.Items.Characteristics.ComputerCaseTypesizes.Get(id).Name;
        }

        public List<string> List() {
            return _repositories.Items.Characteristics.ComputerCaseTypesizes.List().Select(c => c.Name).ToList();
        }

        public void Update(int id, string item) {
            ComputerCaseTypesize obj = _repositories.Items.Characteristics.ComputerCaseTypesizes.Get(id);
            obj.Name = item;
            _repositories.Items.Characteristics.ComputerCaseTypesizes.Update(obj);
        }

        public void Delete(int id) {
            _repositories.Items.Characteristics.ComputerCaseTypesizes.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        public bool BelongsToEnum(string value) {
            return _repositories.Items.Characteristics.ComputerCaseTypesizes.List().Exists(c => c.Name == value);
        }
        
    }
}