using System.Runtime.Serialization;

namespace WcfJsonRestService.Entities.Enums
{
    [DataContract]
    public enum NationalityTypeEnum
    {
        [EnumMember(Value = "RUS")]
        Rus,

        [EnumMember(Value = "MDL")]
        Mdl,

        [EnumMember(Value = "KZ")]
        Kz
    }
}
