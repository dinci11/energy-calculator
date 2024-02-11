using EnergyCalculator.Model.Generators;

namespace EnergyCalculator.Configuration
{
    public class GeneratorFactorMapping
    {
        private readonly IReferenceData _referenceData;
        private readonly IReadOnlyDictionary<Type, double> _emissionMapping;

        public GeneratorFactorMapping(IReferenceData referenceData)
        {
            _referenceData = referenceData;
            _emissionMapping = new Dictionary<Type, double>()
            {
                {typeof(GasGenerator), _referenceData.EmissionsFactor.Medium },
                {typeof(CoalGenerator), referenceData.EmissionsFactor.High },
                { typeof(WindGenerator), 1.0 }
            };
        }

        public double GetEmissionFactor(GeneratorBase generator) => _emissionMapping[generator.GetType()];

        public double GetValueFactor(GeneratorBase generator)
        {
            if (generator is WindGenerator windGenerator)
            {
                return IsOffshore(windGenerator) ? _referenceData.ValueFactor.Low : _referenceData.ValueFactor.High;
            }

            return _referenceData.ValueFactor.Medium;
        }

        private static bool IsOffshore(WindGenerator windGenerator)
        {
            return windGenerator.Location == Enums.GeneratorLocation.Offshore;
        }
    }
}