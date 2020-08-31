
namespace Catalog.API.Settings
{
    public interface ICatalogDatabaseSettings
    {
        string  ConnectionString { get; set; }
        string  DatabaseName    { get; set; }
        string  CollectionName { get; set; }
        
    }
    public class CatalogDatabaseSettings : ICatalogDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set ; }
        public string CollectionName { get ; set; }
    }
}