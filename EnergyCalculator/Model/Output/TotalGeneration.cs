using System.Xml.Serialization;
using EnergyCalculator.Model.Generators;

namespace EnergyCalculator.Model.Output
{
    public class TotalGeneration
    {
        private GeneratorBase Generator { get; set; }

        private readonly double ValueFactor;

        private readonly double EmissionFactor;

        [XmlElement(nameof(Name))]
        public string Name => Generator.Name;

        [XmlElement("Total")]
        public double TotalValue => Generator.TotalValue * ValueFactor;

        public TotalGeneration(GeneratorBase generator, double valueFactor, double emissionFactor)
        {
            Generator = generator;
            ValueFactor = valueFactor;
            EmissionFactor = emissionFactor;
        }

        private TotalGeneration()
        {
        }
    }
}