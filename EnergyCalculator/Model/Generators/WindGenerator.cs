using System.Xml.Serialization;
using EnergyCalculator.Enums;

namespace EnergyCalculator.Model.Generators
{
    public class WindGenerator : GeneratorBase
    {
        [XmlElement(nameof(Location))]
        public GeneratorLocation Location { get; set; }
    }
}