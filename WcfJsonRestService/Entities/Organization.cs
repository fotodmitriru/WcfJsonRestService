using System.Runtime.Serialization;

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
    }
}
