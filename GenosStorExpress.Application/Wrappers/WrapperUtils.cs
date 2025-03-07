// using System.Reflection;
// using GenosStorExpress.Application.Wrappers.Entity.Base;
//
// namespace GenosStorExpress.Application.Wrappers;
//
// public class WrapperUtils {
//     public static E EntityFromWrapper<W, E>(W wrapper) where W: AbstractWrapper {
//         
//         E entity = Activator.CreateInstance<E>();
//         Type entityType = entity!.GetType();
//         
//         var wrapperProperties = wrapper.GetType().GetProperties();
//         foreach (var property in wrapperProperties) {
//             
//             var value = property.GetValue(null);
//             if (value is AbstractWrapper) {
//                 Type valueType = value.GetType();
//                 Type parentEntityType = ((AbstractWrapper) value).Entity;
//                 var conv = WrapperUtils.EntityFromWrapper<typeof(), typeof(AbstractWrapper)>(value)
//                 entityType.GetProperty(property.Name)!.SetValue(
//                     entity,
//                     
//                 );
//             } else {
//                 entityType.GetProperty(property.Name)!.SetValue(entity, value);
//             }
//             
//             // Type _type = Type.GetType("Namespace.AnotherNamespace.ClassName");
//             // PropertyInfo _propertyInfo = _type.GetProperty("Field1");
//             // _propertyInfo.SetValue(_type, _newValue, null);
//         }
//         
//         wrapper.GetType().GetProperties()[0]. .GetProperty(propName).GetValue(src, null);
//     }
// }