using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Shared.Extensions
{
    public static class ReflectionExtensions
    {
        private const BindingFlags _bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance;
        private static readonly ConcurrentDictionary<Type, PropertyInfo[]> s_TypeCache = new ConcurrentDictionary<Type, PropertyInfo[]>();
        private static PropertyInfo[] GetProperties<T>(this T source)
        {
            return source.GetType().GetTypeProperties();
        }

        private static PropertyInfo[] GetTypeProperties(this Type type)
        {
            PropertyInfo[] properties;
            //check if we know of this type?
            if (!s_TypeCache.TryGetValue(type, out properties))
            {
                //nope new type needs to reflect on the object.
                properties = type.GetProperties(_bindingFlags);
                //do not cache generic types
                if (!type.IsGenericType)
                {
                    //add to cache; improve perf by only reflect once.
                    s_TypeCache.TryAdd(type, properties);
                }
            }

            return properties;
        }

        private static string GetTypeFriendlyName(this Type type)
        {
            StringBuilder sb = StringBuilderPool.Get();
            sb.Append(type.Namespace);
            sb.Append(".");
            sb.Append(type.Name);

            if (type.IsGenericType)
            {
                //generic object type extract the details on types
                Type[] arguments = type.GenericTypeArguments;
                sb.Append("[");

                for (int i = 0; i < arguments.Length; i++)
                {
                    if (i > 0)
                    {
                        sb.Append(",");
                    }
                    Type argument = arguments[i];
                    sb.Append("[");
                    sb.Append(argument.Namespace);
                    sb.Append(".");
                    sb.Append(argument.Name);
                    sb.Append("]");
                }//end for

                sb.Append("]");
            }

            return sb.GetStringAndRelease();
        }

        /// <summary>
        /// Transform an object's root properties to IDictionary<string, object> will reflect only on non-static public properties
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IDictionary<string, object> AsDictionary<T>(this T source) where T : class
        {
            if (source == null)
            {
                //is null
                return new Dictionary<string, object>();
            }

            if (source is IDictionary<string, object> castDictionary)
            {
                return castDictionary;
            }

            PropertyInfo[] properties = source.GetProperties();
            int length = properties.Length;
            var dictionary = new Dictionary<string, object>(length)
            {
                //Add type name as field.
                { "___type", typeof(T).GetTypeFriendlyName() }
            };
            PropertyInfo property;
            //reflection on object
            for (var i = 0; i < length; i++)
            {
                property = properties[i];
                if (property.CanRead)
                {
                    try
                    {
                        //only reflect root properties.
                        dictionary.Add(property.Name, property.GetValue(source, null));
                    }
                    catch(Exception ex)
                    {
                        dictionary.Add("ERROR_" + property.Name, ex.Message);
                    }
                }
            }

            return dictionary;
        }

    }
}
