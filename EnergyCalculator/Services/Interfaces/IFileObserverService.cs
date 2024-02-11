namespace EnergyCalculator.Services.Interfaces
{
    public interface IFileObserverService
    {
        void StartObserving();

        void StopObserving();
    }
}