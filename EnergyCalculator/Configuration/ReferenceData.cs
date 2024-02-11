using System.Xml.Serialization;

namespace EnergyCalculator.Configuration
{
    [XmlRoot("Factors")]
    public class ReferenceData : IReferenceData
    {
        [XmlElement(nameof(ValueFactor))]
        public Factor ValueFactor { get; set; }

        [XmlElement(nameof(EmissionsFactor))]
        public Factor EmissionsFactor { get; set; }
    }

    public class Factor : IFactor
    {
        public double High { get; set; }
        public double Medium { get; set; }
        public double Low { get; set; }
    }

    public interface IFactor
    {
        double High { get; }
        public double Medium { get; }
        public double Low { get; }
    }

    public interface IReferenceData
    {
        Factor ValueFactor { get; }
        Factor EmissionsFactor { get; }
    }
}