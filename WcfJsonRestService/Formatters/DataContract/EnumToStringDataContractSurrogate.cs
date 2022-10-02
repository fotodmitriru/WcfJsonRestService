using System;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using WcfJsonRestService.Entities.Enums;

namespace WcfJsonRestService.Formatters.DataContract
{
    /// <summary>Enumerator contract surrogate.</summary>
    internal class EnumToStringDataContractSurrogate : IDataContractSurrogate
    {
        public Type GetDataContractType(Type type) => type == typeof(Enum) ? typeof(string) : type;

        public object GetDeserializedObject(object obj, Type targetType) =>
            targetType.IsEnum
                ? obj == null
                    ? Enum.GetValues(targetType).OfType<int>().FirstOrDefault()
                    : Enum.Parse(targetType, obj.ToString(), ignoreCase: true)
                : obj;

        public object GetObjectToSerialize(object obj, Type targetType)
        {
            if (!(obj is Enum en)) return obj;
            //Console.WriteLine(nameof(GetObjectToSerialize));
            string pair = en.GetEnumDescription();
            return string.IsNullOrEmpty(pair) ? Enum.GetName(obj.GetType(), obj) : en.GetEnumDescription();
        }

        public object GetCustomDataToExport(Type clrType, Type dataContractType) => null;

        public object GetCustomDataToExport(System.Reflection.MemberInfo memberInfo, Type dataContractType) => null;

        public void GetKnownCustomDataTypes(System.Collections.ObjectModel.Collection<Type> customDataTypes)
        {
            //throw new NotImplementedException();
        }

        public Type GetReferencedTypeOnImport(string typeName, string typeNamespace, object customData) => null;

        public System.CodeDom.CodeTypeDeclaration ProcessImportedType(
            System.CodeDom.CodeTypeDeclaration typeDeclaration, System.CodeDom.CodeCompileUnit compileUnit) =>
            typeDeclaration;
    }
}