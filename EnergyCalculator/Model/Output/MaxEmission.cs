using System.Xml.Serialization;

namespace EnergyCalculator.Model.Output
{
    public class MaxEmission
    {
        [XmlElement("Name")]
        public string GeneratorName;

        public DateTime Date;

        public double Emission;
    }
}