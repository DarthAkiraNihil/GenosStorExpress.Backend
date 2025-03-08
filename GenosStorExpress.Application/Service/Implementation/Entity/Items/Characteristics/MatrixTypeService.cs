using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.Characteristic;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.Characteristics {
    public class MatrixTypeService: IMatrixTypeService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly IMatrixTypeRepository _matrixTypes;

        public MatrixTypeService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
            _matrixTypes = _repositories.Items.Characteristics.MatrixTypes;
        }

        public void Create(string item) {
            var created = new MatrixType { Name = item };
            _matrixTypes.Create(created);
        }

        public string Get(int id) {
            return _matrixTypes.Get(id).Name;
        }

        public List<string> List() {
            return _matrixTypes.List().Select(c => c.Name).ToList();
        }

        public void Update(int id, string item) {
            MatrixType obj = _matrixTypes.Get(id);
            obj.Name = item;
            _matrixTypes.Update(obj);
        }

        public void Delete(int id) {
            _matrixTypes.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        public bool BelongsToEnum(string value) {
            return _matrixTypes.List().Exists(c => c.Name == value);
        }

        public MatrixType GetEntityFromString(string value) {
            return _matrixTypes.List().FirstOrDefault(c => c.Name == value, null);
        }
    }
}