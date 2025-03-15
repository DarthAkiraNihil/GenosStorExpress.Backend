using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Wrappers.Enum;
using GenosStorExpress.Domain.Entity.Item;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items {
    public class ItemTypesService: IItemTypeService {
        
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly IItemTypeRepository _itemTypes;

        public ItemTypesService(IGenosStorExpressRepositories repositories) {
            _repositories = repositories;
            _itemTypes = _repositories.Items.ItemTypes;
        }

        public void Create(string item) {
            var created = new ItemType { Name = item };
            _itemTypes.Create(created);
        }

        public string Get(int id) {
            return _itemTypes.Get(id).Name;
        }

        public List<string> List() {
            return _itemTypes.List().Select(c => c.Name).ToList();
        }

        public void Update(int id, string item) {
            ItemType obj = _itemTypes.Get(id);
            obj.Name = item;
            _itemTypes.Update(obj);
        }

        public void Delete(int id) {
            _itemTypes.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }

        public bool BelongsToEnum(string value) {
            return _itemTypes.List().Exists(c => c.Name == value);
        }

        public ItemType GetEntityFromString(string value) {
            return _itemTypes.List().FirstOrDefault(c => c.Name == value, null);
        }

        public ItemTypeDescriptor GetDescriptor(string name) {
            switch (name) {
                case "computer_case": {
                    return ItemTypeDescriptor.ComputerCase;
                }
                case "cpu_cooler": {
                    return ItemTypeDescriptor.CPUCooler;
                }
                case "cpu": {
                    return ItemTypeDescriptor.CPU;
                }
                case "display": {
                    return ItemTypeDescriptor.Display;
                }
                case "graphics_card": {
                    return ItemTypeDescriptor.GraphicsCard;
                }
                case "hdd": {
                    return ItemTypeDescriptor.HDD;
                }
                case "keyboard": {
                    return ItemTypeDescriptor.Keyboard;
                }
                case "motherboard": {
                    return ItemTypeDescriptor.Motherboard;
                }
                case "mouse": {
                    return ItemTypeDescriptor.Mouse;
                }
                case "nvme_ssd": {
                    return ItemTypeDescriptor.NVMeSSD;
                }
                case "power_supply": {
                    return ItemTypeDescriptor.PowerSupply;
                }
                case "ram": {
                    return ItemTypeDescriptor.RAM;
                }
                case "sata_ssd": {
                    return ItemTypeDescriptor.SataSSD;
                }
                case "prepared_assembly": {
                    return ItemTypeDescriptor.PreparedAssembly;
                }
                default: {
                    return ItemTypeDescriptor.Unknown;
                }
            }
        }
    }
}