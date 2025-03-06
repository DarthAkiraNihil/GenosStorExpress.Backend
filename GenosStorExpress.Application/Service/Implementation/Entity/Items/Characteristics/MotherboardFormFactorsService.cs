using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Domain.Entity.Item.Characteristic;
using GenosStorExpress.Domain.Interface;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.Characteristics {
    public class MotherboardFormFactorsService: IMotherboardFormFactorService {
        private readonly IGenosStorExpressRepositories _repositories;

        public MotherboardFormFactorsService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
        }

        public void Create(string item) {
            var created = new MotherboardFormFactor { Name = item };
            _repositories.Items.Characteristics.MotherboardFormFactors.Create(created);
        }

        public string Get(int id) {
            return _repositories.Items.Characteristics.MotherboardFormFactors.Get(id).Name;
        }

        public List<string> List() {
            return _repositories.Items.Characteristics.MotherboardFormFactors.List().Select(c => c.Name).ToList();
        }

        public void Update(int id, string item) {
            MotherboardFormFactor obj = _repositories.Items.Characteristics.MotherboardFormFactors.Get(id);
            obj.Name = item;
            _repositories.Items.Characteristics.MotherboardFormFactors.Update(obj);
        }

        public void Delete(int id) {
            _repositories.Items.Characteristics.MotherboardFormFactors.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        public bool BelongsToEnum(string value) {
            return _repositories.Items.Characteristics.MotherboardFormFactors.List().Exists(c => c.Name == value);
        }
    }
}