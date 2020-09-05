namespace eShop.Web.Settings
{
    public interface IApiSettings
    {
        string BaseAddress { get; set; }
        string BasketPath { get; set; }
        string CatalogPath { get; set; }
        string OrderPath { get; set; }
    }
}