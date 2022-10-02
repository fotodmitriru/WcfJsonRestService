using System;
using System.Runtime.Serialization;
using WcfJsonRestService.Attributes;

namespace WcfJsonRestService.Entities
{
    [DataContract]
    public class Organization
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "persons")]
        public Person[] Persons { get; set; }

        [DataMember(Name = "currentDateTime")]
        public DateTime DateTimeNow { get; set; }

        
        public string DateTimeNowString
        {
            get => DateTimeNow.ToString("yyyy-MM-ddTHH:mm:ssz");
            set => DateTimeNow = DateTime.Parse(value);
        }
    }
}