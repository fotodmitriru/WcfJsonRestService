using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using WcfJsonRestService.Entities.Enums;

namespace WcfJsonRestService.Entities
{
    [DataContract]
    public class Person
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        public CurrencyTypeEnun CurrencyType { get; set; }

        [DataMember(Name = "currencyType")]
        public string CurrencyTypeString
        {
            get => CurrencyType.ToString();
            set => CurrencyType = (CurrencyTypeEnun) Enum.Parse(typeof(CurrencyTypeEnun), value);
        }

        //[DataMember(Name = "nationalities")]
        //[JsonConverter(typeof(JsonStringEnumConverter))]
        public NationalityTypeEnum[] Nationalities { get; set; }

        [DataMember(Name = "nationalities")]
        public string[] NationalitiesStrings
        {
            get
            {
                List<string> result = new List<string>();
                if (Nationalities == null)
                    return null;

                foreach (NationalityTypeEnum nationality in Nationalities)
                {
                    result.Add(nationality.ToString());
                }

                return result.ToArray();
                /*return Nationalities.GetType().GetEnumValues()
                    .Cast<string>()
                    .Select(x => x.ToString())
                    .ToArray();*/
            }
        }
    }
}