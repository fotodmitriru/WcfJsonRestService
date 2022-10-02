using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json;

namespace WcfJsonRestService
{
    public static class Extensions
    {
        /// <summary>
        /// Сериализует текущий объект в строку формата JSON
        /// </summary>
        /// <returns>строка в формате JSON</returns>
        public static string SerializeToJson2<T>(this T objectForSer)
        {
            if (objectForSer == null)
                throw new ArgumentNullException(nameof(objectForSer));

            var options = new JsonSerializerOptions
            {
                WriteIndented = false,
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Serialize(objectForSer, options);
        }

        /// <summary>
        /// Десериализует объект из строки в формате JSON
        /// </summary>
        public static T DeserializeFromJson2<T>(this T objectForDeser, string strJson)
        {
            if (string.IsNullOrEmpty(strJson))
                throw new ArgumentNullException(nameof(strJson));

            return JsonSerializer.Deserialize<T>(strJson);
        }

        public static string GetEnumDescription<TEnum>(this TEnum item)
            => item.GetType()
                   .GetField(item.ToString())
                   .GetCustomAttributes(typeof(EnumMemberAttribute), false)
                   .Cast<EnumMemberAttribute>()
                   .FirstOrDefault()?.Value ?? string.Empty;

        /*public static string[] GetValues<T[]>(this T[] thisEnum) =>Enum.GetValues(thisEnum)
        .Cast<int>()
        .Select(x => x.ToString())
            .ToArray();*/
    }
}
