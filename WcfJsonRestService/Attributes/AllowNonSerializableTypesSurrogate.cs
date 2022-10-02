using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using WcfJsonRestService.Entities;

namespace WcfJsonRestService.Attributes
{
    class AllowNonSerializableTypesSurrogate : IDataContractSurrogate
    {
        #region IDataContractSurrogate Members

        public Type GetDataContractType(Type type)
        {
            Console.WriteLine("TEST!!!");
            //throw new InvalidOperationException("TEST!!!!!");
            // "Person" will be serialized as "PersonSurrogated"
            // This method is called during serialization and schema export
            return typeof(Person).IsAssignableFrom(type) ? typeof(PersonSurrogated) : type;
        }

        public object GetObjectToSerialize(object obj, Type targetType)
        {
            Console.WriteLine("TEST!!!");
            //throw new InvalidOperationException("TEST!!!!!");
            //This method is called on serialization.
            //If we're serializing Person,...
            if (!(obj is Person person)) return obj;
            PersonSurrogated personSurrogated = new PersonSurrogated
            {
                FirstName = person.Name,
                LastName = person.Id.ToString(),
                Age = person.Id
            };
            return personSurrogated;
        }

        public object GetDeserializedObject(object obj, Type targetType)
        {
            Console.WriteLine("TEST!!!");
            //throw new InvalidOperationException("TEST!!!!!");
            //This method is called on deserialization.
            //If we're deserializing PersonSurrogated,...
            if (!(obj is PersonSurrogated personSurrogated)) return obj;
            Person person = new Person
            {
                Name = personSurrogated.FirstName
            };
            return person;
        }

        public Type GetReferencedTypeOnImport(string typeName, string typeNamespace, object customData)
        {
            Console.WriteLine("TEST!!!");
            //throw new InvalidOperationException("TEST!!!!!");
            // This method is called on schema import.

            // What we say here is that if we see a PersonSurrogated data contract in the specified namespace,
            // we should not create a new type for it since we already have an existing type, "Person".
            if (typeNamespace.Equals("http://schemas.datacontract.org/2004/07/DCSurrogateSample"))
            {
                if (typeName.Equals("PersonSurrogated"))
                {
                    return typeof(Person);
                }
            }
            return null;
        }

        public System.CodeDom.CodeTypeDeclaration ProcessImportedType(System.CodeDom.CodeTypeDeclaration typeDeclaration, System.CodeDom.CodeCompileUnit compileUnit)
        {
            Console.WriteLine("TEST!!!");
            //throw new InvalidOperationException("TEST!!!!!");
            // Not used in this sample.
            // We could use this method to construct an entirely new CLR type when a certain type is imported.
            return typeDeclaration;
        }

        public object GetCustomDataToExport(Type clrType, Type dataContractType)
        {
            Console.WriteLine("TEST!!!");
            // Not used in this sample
            return null;
        }

        public object GetCustomDataToExport(System.Reflection.MemberInfo memberInfo, Type dataContractType)
        {
            Console.WriteLine("TEST!!!");
            //throw new InvalidOperationException("TEST!!!!!");
            // Not used in this sample
            return null;
        }

        public void GetKnownCustomDataTypes(Collection<Type> customDataTypes)
        {
            Console.WriteLine("TEST!!!");
            //throw new InvalidOperationException("TEST!!!!!");
            // Not used in this sample
        }

        #endregion
    }

}
