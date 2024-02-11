using System.Xml.Serialization;
using EnergyCalculator.Model.Output;

namespace EnergyCalculator.Model
{
    public class GenerationOutput
    {
        [XmlArray(nameof(Totals))]
        [XmlArrayItem("Generator")]
        public List<TotalGeneration> Totals { get; set; }
    }
}