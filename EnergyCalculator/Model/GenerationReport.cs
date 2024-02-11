using System.Xml;
using System.Xml.Serialization;
using EnergyCalculator.Model.Generators;

namespace EnergyCalculator.Model
{
    [XmlRoot(nameof(GenerationReport))]
    public class GenerationReport
    {
        [XmlElement(typeof(WindGenerator), ElementName = "Wind")]
        [XmlElement(typeof(GasGenerator), ElementName = "Gas")]
        [XmlElement(typeof(CoalGenerator), ElementName = "Coal")]
        public List<GeneratorBase> Generators { get; set; }
    }
}