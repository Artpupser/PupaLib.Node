using System.Reflection;

namespace PupaLib.Node.Utils;

public static class ReflectionUtils {
   public static IEnumerable<PropertyInfo> GetFilteredByAttributeProperties<T>(object obj) where T : Attribute {
      var type = obj.GetType();
      IEnumerable<PropertyInfo> props = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
      props = props.Where(x => x.IsDefined(typeof(T), false));
      return props;
   }
}