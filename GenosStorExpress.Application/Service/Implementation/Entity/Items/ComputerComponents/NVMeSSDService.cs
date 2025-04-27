using GenosStorExpress.Application.Service.Implementation.Base;
using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Application.Service.Interface.Entity.Items.SimpleComputerComponents;
using GenosStorExpress.Application.Service.Interface.Entity.Orders;
using GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;
using GenosStorExpress.Application.Wrappers.Filters;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.ComputerComponents {
    public class NVMeSSDService: AbstractSSDService, INVMeSSDService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly INVMeSSDRepository _nvmeSSDs;

        public NVMeSSDService(IItemTypeService itemTypeService, IActiveDiscountService activeDiscountService, IVendorService vendorService, ISSDControllerService ssdControllerService, IGenosStorExpressRepositories repositories) : base(itemTypeService, activeDiscountService, vendorService, ssdControllerService) {
            _repositories = repositories;
            _nvmeSSDs = _repositories.Items.ComputerComponents.NVMeSSDs;
        }
        
        public void Create(NVMeSSDWrapper item) {
            var created = new NVMeSSD();
            _setEntityPropertiesFromWrapper(created, item);
            _nvmeSSDs.Create(created);
            item.Id = created.Id;
        }

        public NVMeSSDWrapper? Get(int id) {
            NVMeSSD? obj = _nvmeSSDs.Get(id);
            if (obj == null) {
                return null;
            }
            var wrapped = new NVMeSSDWrapper();
            _setWrapperPropertiesFromEntity(obj, wrapped);
            return wrapped;
        }

        public List<NVMeSSDWrapper> List() {
            return _nvmeSSDs.List().Select(obj => {
                var wrapped = new NVMeSSDWrapper();
                _setWrapperPropertiesFromEntity(obj, wrapped);
                return wrapped;
            }).ToList();
        }

        public void Update(int id, NVMeSSDWrapper item) {
            var obj = _nvmeSSDs.Get(id);
            if (obj == null) {
                throw new NullReferenceException($"Твердотельного накопителя NVMe с номером {id} не существует");
            }
            _setEntityPropertiesFromWrapper(obj, item);
            _nvmeSSDs.Update(obj);
        }

        public void Delete(int id) {
            _nvmeSSDs.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }
        
        public List<NVMeSSDWrapper> Filter(List<Func<NVMeSSDWrapper, bool>> filters) {
            var result = List();
            foreach (var filter in filters) {
                result = result.Where(filter).ToList();
            }
            return result;
        }

        public IList<NVMeSSDWrapper> Filter(FilterContainerWrapper filters) {
            var filters_ = new List<Func<NVMeSSDWrapper, bool>>();

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
                    VerboseName = "Потребляемая мощность"
                },
                new FilterDescription {
                    Type = FilterType.Range,
                    Name = "capacity",
                    VerboseName = "Ёмкость"
                },
                new FilterDescription {
                    Type = FilterType.Range,
                    Name = "read_speed",
                    VerboseName = "Скорость чтения"
                },
                new FilterDescription {
                    Type = FilterType.Range,
                    Name = "write_speed",
                    VerboseName = "Скорость записи"
                },
                new FilterDescription {
                    Type = FilterType.Range,
                    Name = "tbw",
                    VerboseName = "TBW"
                },
                new FilterDescription {
                    Type = FilterType.Range,
                    Name = "dwpd",
                    VerboseName = "DWPD"
                },
                new FilterDescription {
                    Type = FilterType.Range,
                    Name = "bits_for_cell",
                    VerboseName = "Количество бит на ячейку"
                },
            };
        }
    }
}