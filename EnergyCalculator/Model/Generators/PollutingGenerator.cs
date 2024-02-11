using System.Xml.Serialization;

namespace EnergyCalculator.Model.Generators
{
    public abstract class PollutingGenerator : GeneratorBase
    {
        [XmlElement(nameof(EmissionsRating))]
        public double EmissionsRating { get; set; }
    }
}