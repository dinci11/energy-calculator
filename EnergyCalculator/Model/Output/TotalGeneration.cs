using System.Xml.Serialization;
using EnergyCalculator.Model.Generators;

namespace EnergyCalculator.Model.Output
{
    [XmlRoot("Generator")]
    public class TotalGeneration
    {
        [XmlElement(nameof(Name))]
        public string Name;

        [XmlElement("Total")]
        public double TotalValue;

        public TotalGeneration(GeneratorBase generator, double valueFactor)
        {
            Name = generator.Name;
            TotalValue = generator.TotalValue * valueFactor;
        }

        private TotalGeneration()
        {
        }
    }
}