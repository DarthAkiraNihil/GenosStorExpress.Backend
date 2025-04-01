using GenosStorExpress.Application.Service.Implementation.Base;
using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Application.Service.Interface.Entity.Items.SimpleComputerComponents;
using GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;
using GenosStorExpress.Application.Wrappers.Filters;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.ComputerComponents {
    public class SataSSDService: AbstractSSDService, ISataSSDService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly ISataSSDRepository _sataSSDs;

        public SataSSDService(IItemTypeService itemTypeService, IVendorService vendorService, ISSDControllerService ssdControllerService, IGenosStorExpressRepositories repositories) : base(itemTypeService, vendorService, ssdControllerService) {
            _repositories = repositories;
            _sataSSDs = _repositories.Items.ComputerComponents.SataSSDs;
        }

        public void Create(SataSSDWrapper item) {
            var created = new SataSSD();
            _setEntityPropertiesFromWrapper(created, item);
            _sataSSDs.Create(created);
            item.Id = created.Id;
        }

        public SataSSDWrapper? Get(int id) {
            SataSSD? obj = _sataSSDs.Get(id);
            if (obj == null) {
                return null;
            }
            var wrapped = new SataSSDWrapper();
            _setWrapperPropertiesFromEntity(obj, wrapped);
            return wrapped;
        }

        public List<SataSSDWrapper> List() {
            return _sataSSDs.List().Select(obj => {
                var wrapped = new SataSSDWrapper();
                _setWrapperPropertiesFromEntity(obj, wrapped);
                return wrapped;
            }).ToList();
        }

        public void Update(int id, SataSSDWrapper item) {
            var obj = _sataSSDs.Get(id);
            if (obj == null) {
                throw new NullReferenceException($"Твердотельного накопителя Sata с номером {id} не существует");
            }
            _setEntityPropertiesFromWrapper(obj, item);
            _sataSSDs.Update(obj);
        }

        public void Delete(int id) {
            _sataSSDs.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        public IList<SataSSDWrapper> Filter(FilterContainerWrapper filters) {
            var filters_ = new List<Func<SataSSDWrapper, bool>>();

            IDictionary<string, RangeFilterWrapper> ranges = filters.Ranges;
            IDictionary<string, ChoiceFilterWrapper> choices = filters.Choices;

            if (ranges.ContainsKey("capacity")) {
                if (ranges["capacity"].IsValid()) {
                    filters_.Add(
                        i => ranges["capacity"].From <= i.Capacity && i.Capacity <= ranges["capacity"].To
                    );
                }
            }

            if (ranges.ContainsKey("read_speed")) {
                if (ranges["read_speed"].IsValid()) {
                    filters_.Add(
                        i => ranges["read_speed"].From <= i.ReadSpeed && i.ReadSpeed <= ranges["read_speed"].To
                    );
                }
            }

            if (ranges.ContainsKey("write_speed")) {
                if (ranges["write_speed"].IsValid()) {
                    filters_.Add(
                        i => ranges["write_speed"].From <= i.WriteSpeed && i.WriteSpeed <= ranges["write_speed"].To
                    );
                }
            }

            if (ranges.ContainsKey("tbw")) {
                if (ranges["tbw"].IsValid()) {
                    filters_.Add(
                        i => ranges["tbw"].From <= i.TBW && i.TBW <= ranges["tbw"].To
                    );
                }
            }

            if (ranges.ContainsKey("dwpd")) {
                if (ranges["dwpd"].IsValid()) {
                    filters_.Add(
                        i => ranges["dwpd"].From <= i.DWPD && i.DWPD <= ranges["dwpd"].To
                    );
                }
            }

            if (ranges.ContainsKey("bits_for_cell")) {
                if (ranges["bits_for_cell"].IsValid()) {
                    filters_.Add(
                        i => ranges["bits_for_cell"].From <= i.BitsForCell && i.BitsForCell <= ranges["bits_for_cell"].To
                    );
                }
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