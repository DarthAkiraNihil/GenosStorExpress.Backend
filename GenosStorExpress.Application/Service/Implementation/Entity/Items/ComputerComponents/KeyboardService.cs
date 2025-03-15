using GenosStorExpress.Application.Service.Implementation.Base;
using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Service.Interface.Entity.Items.Characteristics;
using GenosStorExpress.Application.Service.Interface.Entity.Items.ComputerComponents;
using GenosStorExpress.Application.Wrappers.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Entity.Item.ComputerComponent;
using GenosStorExpress.Domain.Interface;
using GenosStorExpress.Domain.Interface.Item.ComputerComponent;

namespace GenosStorExpress.Application.Service.Implementation.Entity.Items.ComputerComponents {
    public class KeyboardService: AbstractComputerComponentService, IKeyboardService {
        private readonly IGenosStorExpressRepositories _repositories;
        private readonly IKeyboardRepository _keyboards;
        private readonly IKeyboardTypesizeService _keyboardTypesizes;
        private readonly IKeyboardTypeService _keyboardTypes;

        public KeyboardService(IItemTypeService itemTypeService, IVendorService vendorService, IGenosStorExpressRepositories repositories, IKeyboardTypesizeService keyboardTypesizes, IKeyboardTypeService keyboardTypes) : base(itemTypeService, vendorService) {
            _repositories = repositories;
            _keyboardTypesizes = keyboardTypesizes;
            _keyboardTypes = keyboardTypes;
            _keyboards = _repositories.Items.ComputerComponents.Keyboards;
        }

        public void Create(KeyboardWrapper item) {
            
            var created = new Keyboard();
            _setEntityPropertiesFromWrapper(created, item);
            
            created.HasRGBLighting = item.HasRGBLighting;
            created.IsWireless = item.IsWireless;
            
            var keyboardType = _keyboardTypes.GetEntityFromString(item.Type);
            if (keyboardType == null) {
                throw new NullReferenceException($"Типа клавиатуры {item.Type} не существует");
            }
            created.KeyboardType = keyboardType;
            
            var typesize =  _keyboardTypesizes.GetEntityFromString(item.Typesize);
            if (typesize == null) {
                throw new NullReferenceException($"Типоразмера клавиатуры {item.Typesize} не существует");
            }
            created.Typesize = typesize;
            
            _keyboards.Create(created);
        }

        public KeyboardWrapper? Get(int id) {
            Keyboard? obj = _keyboards.Get(id);
            if (obj == null) {
                return null;
            }
            var wrapped = new KeyboardWrapper();
            
            _setWrapperPropertiesFromEntity(obj, wrapped);
            
            wrapped.HasRGBLighting = obj.HasRGBLighting;
            wrapped.IsWireless = obj.IsWireless;
            wrapped.Type = obj.KeyboardType.Name;
            wrapped.Typesize = obj.Typesize.Name;
            
            return wrapped;
        }

        public List<KeyboardWrapper> List() {
            return _keyboards.List().Select(obj => {
                var wrapped = new KeyboardWrapper();
            
                _setWrapperPropertiesFromEntity(obj, wrapped);
            
                wrapped.HasRGBLighting = obj.HasRGBLighting;
                wrapped.IsWireless = obj.IsWireless;
                wrapped.Type = obj.KeyboardType.Name;
                wrapped.Typesize = obj.Typesize.Name;
            
                return wrapped;
            }).ToList();
        }

        public void Update(int id, KeyboardWrapper item) {
            var obj = _keyboards.Get(id);
            if (obj == null) {
                throw new NullReferenceException($"Клавиатуры с номером {id} не существует");
            }
            _setEntityPropertiesFromWrapper(obj, item);
            
            obj.HasRGBLighting = item.HasRGBLighting;
            obj.IsWireless = item.IsWireless;
            
            var keyboardType = _keyboardTypes.GetEntityFromString(item.Type);
            if (keyboardType == null) {
                throw new NullReferenceException($"Типа клавиатуры {item.Type} не существует");
            }
            obj.KeyboardType = keyboardType;
            
            var typesize =  _keyboardTypesizes.GetEntityFromString(item.Typesize);
            if (typesize == null) {
                throw new NullReferenceException($"Типоразмера клавиатуры {item.Typesize} не существует");
            }
            obj.Typesize = typesize;
            
            _keyboards.Update(obj);
        }

        public void Delete(int id) {
            _keyboards.Delete(id);
        }

        public int Save() {
            return _repositories.Save();
        }
        
        public List<KeyboardWrapper> Filter(List<Func<KeyboardWrapper, bool>> filters) {
            var result = List();
            foreach (var filter in filters) {
                result = result.Where(filter).ToList();
            }
            return result;
        }
    }
}