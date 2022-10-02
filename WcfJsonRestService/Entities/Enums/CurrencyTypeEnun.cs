using System.Runtime.Serialization;

namespace WcfJsonRestService.Entities.Enums
{
    [DataContract]
    public enum CurrencyTypeEnun
    {
        [EnumMember(Value = "RUB")]
        Rub,

        [EnumMember(Value = "RUP")]
        Rup,
        
        [EnumMember(Value = "MDL")]
        Mdl
    }
}