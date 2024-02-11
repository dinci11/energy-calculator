using System.Xml;
using System.Xml.Serialization;
using EnergyCalculator.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnergyCalculator.Extensions
{
    public static class ReferenceDataExtensions
    {
        public static void ConfigureReferenceData(this IServiceCollection services, string filePath)
        {
            var referenceData = new ReferenceData();

            using (var reader = XmlReader.Create(filePath))
            {
                reader.MoveToContent();
                reader.ReadToDescendant("Factors");

                var serializer = new XmlSerializer(typeof(ReferenceData));
                referenceData = (ReferenceData)serializer.Deserialize(reader);
            }

            services.AddSingleton<IReferenceData>(referenceData);
        }
    }
}