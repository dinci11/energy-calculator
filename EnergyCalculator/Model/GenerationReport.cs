using System.Xml.Serialization;
using EnergyCalculator.Model.Generators;

namespace EnergyCalculator.Model
{
    [XmlRoot(nameof(GenerationReport))]
    public class GenerationReport
    {
        [XmlArray("Wind")]
        [XmlArrayItem(nameof(WindGenerator))]
        public List<WindGenerator> WindGenerators { get; set; }

        [XmlArray("Gas")]
        [XmlArrayItem(nameof(GasGenerator))]
        public List<GasGenerator> GasGenerators { get; set; }

        [XmlArray("Coal")]
        [XmlArrayItem(nameof(CoalGenerator))]
        public List<CoalGenerator> CoalsGenerators { get; set; }
    }
}