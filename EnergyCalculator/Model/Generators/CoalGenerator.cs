using System.Xml.Serialization;

namespace EnergyCalculator.Model.Generators
{
    public class CoalGenerator : PollutingGenerator
    {
        [XmlElement(nameof(TotalHeatInput))]
        public double TotalHeatInput { get; set; }

        [XmlElement(nameof(ActualNetGeneration))]
        public double ActualNetGeneration { get; set; }
    }
}