namespace estiaApi.Models
{
    public class EstiaDatabaseSettings : IEstiaDatabaseSettings
    {
        public string BuildingsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IEstiaDatabaseSettings
    {
        string BuildingsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}