using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.Characteristic;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.Characteristics {
    public class ComputerCaseTypesizesService: IComputerCaseTypesizeService {

        private readonly IGenosStorExpressRepositories _repositories;
        private readonly IComputerCaseTypesizeRepository _computerCaseTypesizes;

        public ComputerCaseTypesizesService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
            _computerCaseTypesizes = _repositories.Items.Characteristics.ComputerCaseTypesizes;
        }


        public void Create(string item) {
            var created = new ComputerCaseTypesize { Name = item };
            _computerCaseTypesizes.Create(created);
        }

        public string? Get(int id) {
            return _computerCaseTypesizes.Get(id)?.Name;
        }

        public List<string> List() {
            return _computerCaseTypesizes.List().Select(c => c.Name).ToList();
        }

        public void Update(int id, string item) {
            ComputerCaseTypesize obj = _computerCaseTypesizes.Get(id)!;
            obj.Name = item;
            _computerCaseTypesizes.Update(obj);
        }

        public void Delete(int id) {
            _computerCaseTypesizes.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        public bool BelongsToEnum(string value) {
            return _computerCaseTypesizes.List().Exists(c => c.Name == value);
        }

        public ComputerCaseTypesize? GetEntityFromString(string value) {
            return _computerCaseTypesizes.List().FirstOrDefault(c => c.Name == value);
        }
    }
}