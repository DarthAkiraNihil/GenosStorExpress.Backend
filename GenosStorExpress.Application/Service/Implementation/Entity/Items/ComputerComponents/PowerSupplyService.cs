using GenosStorExpress.Application.Service.Implementation.Base;
using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;
using GenosStorExpress.Application.Wrappers.Filters;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.ComputerComponents {
    public class PowerSupplyService: AbstractComputerComponentService, IPowerSupplyService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly IPowerSupplyRepository _powerSupplies;
        private readonly ICertificate80PlusService _certificate80PlusService;

        public PowerSupplyService(IItemTypeService itemTypeService, IVendorService vendorService, IGenosStorExpressRepositories repositories, ICertificate80PlusService certificate80PlusService) : base(itemTypeService, vendorService) {
            _repositories = repositories;
            _certificate80PlusService = certificate80PlusService;
            _powerSupplies = _repositories.Items.ComputerComponents.PowerSupplies;
        }

        public void Create(PowerSupplyWrapper item) {
            
            var created = new PowerSupply();
            _setEntityPropertiesFromWrapper(created, item);
            
            created.SataPorts = item.SataPorts;
            created.MolexPorts = item.MolexPorts;
            created.PowerOutput = item.PowerOutput;
            
            var certificate = _certificate80PlusService.GetEntityFromString(item.Certificate80Plus);
            if (certificate == null) {
                throw new NullReferenceException($"Сертификата 80Plus {item.Certificate80Plus} не существует");
            }

            created.Certificate80Plus = certificate;
            
            _powerSupplies.Create(created);
            item.Id = created.Id;
            
        }

        public PowerSupplyWrapper? Get(int id) {
            PowerSupply? obj = _powerSupplies.Get(id);
            if (obj == null) {
                return null;
            }
            var wrapped = new PowerSupplyWrapper();
            _setWrapperPropertiesFromEntity(obj, wrapped);
            
            wrapped.SataPorts = obj.SataPorts;
            wrapped.MolexPorts = obj.MolexPorts;
            wrapped.PowerOutput = obj.PowerOutput;
            wrapped.Certificate80Plus = obj.Certificate80Plus.Name;
            
            return wrapped;
        }

        public List<PowerSupplyWrapper> List() {
            return _powerSupplies.List().Select(obj => {
                var wrapped = new PowerSupplyWrapper();
                _setWrapperPropertiesFromEntity(obj, wrapped);
            
                wrapped.SataPorts = obj.SataPorts;
                wrapped.MolexPorts = obj.MolexPorts;
                wrapped.PowerOutput = obj.PowerOutput;
                wrapped.Certificate80Plus = obj.Certificate80Plus.Name;
            
                return wrapped;
            }).ToList();
        }

        public void Update(int id, PowerSupplyWrapper item) {
            var obj = _powerSupplies.Get(id);
            if (obj == null) {
                throw new NullReferenceException("Блока питания с номером {id} не существует");
            }
            _setEntityPropertiesFromWrapper(obj, item);
            
            obj.SataPorts = item.SataPorts;
            obj.MolexPorts = item.MolexPorts;
            obj.PowerOutput = item.PowerOutput;
            
            var certificate = _certificate80PlusService.GetEntityFromString(item.Certificate80Plus);
            if (certificate == null) {
                throw new NullReferenceException($"Сертификата 80Plus {item.Certificate80Plus} не существует");
            }

            obj.Certificate80Plus = certificate;
            
            _powerSupplies.Update(obj);
        }

        public void Delete(int id) {
            _powerSupplies.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        public IList<PowerSupplyWrapper> Filter(FilterContainerWrapper filters) {
            
            var filters_ = new List<Func<PowerSupplyWrapper, bool>>();

            IDictionary<string, RangeFilterWrapper> ranges = filters.Ranges;
            IDictionary<string, ChoiceFilterWrapper> choices = filters.Choices;
            IDictionary<string, bool> havings = filters.Havings;

            if (ranges.ContainsKey("sata_ports")) {
                if (ranges["sata_ports"].IsValid()) {
                    filters_.Add(
                        i => ranges["sata_ports"].From <= i.SataPorts && i.SataPorts <= ranges["sata_ports"].To
                    );
                }
            }

            if (ranges.ContainsKey("molex_ports")) {
                if (ranges["molex_ports"].IsValid()) {
                    filters_.Add(
                        i => ranges["molex_ports"].From <= i.MolexPorts && i.MolexPorts <= ranges["molex_ports"].To
                    );
                }
            }

            if (ranges.ContainsKey("power_output")) {
                if (ranges["power_output"].IsValid()) {
                    filters_.Add(
                        i => ranges["power_output"].From <= i.PowerOutput && i.PowerOutput <= ranges["power_output"].To
                    );
                }
            }

            if (choices.ContainsKey("certificates_80plus")) {
                filters_.Add(
                    i => choices["certificates_80plus"].CreateFilterClosure(n => n.Contains(i.Certificate80Plus))
                );
            }
            
            if (ranges.ContainsKey("price")) {
                if (ranges["price"].IsValid()) {
                    filters_.Add(
                        i => ranges["price"].From <= i.Price && i.Price <= ranges["price"].To
                    );
                }
            }

            if (ranges.ContainsKey("tdp")) {
                if (ranges["tdp"].IsValid()) {
                    filters_.Add(
                        i => ranges["tdp"].From <= i.TDP && i.TDP <= ranges["tdp"].To
                    );
                }
            }

            if (choices.ContainsKey("vendors")) {
                filters_.Add(
                    i => choices["vendors"].CreateFilterClosure(n => n.Contains(i.Vendor))
                );
            }
            
            var result = List();
            foreach (var filter in filters_) {
                result = result.Where(filter).ToList();
            }
            return result;
        }
    }
}