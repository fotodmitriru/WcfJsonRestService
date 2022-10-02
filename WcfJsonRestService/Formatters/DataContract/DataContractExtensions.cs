using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace WcfJsonRestService.Formatters.DataContract
{
    public static class DataContractExtensions
    {
        #region JSon

        /// <summary>Serializes an object to JSon.</summary>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>Returns a byte array with the serialized object.</returns>
        /// <remarks>This implementation outputs dates in the correct format and enums as string.</remarks>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        public static byte[] SerializeJson(this object obj)
        {
            Console.WriteLine(nameof(SerializeJson));
            using (MemoryStream b = new MemoryStream())
            {
                SerializeJson(obj, b);
                return b.ToArray();
            }
        }

        /// <summary>Serializes an object to JSon.</summary>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="stream">The stream to write to.</param>
        /// <remarks>This implementation outputs dates in the correct format and enums as string.</remarks>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        public static void SerializeJson(this object obj, Stream stream)
        {
            Console.WriteLine(nameof(SerializeJson));
            var settings = new DataContractJsonSerializerSettings
            {
                DateTimeFormat = new DateTimeFormat("yyyy-MM-dd'T'HH:mm:ssZ"),
                DataContractSurrogate = new EnumToStringDataContractSurrogate()
            };

            var type = obj == null ? typeof(object) : obj.GetType();

            var enumerationValue = obj as IEnumerable;

            var fixedValue = enumerationValue != null
                             ? type.IsGenericType && !type.GetGenericArguments()[0].IsInterface
                                ? enumerationValue.ToArray(type.GetGenericArguments()[0])
                                : enumerationValue.OfType<object>().ToArray()
                             : obj;

            if (enumerationValue != null && (!type.IsGenericType || (type.IsGenericType || type.GetGenericArguments()[0].IsInterface)))
            {
                var firstMember = (fixedValue as System.Collections.IEnumerable).OfType<object>().FirstOrDefault();
                if (firstMember != null)
                    fixedValue = enumerationValue.ToArray(firstMember.GetType());
            }

            var fixedType = obj == null
                            ? type
                            : fixedValue.GetType();

            var jsonSer = new DataContractJsonSerializer(fixedType, settings);
            jsonSer.WriteObject(stream, fixedValue ?? throw new InvalidOperationException($"{nameof(fixedValue)} is null!"));
        }

        /// <summary>
        /// Deserializes an object.
        /// </summary>
        /// <typeparam name="T">The output type of the object.</typeparam>
        /// <param name="data">The serialized contents.</param>
        /// <returns>Returns the typed deserialized object.</returns>
        /// <remarks>This implementation outputs dates in the correct format and enums as string.</remarks>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "JSon")]
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        public static T DeserializeJSon<T>(this byte[] data)
        {
            using (MemoryStream b = new MemoryStream(data))
                return DeserializeJSon<T>(b);
        }

        /// <summary>Deserializes a JSon object.</summary>
        /// <typeparam name="T">The output type of the object.</typeparam>
        /// <param name="stream">The stream to read from.</param>
        /// <returns>Returns the typed object.</returns>
        /// <remarks>This implementation outputs dates in the correct format and enums as string.</remarks>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "JSon")]
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        public static T DeserializeJSon<T>(this Stream stream)
        {
            var settings = new DataContractJsonSerializerSettings
            {
                DateTimeFormat = new DateTimeFormat("yyyy-MM-dd'T'HH:mm:ssZ"),
                DataContractSurrogate = new EnumToStringDataContractSurrogate()
            };

            var jsonSer = new DataContractJsonSerializer(typeof(T), settings);
            return (T)jsonSer.ReadObject(stream);
        }

        /// <summary>Deserializes a JSon object.</summary>
        /// <param name="data">The serialized contents.</param>
        /// <param name="targetType">The target type.</param>
        /// <returns>Returns the typed deserialized object.</returns>
        /// <remarks>This implementation outputs dates in the correct format and enums as string.</remarks>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "JSon")]
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        public static object DeserializeJSon(this byte[] data, Type targetType)
        {
            using (MemoryStream b = new MemoryStream(data))
            {
                return DeserializeJSon(b, targetType);
            }
        }

        /// <summary>Deserializes a JSon object.</summary>
        /// <param name="data">The serialized contents.</param>
        /// <param name="targetType">The target type.</param>
        /// <returns>Returns the typed deserialized object.</returns>
        /// <remarks>This implementation outputs dates in the correct format and enums as string.</remarks>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "JSon")]
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        public static object DeserializeJSon(this Stream data, Type targetType)
        {
            Console.WriteLine(nameof(DeserializeJSon));
            var settings = new DataContractJsonSerializerSettings
            {
                DateTimeFormat = new DateTimeFormat("yyyy-MM-dd'T'HH:mm:ssZ"),
                DataContractSurrogate = new EnumToStringDataContractSurrogate()
            };

            var jsonSer = new DataContractJsonSerializer(targetType, settings);
            return jsonSer.ReadObject(data);
        }

        #endregion


        /// <summary>Creates an array from a non generic source.</summary>
        /// <param name="source">The source.</param>
        /// <param name="type">The target type of the array.</param>
        /// <returns>Returns a typed array.</returns>
        public static Array ToArray(this IEnumerable source, Type type)
        {
            var param = Expression.Parameter(typeof(IEnumerable), "source");
            var cast = Expression.Call(typeof(Enumerable), "Cast", new[] { type }, param);
            var toArray = Expression.Call(typeof(Enumerable), "ToArray", new[] { type }, cast);
            var lambda = Expression.Lambda<Func<IEnumerable, Array>>(toArray, param).Compile();

            return lambda(source);
        }
    }
}
