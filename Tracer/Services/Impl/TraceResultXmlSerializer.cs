using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Tracer.Entities;

namespace Tracer.Services.Impl
{
    public class TraceResultXmlSerializer : ISerializer<TraceResult>
    {
        private const string RootElementName = "root";

        public string Serialize(TraceResult data)
        {
            var traces = data.Traces.ToList();
            var xmlSerializer = new XmlSerializer(traces.GetType(), new XmlRootAttribute(RootElementName));

            using var stringWriter = new StringWriter();
            using var writer = new XmlTextWriter(stringWriter);
            writer.Formatting = Formatting.Indented;
            xmlSerializer.Serialize(writer, traces);

            return stringWriter.ToString();
        }
    }
}