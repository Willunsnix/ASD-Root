using System;
using System.ComponentModel;
using System.Reflection;

namespace ASD.Global.Helpers
{
    /// <summary>
    /// Entity attribute processing
    /// To refresh the designer property window add, the [RefreshProperties(RefreshProperties.All)] attribute to the initiating property
    /// </summary>
    public class PropertyHandler
    {
        /// <summary>
        /// Control whether the attribute is read-only through reflection
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <param name="readOnly"></param>
        public static void SetPropertyReadOnly(object obj, string propertyName, bool readOnly)
        {
            Type type = typeof(ReadOnlyAttribute);
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(obj);
            AttributeCollection attrs = props[propertyName].Attributes;
            FieldInfo fld = type.GetField("isReadOnly", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.CreateInstance);
            fld.SetValue(attrs[type], readOnly);
        }

        /// <summary>
        /// Control whether the attribute is visible through reflection
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <param name="visible"></param>
        public static void SetPropertyVisibility(object obj, string propertyName, bool visible)
        {
            Type type = typeof(BrowsableAttribute);
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(obj);
            AttributeCollection attrs = props[propertyName].Attributes;
            FieldInfo fld = type.GetField("browsable", BindingFlags.Instance | BindingFlags.NonPublic);
            fld.SetValue(attrs[type], visible);
        }

        public static string PropertiesToString(object o, char separator)
        {
            string result = string.Empty;

            // Get the type.
            Type type = o.GetType();

            // Get all public instance properties.
            // Use the override if you want to classify
            // which properties to return.
            foreach (PropertyInfo propInfo in type.GetProperties())
            {
                // Do something with the property info.
                result += propInfo.GetValue(o).ToString() + separator;
            }
            return result.TrimEnd(separator);
        }

        public static string PropertiesToString(object o, char separator, bool enclosed)
        {
            string result = string.Empty;

            // Get the type.
            Type type = o.GetType();

            // Get all public instance properties.
            // Use the override if you want to classify
            // which properties to return.
            foreach (PropertyInfo propInfo in type.GetProperties())
            {
                // Do something with the property info.
                result += propInfo.GetValue(o).ToString() + separator;
            }

            result = enclosed ? "[" + result.TrimEnd(separator) + "]" : result.TrimEnd(separator);

            return result;
        }

        public static PropertyDescriptorCollection GetProperties(object obj)
        {
            return TypeDescriptor.GetProperties(obj);
        }

        public static AttributeCollection GetAttributes(PropertyDescriptor prop)
        {
            return prop.Attributes;
        }

        public static void SetPropertyVisibilityByPath(object obj, string propertyPath, bool visible)
        {
            var pathArray = propertyPath.Split('.');

            if (pathArray.Length > 1)
            {
                string[] subArray = new string[pathArray.Length - 1];
                Array.Copy(pathArray, 1, subArray, 0, pathArray.Length - 1);

                Type currentType = obj.GetType();
                PropertyInfo property = currentType.GetProperty(pathArray[0]);
                object subObject = property.GetValue(obj);
                SetPropertyVisibilityByPath(subObject, string.Join(".", subArray), visible);
            }
            else
            {
                Type type = typeof(BrowsableAttribute);
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(obj);

                AttributeCollection attrs = props[pathArray[0]].Attributes;
                FieldInfo fld = type.GetField("browsable", BindingFlags.Instance | BindingFlags.NonPublic);
                fld.SetValue(attrs[type], visible);
            }
        }

    }
}