using System.Xml.Serialization;
using EnergyCalculator.Model.Generators;

namespace EnergyCalculator.Model.Output
{
    public class ActualHeatRate
    {
        [XmlElement("Name")]
        public string GeneratorName;

        public double HeatRate;

        public ActualHeatRate(CoalGenerator generator)
        {
            GeneratorName = generator.Name;
            HeatRate = generator.TotalHeatInput / generator.ActualNetGeneration;
        }

        public ActualHeatRate()
        {
        }
    }
}