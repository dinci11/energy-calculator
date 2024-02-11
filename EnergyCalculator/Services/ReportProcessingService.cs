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

            var output = new GenerationOutput
            {
                Totals = await totalGenerations,
            };

            return output;
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
                var total = new TotalGeneration(generator, _mapping.GetValueFactor(generator), _mapping.GetEmissionFactor(generator));

                totals.Add(total);
            }

            return totals;
        }
    }
}