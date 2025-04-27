using GenosStorExpress.Application.Service.Implementation.Base;
using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Application.Service.Interface.Entity.Orders;
using GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;
using GenosStorExpress.Application.Wrappers.Filters;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.ComputerComponents {
    public class HDDService: AbstractDiskDriveService, IHDDService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly IHDDRepository _hdds;

        public HDDService(IItemTypeService itemTypeService, IActiveDiscountService activeDiscountService, IVendorService vendorService, IGenosStorExpressRepositories repositories) : base(itemTypeService, activeDiscountService, vendorService) {
            _repositories = repositories;
            _hdds = _repositories.Items.ComputerComponents.HDDs;
        }

        public void Create(HDDWrapper item) {
            var created = new HDD();
            
            _setEntityPropertiesFromWrapper(created, item);
            created.RPM = item.RPM;
            
            _hdds.Create(created);
            item.Id = created.Id;
        }

        public HDDWrapper? Get(int id) {
            HDD? obj = _hdds.Get(id);
            if (obj == null) {
                return null;
            }
            var wrapped = new HDDWrapper();
            
            _setWrapperPropertiesFromEntity(obj, wrapped);
            wrapped.RPM = obj.RPM;
            
            return wrapped;
        }

        public List<HDDWrapper> List() {
            return _hdds.List().Select(obj => {
                var wrapped = new HDDWrapper();
            
                _setWrapperPropertiesFromEntity(obj, wrapped);
                wrapped.RPM = obj.RPM;
            
                return wrapped;
            }).ToList();
        }

        public void Update(int id, HDDWrapper item) {
            HDD? obj = _hdds.Get(id);
            if (obj == null) {
                throw new NullReferenceException($"Жёсткого диска с номером {id} не существует");
            }
            _setEntityPropertiesFromWrapper(obj, item);
            obj.RPM = item.RPM;
            
            _hdds.Update(obj);
        }

        public void Delete(int id) {
            _hdds.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        public IList<HDDWrapper> Filter(FilterContainerWrapper filters) {
            var filters_ = new List<Func<HDDWrapper, bool>>();

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

            if (ranges.ContainsKey("rpm")) {
                if (ranges["rpm"].IsValid()) {
                    filters_.Add(
                        i => ranges["rpm"].From <= i.RPM && i.RPM <= ranges["rpm"].To
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
            
            if (filters.Name.Length != 0) {
                filters_.Add(
                    i => i.Name.Contains(filters.Name)
                );
            }
            
            var result = List();
            foreach (var filter in filters_) {
                result = result.Where(filter).ToList();
            }
            return result;
        }
        
        /// <summary>
        /// Получение данных о возможных фильтрах товара
        /// </summary>
        /// <returns>Список возможных фильтров</returns>
        public IList<FilterDescription> FilterData() {
            return new List<FilterDescription> {
                new FilterDescription {
                    Type = FilterType.Range,
                    Name = "price",
                    VerboseName = "Цена"
                },
                new FilterDescription {
                    Type = FilterType.Choice,
                    Name = "vendors",
                    VerboseName = "Производители",
                    Choices = _repositories.Items.Characteristics.Vendors.List().Select(i => i.Name).ToList()
                },
                new FilterDescription {
                    Type = FilterType.Range,
                    Name = "tdp",
                    VerboseName = "tdp"
                },
                new FilterDescription {
                    Type = FilterType.Range,
                    Name = "capacity",
                    VerboseName = "capacity"
                },
                new FilterDescription {
                    Type = FilterType.Range,
                    Name = "read_speed",
                    VerboseName = "read_speed"
                },
                new FilterDescription {
                    Type = FilterType.Range,
                    Name = "write_speed",
                    VerboseName = "write_speed"
                },
                new FilterDescription {
                    Type = FilterType.Range,
                    Name = "rpm",
                    VerboseName = "rpm"
                },
            };
        }
    }
}