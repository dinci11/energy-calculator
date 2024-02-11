namespace EnergyCalculator.Services.Interfaces
{
    public interface IXmlService
    {
        T Deserialize<T>(Stream stream) where T : class;

        string SerializeToString<T>(T entity) where T : class;
    }
}