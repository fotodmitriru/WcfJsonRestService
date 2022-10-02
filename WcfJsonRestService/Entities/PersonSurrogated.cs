using System.Runtime.Serialization;

namespace WcfJsonRestService.Entities
{
    [DataContract(Name = "PersonSur")]
    public class PersonSurrogated
    {
        [DataMember]
        public string FirstName;

        [DataMember]
        public string LastName;

        [DataMember]
        public int Age;
    }
}
