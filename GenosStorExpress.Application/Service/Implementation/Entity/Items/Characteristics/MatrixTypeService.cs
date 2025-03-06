using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.Characteristics {
    public class MatrixTypeService: IMatrixTypeService {
        private readonly IGenosStorExpressRepositories _repositories;

        public MatrixTypeService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }

        public void Create(string item) {
            var created = new MatrixType { Name = item };
            _repositories.Items.Characteristics.MatrixTypes.Create(created);
        }

        public string Get(int id) {
            return _repositories.Items.Characteristics.MatrixTypes.Get(id).Name;
        }

        public List<string> List() {
            return _repositories.Items.Characteristics.MatrixTypes.List().Select(c => c.Name).ToList();
        }

        public void Update(int id, string item) {
            MatrixType obj = _repositories.Items.Characteristics.MatrixTypes.Get(id);
            obj.Name = item;
            _repositories.Items.Characteristics.MatrixTypes.Update(obj);
        }

        public void Delete(int id) {
            _repositories.Items.Characteristics.MatrixTypes.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        public bool BelongsToEnum(string value) {
            return _repositories.Items.Characteristics.MatrixTypes.List().Exists(c => c.Name == value);
        }
    }
}