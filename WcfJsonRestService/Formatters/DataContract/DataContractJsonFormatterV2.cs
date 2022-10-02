using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace WcfJsonRestService.Formatters.DataContract
{
    public class DataContractJsonFormatterV2: IExtensibleDataObject

    {
        public void DeserializeRequest(Message message, object[] parameters)
        {
            throw new NotImplementedException();
        }

        public Message SerializeReply(MessageVersion messageVersion, object[] parameters, object result)
        {
            throw new NotImplementedException();
        }

        public ExtensionDataObject ExtensionData { get; set; }
    }
}
