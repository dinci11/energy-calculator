using System.Xml.Serialization;
using EnergyCalculator.Model.Output;

namespace EnergyCalculator.Model
{
    public class GenerationOutput
    {
        [XmlArray(nameof(Totals))]
        [XmlArrayItem("Generator")]
        public List<TotalGeneration> Totals { get; set; }

        [XmlArray(nameof(MaxEmissionGenerators))]
        [XmlArrayItem("Day")]
        public List<MaxEmission> MaxEmissionGenerators { get; set; }

        [XmlArray(nameof(ActualHeatRates))]
        [XmlArrayItem(nameof(ActualHeatRate))]
        public List<ActualHeatRate> ActualHeatRates { get; set; }
    }
}