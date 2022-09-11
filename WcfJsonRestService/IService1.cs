using System.Collections.Generic;
using System.ServiceModel;
using WcfJsonRestService.Entities;

namespace WcfJsonRestService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IService1" в коде и файле конфигурации.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        Person GetData(string id);

        [OperationContract]
        List<Person> GetPersons();

        [OperationContract]
        Organization GetOrganization(string id);

        [OperationContract]
        string GetOrganizationString(string id);
    }
}
