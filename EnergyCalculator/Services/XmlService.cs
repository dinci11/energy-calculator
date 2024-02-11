using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
using EnergyCalculator.Services.Interfaces;

namespace EnergyCalculator.Services
{
    public class XmlService : IXmlService
    {
        public T Deserialize<T>(Stream stream) where T : class
        {
            var serializer = new XmlSerializer(typeof(T));

            var t = (T)serializer.Deserialize(stream);

            if (t == null)
            {
                throw new SerializationException($"XML cannot be serialized to {typeof(T)}");
            }

            return t;
        }

        public string SerializeToString<T>(T entity) where T : class
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            using (var stream = new StringWriter())
            {
                using (var writer = XmlWriter.Create(stream))
                {
                    xmlSerializer.Serialize(writer, entity);
                    return stream.ToString();
                }
            }
        }
    }
}