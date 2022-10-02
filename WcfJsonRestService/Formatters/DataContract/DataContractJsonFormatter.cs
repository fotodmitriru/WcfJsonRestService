using System;
using System.IO;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace WcfJsonRestService.Formatters.DataContract
{
    /// <summary>Custom implementation of DataContract formatter.</summary>
    public class DataContractJsonFormatter : MediaTypeFormatter
    {

        /// <summary>Creates a new instance.</summary>
        public DataContractJsonFormatter()
        {
            Console.WriteLine(nameof(DataContractJsonFormatter));
            SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
        }

        /// <summary>Gets if the formatter the write a given type.</summary>
        /// <param name="type">The type to handle.</param>
        /// <returns>Returns if the formatter the write a given type.</returns>
        public override bool CanWriteType(Type type)
        {
            return true;
        }

        /// <summary>Gets if the formatter the read a given type.</summary>
        /// <param name="type">The type to handle.</param>
        /// <returns>Returns if the formatter the read a given type.</returns>
        public override bool CanReadType(Type type)
        {
            return true;
        }

        /// <summary>Deserializes an object.</summary>
        /// <param name="type">The target type.</param>
        /// <param name="readStream">The stream to read from.</param>
        /// <param name="content">The http content.</param>
        /// <param name="formatterLogger">A logger.</param>
        /// <returns>Returns the deserialized object.</returns>
        public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, System.Net.Http.HttpContent content, IFormatterLogger formatterLogger)
        {
            Console.WriteLine(nameof(ReadFromStreamAsync));
            var task = Task<object>.Factory.StartNew(() => readStream.DeserializeJSon(type));

            return task;
        }

        /// <summary>Serializes an object.</summary>
        /// <param name="type">The target type.</param>
        /// <param name="value">The object to serialize.</param>
        /// <param name="writeStream">The stream to write to.</param>
        /// <param name="content">The http content.</param>
        /// <param name="transportContext">The context.</param>
        /// <returns>Returns the deserialized object.</returns>
        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, System.Net.Http.HttpContent content, System.Net.TransportContext transportContext)
        {
            Console.WriteLine(nameof(WriteToStreamAsync));
            var task = Task.Factory.StartNew(() =>
            {
                value.SerializeJson(writeStream);
            });

            return task;
        }
    }
}