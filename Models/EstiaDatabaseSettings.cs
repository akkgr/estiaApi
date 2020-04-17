namespace estiaApi.Models
{
    public class EstiaDatabaseSettings : IEstiaDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string BuildingsCollectionName { get; set; }
        public string ApartmentsCollectionName { get; set; }
    }

    public interface IEstiaDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string BuildingsCollectionName { get; set; }
        string ApartmentsCollectionName { get; set; }
    }
}