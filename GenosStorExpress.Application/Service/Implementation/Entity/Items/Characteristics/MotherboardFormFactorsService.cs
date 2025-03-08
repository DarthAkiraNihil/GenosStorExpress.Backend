using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.Characteristic;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.Characteristics {
    public class MotherboardFormFactorsService: IMotherboardFormFactorService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly IMotherboardFormFactorRepository _motherboardFormFactors;

        public MotherboardFormFactorsService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
            _motherboardFormFactors = _repositories.Items.Characteristics.MotherboardFormFactors;
        }

        public void Create(string item) {
            var created = new MotherboardFormFactor { Name = item };
            _motherboardFormFactors.Create(created);
        }

        public string Get(int id) {
            return _motherboardFormFactors.Get(id).Name;
        }

        public List<string> List() {
            return _motherboardFormFactors.List().Select(c => c.Name).ToList();
        }

        public void Update(int id, string item) {
            MotherboardFormFactor obj = _motherboardFormFactors.Get(id);
            obj.Name = item;
            _motherboardFormFactors.Update(obj);
        }

        public void Delete(int id) {
            _motherboardFormFactors.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        public bool BelongsToEnum(string value) {
            return _motherboardFormFactors.List().Exists(c => c.Name == value);
        }

        public MotherboardFormFactor GetEntityFromString(string value) {
            return _motherboardFormFactors.List().FirstOrDefault(c => c.Name == value, null);
        }
    }
}