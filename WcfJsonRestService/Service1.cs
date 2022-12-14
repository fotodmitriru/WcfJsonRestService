using System;
using System.Collections.Generic;
using System.ServiceModel.Web;
using WcfJsonRestService.Entities;
using WcfJsonRestService.Entities.Enums;

namespace WcfJsonRestService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде и файле конфигурации.

    public class Service1 : IService1
    {
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "data/{id}")]
        public Person GetData(string id)
        {
            // lookup person with the requested id 
            return new Person()
            {
                Id = Convert.ToInt32(id),
                Name = "Leo Messi",
                CurrencyType = CurrencyTypeEnun.Mdl
            };
        }

        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "data/persons")]
        public List<Person> GetPersons()
        {
            return new List<Person>()
            {
                new Person
                {
                    Id = 1,
                    Name = "Leo Binasi",
                    CurrencyType = CurrencyTypeEnun.Mdl
                },
                new Person
                {
                    Id = 20,
                    Name = "Hrenase",
                    CurrencyType = CurrencyTypeEnun.Rup
                }
            };
        }

        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "data/organization/{id}")]
        public Organization GetOrganization(string id)
        {
            return new Organization
            {
                Id = Convert.ToInt32(id),
                Name = "Moskvich",
                Persons = new[]
                {
                    new Person
                    {
                        Id = 1,
                        Name = "Leo Binasi",
                        CurrencyType = CurrencyTypeEnun.Mdl,
                        Nationalities = new[] {NationalityTypeEnum.Kz, NationalityTypeEnum.Rus}
                    },
                    new Person
                    {
                        Id = 20,
                        Name = "Hrenase",
                        CurrencyType = CurrencyTypeEnun.Rup,
                        Nationalities = new[] {NationalityTypeEnum.Rus, NationalityTypeEnum.Mdl}
                    }
                },
                DateTimeNow = DateTime.Now
            };
        }

        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "data/organization2/{id}")]
        public string GetOrganizationString(string id)
        {
            var org = new Organization
            {
                Id = Convert.ToInt32(id),
                Name = "Moskvich",
                Persons = new[]
                {
                    new Person
                    {
                        Id = 1,
                        Name = "Leo Binasi",
                        CurrencyType = CurrencyTypeEnun.Mdl,
                        Nationalities = new[] {NationalityTypeEnum.Kz, NationalityTypeEnum.Rus}
                    },
                    new Person
                    {
                        Id = 20,
                        Name = "Hrenase",
                        //CurrencyType = CurrencyTypeEnun.RUP
                    }
                }
            };

            return org.SerializeToJson2();
        }
    }
}