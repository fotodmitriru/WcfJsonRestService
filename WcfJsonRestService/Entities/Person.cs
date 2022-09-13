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
            get => Nationalities?.Select(nationality => nationality.ToString()).ToArray();
            set => Nationalities = value.Select(nationality =>
                Enum.Parse(typeof(NationalityTypeEnum), nationality)).Cast<NationalityTypeEnum>().ToArray();
        }
    }
}