using System.Xml.Serialization;

namespace EnergyCalculator.Model.Generators
{
    public abstract class GeneratorBase
    {
        [XmlElement(nameof(Name))]
        public string Name { get; set; }

        [XmlArray(nameof(Generation))]
        [XmlArrayItem("Day")]
        public List<Generation> Generations { get; set; }
    }
}