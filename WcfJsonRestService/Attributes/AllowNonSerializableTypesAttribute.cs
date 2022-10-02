using System;
using System.ServiceModel.Description;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using WcfJsonRestService.Formatters.DataContract;
using WcfJsonRestService.Formatters.Json;

namespace WcfJsonRestService.Attributes
{
    public sealed class AllowNonSerializableTypesAttribute : Attribute, IContractBehavior, IOperationBehavior, IWsdlExportExtension
    {
        #region IContractBehavior Members

        public void AddBindingParameters(ContractDescription description, ServiceEndpoint endpoint, BindingParameterCollection parameters)
        {
        }

        public void ApplyClientBehavior(ContractDescription description, ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime proxy)
        {
            Console.WriteLine(nameof(ApplyClientBehavior));
            foreach (OperationDescription opDesc in description.Operations)
            {
                ApplyDataContractSurrogate(opDesc);
            }
        }

        public void ApplyDispatchBehavior(ContractDescription description, ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.DispatchRuntime dispatch)
        {
            Console.WriteLine(nameof(ApplyDispatchBehavior));
            foreach (OperationDescription opDesc in description.Operations)
            {
                opDesc.Behaviors.Add(new TestMyBehavior());
                ApplyDataContractSurrogate(opDesc);
            }
        }

        public void Validate(ContractDescription description, ServiceEndpoint endpoint)
        {
        }

        #endregion

        #region IWsdlExportExtension Members

        public void ExportContract(WsdlExporter exporter, WsdlContractConversionContext context)
        {
            if (exporter == null)
                throw new ArgumentNullException(nameof(exporter));

            XsdDataContractExporter xsdDcExporter;
            if (!exporter.State.TryGetValue(typeof(XsdDataContractExporter), out object dataContractExporter))
            {
                xsdDcExporter = new XsdDataContractExporter(exporter.GeneratedXmlSchemas);
                exporter.State.Add(typeof(XsdDataContractExporter), xsdDcExporter);
            }
            else
            {
                xsdDcExporter = (XsdDataContractExporter)dataContractExporter;
            }
            if (xsdDcExporter.Options == null)
                xsdDcExporter.Options = new ExportOptions();

            if (xsdDcExporter.Options.DataContractSurrogate == null)
                //xsdDcExporter.Options.DataContractSurrogate = new AllowNonSerializableTypesSurrogate();
                xsdDcExporter.Options.DataContractSurrogate = new EnumToStringDataContractSurrogate();
        }

        public void ExportEndpoint(WsdlExporter exporter, WsdlEndpointConversionContext context)
        {
        }

        #endregion

        #region IOperationBehavior Members

        public void AddBindingParameters(OperationDescription description, BindingParameterCollection parameters)
        {
        }

        public void ApplyClientBehavior(OperationDescription description, System.ServiceModel.Dispatcher.ClientOperation proxy)
        {
            Console.WriteLine("hello");
            ApplyDataContractSurrogate(description);
        }

        public void ApplyDispatchBehavior(OperationDescription description, System.ServiceModel.Dispatcher.DispatchOperation dispatch)
        {
            //dispatch.Formatter = new JsonDispatchFormatter(); //NOTE: подключение своего форматтера
            Console.WriteLine(dispatch.Formatter == null);
            ApplyDataContractSurrogate(description);
        }

        public void Validate(OperationDescription description)
        {
        }

        #endregion

        private static void ApplyDataContractSurrogate(OperationDescription description)
        {
            DataContractSerializerOperationBehavior dcsOperationBehavior = description.Behaviors.Find<DataContractSerializerOperationBehavior>();
            if (dcsOperationBehavior == null) return;
            if (dcsOperationBehavior.DataContractSurrogate == null)
                //dcsOperationBehavior.DataContractSurrogate = new AllowNonSerializableTypesSurrogate();
                dcsOperationBehavior.DataContractSurrogate = new EnumToStringDataContractSurrogate();
        }

    }

    public class TestMyBehavior : IOperationBehavior
    {
        public void Validate(OperationDescription operationDescription)
        {
            //throw new NotImplementedException();
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            Console.WriteLine(dispatchOperation.Formatter == null);
            Console.WriteLine(dispatchOperation.Formatter?.GetType());
            dispatchOperation.Formatter = new JsonDispatchFormatter(operationDescription, true);
            Console.WriteLine(dispatchOperation.Formatter?.GetType());

        }

        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
            //throw new NotImplementedException();
        }

        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {
            //throw new NotImplementedException();
        }
    }
}