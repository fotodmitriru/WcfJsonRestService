using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Text.Json;
using WcfJsonRestService.Entities;

namespace WcfJsonRestService.Formatters.Json
{
    public class JsonDispatchFormatter : IDispatchMessageFormatter
    {
        /*public void DeserializeRequest(Message message, object[] parameters)
        {
            Console.WriteLine(nameof(DeserializeRequest));
        }

        public Message SerializeReply(MessageVersion messageVersion, object[] parameters, object result)
        {
            Console.WriteLine(nameof(SerializeReply));
            return null;
        }*/

        private readonly OperationDescription _operation;
        private readonly Dictionary<string, int> _parameterNames;

        public JsonDispatchFormatter(OperationDescription operation, bool isRequest)
        {
            _operation = operation;
            if (!isRequest) return;
            var operationParameterCount = operation.Messages[0].Body.Parts.Count;
            if (operationParameterCount <= 1) return;
            _parameterNames = new Dictionary<string, int>();
            for (var i = 0; i < operationParameterCount; i++)
            {
                _parameterNames.Add(operation.Messages[0].Body.Parts[i].Name, i);
            }
        }

        public void DeserializeRequest(Message message, object[] parameters)
        {
            /*object bodyFormatProperty;
            if (!message.Properties.TryGetValue(WebBodyFormatMessageProperty.Name, out bodyFormatProperty) ||
                 (bodyFormatProperty as WebBodyFormatMessageProperty).Format != WebContentFormat.Raw)
            {
                throw new InvalidOperationException("Incoming messages must have a body format of Raw. Is a ContentTypeMapper set on the WebHttpBinding?");
            }*/

            /*var bodyReader = message.GetReaderAtBodyContents();
            bodyReader.ReadStartElement("Binary");
            var rawBody = bodyReader.ReadContentAsBase64();
            var ms = new MemoryStream(rawBody);

            var serializer = new DataContractJsonSerializer(_operation.Messages[0].Body.Parts[0].Type,
                new DataContractJsonSerializerSettings
                {
                    DateTimeFormat = new DateTimeFormat("yyyy-MM-dd'T'HH:mm:ssZ")
                });

            if (parameters.Length == 1)
            {
                // single parameter, assuming bare
                serializer.ReadObject(ms);
                ms.Position = 0;
            }
            
            ms.Close();*/
        }

        public Message SerializeReply(MessageVersion messageVersion, object[] parameters, object result)
        {
            //Console.WriteLine("HELLO");
            byte[] body;
            //var serializer = new DataContractJsonSerializer(_operation.Messages[0].Body.Parts[0].Type,
            var serializer = new DataContractJsonSerializer(result.GetType(),
                new DataContractJsonSerializerSettings
                {
                    DateTimeFormat = new DateTimeFormat("yyyy-MM-dd'T'HH:mm:ssZ")
                });
            
            using (var ms = new MemoryStream())
            {
                serializer.WriteObject(ms, result);
                body = ms.ToArray();
            }
            Console.WriteLine("HELLO");
            var replyMessage = Message.CreateMessage(messageVersion, _operation.Messages[1].Action, new RawBodyWriter(body));
            replyMessage.Properties.Add(WebBodyFormatMessageProperty.Name, new WebBodyFormatMessageProperty(WebContentFormat.Raw));
            var respProp = new HttpResponseMessageProperty();
            respProp.Headers[HttpResponseHeader.ContentType] = "application/json";
            replyMessage.Properties.Add(HttpResponseMessageProperty.Name, respProp);
            return replyMessage;

        }

    }
}