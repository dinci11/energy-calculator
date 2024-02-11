using EnergyCalculator.Configuration;
using EnergyCalculator.Model;
using EnergyCalculator.Model.Generators;
using EnergyCalculator.Model.Output;
using EnergyCalculator.Model.Report;
using EnergyCalculator.Services.Interfaces;

namespace EnergyCalculator.Services
{
    public class ReportProcessingService : IReportProcessingService
    {
        private readonly GeneratorFactorMapping _mapping;

        public ReportProcessingService(GeneratorFactorMapping mapping)
        {
            _mapping = mapping;
        }

        public async Task<GenerationOutput> ProcessAsync(GenerationReport report)
        {
            var totalGenerations = Task.Run(() => CalculateTotalGenerations(report));

            var maxEmissionGenerators = Task.Run(() => CalculateMaxEmissions(report));

            var actualHeatRates = Task.Run(() => CalculateActualHeatRates(report));

            var output = new GenerationOutput
            {
                Totals = await totalGenerations,
                MaxEmissionGenerators = await maxEmissionGenerators,
                ActualHeatRates = await actualHeatRates
            };

            return output;
        }

        private List<ActualHeatRate> CalculateActualHeatRates(GenerationReport report) => report.CoalsGenerators.Select(g => new ActualHeatRate(g)).ToList();

        private List<MaxEmission> CalculateMaxEmissions(GenerationReport report)
        {
            var maxEmissionForDay = new Dictionary<DateTime, MaxEmission>();

            CalculateMaxEmissionsForGenerators(report.GasGenerators, maxEmissionForDay);
            CalculateMaxEmissionsForGenerators(report.CoalsGenerators, maxEmissionForDay);

            return maxEmissionForDay.Values.ToList();
        }

        private void CalculateMaxEmissionsForGenerators<T>(List<T> generators, Dictionary<DateTime, MaxEmission> maxEmissionForDay)
            where T : PollutingGenerator
        {
            foreach (var generator in generators)
            {
                foreach (var generation in generator.Generations)
                {
                    var emission = CalculateEmission(generator, generation);

                    if (!maxEmissionForDay.ContainsKey(generation.Date)
                        || maxEmissionForDay[generation.Date].Emission < emission)
                    {
                        var maxEmission = new MaxEmission
                        {
                            Date = generation.Date,
                            Emission = emission,
                            GeneratorName = generator.Name
                        };
                        maxEmissionForDay[generation.Date] = maxEmission;
                    }
                }
            }
        }

        private double CalculateEmission(PollutingGenerator generator, Generation generation)
        {
            return generation.Energy * generator.EmissionsRating * _mapping.GetEmissionFactor(generator);
        }

        private List<TotalGeneration> CalculateTotalGenerations(GenerationReport report)
        {
            var totals = new List<TotalGeneration>();

            totals.AddRange(CalculateTotalGenerationForGenerators(report.WindGenerators));
            totals.AddRange(CalculateTotalGenerationForGenerators(report.GasGenerators));
            totals.AddRange(CalculateTotalGenerationForGenerators(report.CoalsGenerators));

            return totals;
        }

        private List<TotalGeneration> CalculateTotalGenerationForGenerators<T>(List<T> generators) where T : GeneratorBase
        {
            var totals = new List<TotalGeneration>();

            foreach (var generator in generators)
            {
                var total = new TotalGeneration(generator, _mapping.GetValueFactor(generator));

                totals.Add(total);
            }

            return totals;
        }
    }
}